using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Player : ActorBase
{
    [Header("Basic Stats")]
    [Space]
    [Header("MaxHp (value)")]
    [SerializeField] private float _maxHp;
    [Header("Attack (value)")]
    [SerializeField] private float _attack;
    [Header("Accuracy (%)")]
    [SerializeField] private float _accuracy;
    [Header("Evade (%)")]
    [SerializeField] private float _evade;
    [Header("Guard (value)")]
    [SerializeField] private float _guard;

    [Space]

    [Header("Specific Stats")]
    [SerializeField] private float _strike;
    [SerializeField] private float _slash;
    [SerializeField] private float _penetration;
    [SerializeField] private float _ranged;
    [SerializeField] private float _counter;


    public override void Intialize()
    {
        base.Intialize();

        _status = GameManager.Instance.PlayerStatus;

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
        skillInfo.Delay = 0.5f;
        skillInfo.IsRepeated = false;

        return skillInfo;
    }

    public SkillInfo TestSkill()
    {
        Debug.Log($"{name} Skill");

        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio = 2f;
        skillInfo.Delay = 1f;
        skillInfo.IsRepeated = false;

        return skillInfo;
    }

    public SkillInfo TestRepeatedSkill()
    {
        Debug.Log($"{name} Repeated Skill!!!!");

        // 최대 5회 반복, 50퍼센트 확률로 여러번 발동
        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio = 2f;
        skillInfo.Delay = 1f;
        skillInfo.IsRepeated = true;
        skillInfo.RepeatProbability = 0.9f;
        skillInfo.MaxRepeatCount = 5;

        return skillInfo;
    }
}
