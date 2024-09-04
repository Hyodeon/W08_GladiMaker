using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemySample : ActorBase
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
