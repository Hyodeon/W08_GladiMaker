using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Enemy_Boss : ActorBase
{
    [Header("<color=yellow>�⺻ ����")]
    [Space]
    [Header("�ִ� ü�� <color=yellow>(��)")]
    [SerializeField] private float _maxHp;
    [Header("���ݷ� <color=yellow>(��)")]
    [SerializeField] private float _attack;
    [Header("��Ȯ�� <color=purple>(%)")]
    [SerializeField] private float _accuracy;
    [Header("ȸ���� <color=purple>(%)")]
    [SerializeField] private float _evade;
    [Header("�� <color=yellow>(��)")]
    [SerializeField] private float _guard;

    [Space]

    [Header("<color=red>Ư�� ����")]
    [Space]
    [Header("Ÿ�� <color=purple>(%)")]
    [SerializeField] private float _strike;
    [Header("���� <color=purple>(%)")]
    [SerializeField] private float _slash;
    [Header("���� <color=purple>(%)")]
    [SerializeField] private float _penetration;
    [Header("��ô <color=purple>(%)")]
    [SerializeField] private float _ranged;
    [Header("�ݰ� <color=purple>(%)")]
    [SerializeField] private float _counter;

    [Space]
    [Space]

    [Header("<color=green>�ൿ �ִϸ��̼� Ŭ��")]
    [SerializeField]
    private List<AnimationClip> _animators = new List<AnimationClip>();


    public override void Intialize()
    {
        base.Intialize();
        DropWeapon = SetRandomDropItem();

        Status.MaxHp = _maxHp;
        Status.Hp = _maxHp;
        Status.Attack = _attack;
        Status.Accuracy = _accuracy;
        Status.Evade = _evade;
        Status.Guard = _guard;

        Status.Strike = _strike;
        Status.Slash = _slash;
        Status.Penetration = _penetration;
        Status.Ranged = _ranged;
        Status.Counter = _counter;

        _currentWeapon.Mechanic = SkillMechanism.Monster;
        ConnectHealthBar();
        BindActions();
    }

    void ConnectHealthBar()
    {
        HealthBar = GameObject.Find("EnemyHP").GetComponent<HealthBar>();
        HealthBar.Enemy = gameObject;
        HealthBar.max_HP = _maxHp;
        HealthBar.Initialize();
    }

    GameObject SetRandomDropItem()
    {
        float rand = Random.Range(0, 101);
        if (rand <= 10) return LegendaryWeapons[Random.Range(0, LegendaryWeapons.Count)];
        else if (rand <= 25) return RareWeapons[Random.Range(0, RareWeapons.Count)];
        else if (rand <= 50) return EpicWeapons[Random.Range(0, EpicWeapons.Count)];
        else return NormalWeapons[Random.Range(0, NormalWeapons.Count)];
    }
    public void BindActions()
    {
        Actions.Add(Action_Attack);
        Actions.Add(Action_Skill);
    }

    public SkillInfo Action_Attack()
    {
        Debug.Log($"{name} Attack");

        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio = 1f;
        skillInfo.PlayerDamage = CalculateDamage();
        skillInfo.Clip = _animators[0];
        skillInfo.IsRepeated = false;

        return skillInfo;
    }

    public SkillInfo Action_Skill()
    {
        Debug.Log($"{name} Skill");

        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio = 5f;
        skillInfo.PlayerDamage = CalculateDamage();
        skillInfo.Clip = _animators[1];
        skillInfo.IsRepeated = false;

        return skillInfo;
    }

    //public SkillInfo TestRepeatedSkill()
    //{
    //    Debug.Log($"{name} Repeated Skill!!!!");

    //    SkillInfo skillInfo = new SkillInfo();
    //    skillInfo.DamageRatio = 2f;
    //    skillInfo.Delay = 1f;
    //    skillInfo.IsRepeated = true;
    //    skillInfo.RepeatProbability = 0.5f;
    //    skillInfo.MaxRepeatCount = 5;

    //    return skillInfo;
    //}
}
