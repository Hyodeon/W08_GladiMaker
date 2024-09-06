using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class ActorBase : MonoBehaviour
{

    public string Name;
    public Sprite EnemyAvatar;

    [Header("��� ������")]
    public List<GameObject> NormalWeapons;
    public List<GameObject> RareWeapons;
    public List<GameObject> EpicWeapons;
    public List<GameObject> LegendaryWeapons;

    public GameObject DropWeapon;

    [Header("ü�¹�")]
    public HealthBar HealthBar;

    protected Status _status;

    public Status Status { get { return _status; } }

    private List<Func<SkillInfo>> _actions;

    public List<Func<SkillInfo>> Actions { get { return _actions; } }

    protected Animator _animator;

    protected WeaponObj _weaponObject;

    protected Weapon _currentWeapon;

    public Weapon CurrentWeapon { get { return _currentWeapon; } }

    public Animator Animator { get { return _animator; } }

    [SerializeField] GameObject _damageTextPrefab;

    public virtual void Intialize()
    {
        _status = new Status();
        _actions = new List<Func<SkillInfo>>();
        _animator = GetComponent<Animator>();

        // TODO : Animator Idle�� �����ϱ�
        _animator.Play("Idle");

        _currentWeapon = new Weapon();

        Debug.Log($"Initialized {name}");
    }

    public Func<SkillInfo> Attack(int turnCount)
    {
        switch (CurrentWeapon.Mechanic)
        {
            case SkillMechanism.TurnBased:

                if (turnCount % CurrentWeapon.Turn_TurnCount == 0)
                {
                    // TODO : ��ų �ߵ�
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

            case SkillMechanism.Non_Skill:

                return Actions[0];

            case SkillMechanism.Monster:
                int result2 = UnityEngine.Random.value
                    < 0.2f
                    ? 1 : 0;

                return Actions[result2];

        }

        return Actions[0];
    }

    public bool GetDamageCheckDead(int damage)
    {

        // ������ ����
        Status.Hp -= damage;
        Debug.Log($"{name}: {damage} ��ŭ�� ���ظ� �Ծ����ϴ�! (���� ü��: {Status.Hp}");
        
        // ü�¹� ����
        HealthBar.current_HP = Status.Hp;
        HealthBar.GetComponent<Animator>().Play("Damaged");

        // ������ �ؽ�Ʈ ����
        var damageText = Instantiate(_damageTextPrefab, this.transform);
        damageText.GetComponent<RectTransform>().anchoredPosition = damageText.GetComponent<RectTransform>().anchoredPosition + Vector2.up * 200f;
        damageText.GetComponent<TMPro.TMP_Text>().text = damage.ToString();

        HealthBar.current_HP = Status.Hp;

        if (Status.Hp <= 0f)
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

    public virtual void SwitchWeapon(WeaponObj weapon)
    {
        if (weapon == null) _currentWeapon = new Weapon();
        _currentWeapon = weapon.weapon;
    }

    protected float CalculateDamage()
    {
        float ratio = 1f;
        float damage = 0f;

        // 1. ���ݷ� ����� ���� ���ݷ� ����
        ratio *= 1f + (Status.Attack * 0.005f);
        damage += Status.Attack;

        // 2. ���� ���ݷ� �ջ�
        if (gameObject.tag == "Player")
            ratio *= CurrentWeapon.WeaponDamage / 100;

        // 3. ���� Ÿ�Կ� ���� ���
        ratio *= 1 + CurrentWeapon.Type switch
        {
            WeaponAttackType.Strike => Status.Strike / 100,
            WeaponAttackType.Slash => Status.Slash / 100,
            WeaponAttackType.Ranged => Status.Ranged / 100,
            WeaponAttackType.Penetration => Status.Penetration / 100,
            _ => 0f
        };

        float randum = UnityEngine.Random.Range(0.8f, 1.2f);

        ratio *= randum;

        return ratio * damage;
    }

    public void CallTakeDamage()
    {
        BattleManager.Instance.TakeDamage();
    }
    
}
