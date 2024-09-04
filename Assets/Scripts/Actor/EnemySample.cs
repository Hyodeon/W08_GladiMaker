using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemySample : ActorBase
{
    [Header("<color=yellow>기본 스탯")]
    [Space]
    [Header("최대 체력 <color=yellow>(값)")]
    [SerializeField] private float _maxHp;
    [Header("공격력 <color=yellow>(값)")]
    [SerializeField] private float _attack;
    [Header("정확도 <color=purple>(%)")]
    [SerializeField] private float _accuracy;
    [Header("회피율 <color=purple>(%)")]
    [SerializeField] private float _evade;
    [Header("방어도 <color=yellow>(값)")]
    [SerializeField] private float _guard;

    [Space]

    [Header("<color=red>특수 스탯")]
    [Space]
    [Header("타격 <color=purple>(%)")]
    [SerializeField] private float _strike;
    [Header("참격 <color=purple>(%)")]
    [SerializeField] private float _slash;
    [Header("관통 <color=purple>(%)")]
    [SerializeField] private float _penetration;
    [Header("투척 <color=purple>(%)")]
    [SerializeField] private float _ranged;
    [Header("반격 <color=purple>(%)")]
    [SerializeField] private float _counter;

    [Space]
    [Space]

    [Header("<color=green>행동 애니메이션 클립")]
    [SerializeField]
    private List<AnimationClip> _animators = new List<AnimationClip>();

    [Space]
    [SerializeField] HealthBar myHealthBar;

    private void Start()
    {
        Intialize();
    }

    public override void Intialize()
    {
        base.Intialize();

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

        //myHealthBar = GameObject.Find("EnemyHP").GetComponent<HealthBar>();
        //myHealthBar.Enemy = this;
        //myHealthBar.max_HP = _maxHp;
        //myHealthBar.Initialize();
        BindActions();
    }

    public void BindActions()
    {
        Actions.Add(TestAction);
        Actions.Add(TestSkill);
        Actions.Add(TestRepeatedSkill);
    }

    public SkillInfo TestAction()
    {
        Debug.Log($"{name} Attack");

        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio = 1f;
        skillInfo.Clip = _animators[0];
        skillInfo.IsRepeated = false;

        return skillInfo;
    }

    public SkillInfo TestSkill()
    {
        Debug.Log($"{name} Skill");

        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio = 2f;
        skillInfo.Clip = _animators[1];
        skillInfo.IsRepeated = false;

        return skillInfo;
    }

    public SkillInfo TestRepeatedSkill()
    {
        Debug.Log($"{name} Repeated Skill!!!!");

        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio = 2f;
        skillInfo.Clip = _animators[2];
        skillInfo.IsRepeated = true;
        skillInfo.RepeatProbability = 0.5f;
        skillInfo.MaxRepeatCount = 5;

        return skillInfo;
    }
}
