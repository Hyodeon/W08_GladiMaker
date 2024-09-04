using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class ActorBase : MonoBehaviour
{
    protected Status _status;

    public Status Status { get { return _status; } }

    private List<Func<SkillInfo>> _actions;

    public List<Func<SkillInfo>> Actions { get { return _actions; } }

    protected Animator _animator;

    private Weapon _currentWeapon;

    public Weapon CurrentWeapon { get { return _currentWeapon; } }

    public Animator Animator { get { return _animator; } }

    public virtual void Intialize()
    {
        _status = new Status();
        _actions = new List<Func<SkillInfo>>();
        _animator = GetComponent<Animator>();

        // TODO : Animator Idle로 설정하기
        _animator.Play("Idle");

        _currentWeapon = new Weapon();

        SwitchWeapon(new Weapon());

        Debug.Log($"Initialized {name}");
    }

    public Func<SkillInfo> Attack(int turnCount)
    {
        switch (CurrentWeapon.Mechanic)
        {
            case SkillMechanism.TurnBased:

                if (turnCount % CurrentWeapon.Turn_TurnCount == 0)
                {
                    // TODO : 스킬 발동
                    return Actions[1];
                }
                else
                {
                    return Actions[0];
                }

            case SkillMechanism.Percentage:

                int result = UnityEngine.Random.value
                    < CurrentWeapon.Per_ActivationPercentage / 100
                    ? 1 : 0;

                return Actions[result];

        }

        return Actions[0];
    }

    public void BindWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    public bool GetDamageCheckDead(int damage)
    {

        Status.Hp -= damage;
        Debug.Log($"{name}: {damage} 만큼의 피해를 입었습니다! (현재 체력: {Status.Hp}");

        if (Status.Hp < 0)
        {
            return true;
        }

        return false;
    }

    public virtual void PlayAnimationClip(string name)
    {
        _animator.Play(name);
    }

    public void StopAnimation()
    {
        _animator.StopPlayback();
    }

    public void SwitchWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;

        // Test Code
        _currentWeapon.WeaponDamage = 200;
        _currentWeapon.Type = WeaponAttackType.Strike;
        _currentWeapon.Tier = WeaponTier.Epic;
        _currentWeapon.Mechanic = SkillMechanism.Percentage;
        _currentWeapon.Per_DamageRatio = 1.5f;
        _currentWeapon.Per_ActivationPercentage = 10;
    }

    protected float CalculateDamage()
    {
        float ratio = 1f;
        float damage = 0f;

        // 1. 공격력 배수에 따른 공격력 증가
        ratio *= 1f + (Status.Attack * 0.005f);
        damage += Status.Attack;

        // 2. 무기 공격력 합산
        ratio *= CurrentWeapon.WeaponDamage / 100;

        // 3. 무기 타입에 따른 배수
        ratio *= 1 + CurrentWeapon.Type switch
        {
            WeaponAttackType.Strike => Status.Strike / 100,
            WeaponAttackType.Slash => Status.Slash / 100,
            WeaponAttackType.Ranged => Status.Ranged / 100,
            WeaponAttackType.Penetration => Status.Penetration / 100,
            _ => 0f
        };

        return ratio * damage;
    }

    public void CallTakeDamage()
    {
        BattleManager.Instance.TakeDamage();
    }
    
}
