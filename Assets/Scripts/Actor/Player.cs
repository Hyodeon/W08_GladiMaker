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

    [Space]
    [SerializeField] HealthBar myHealthBar;

    private void Start()
    {
        Intialize();
    }

    [Header("<color=green>행동 애니메이션 클립")]
    [SerializeField]
    private List<AnimationClip> _animators = new List<AnimationClip>();


    public override void Intialize()
    {
        base.Intialize();

        _status = GameManager.Instance.PlayerStatus;

        Debug.Log($"{gameObject.GetInstanceID()}");

        if (_status == null)
        {
            Debug.Log("[Player.cs] Player Status Initialize Error!");
        }

        myHealthBar = GameObject.Find("PlayerHP").GetComponent<HealthBar>();
        myHealthBar.player = this;
        myHealthBar.max_HP = _maxHp;
        myHealthBar.Initialize();

        BindActions();
    }

    public void BindActions()
    {
        Actions.Add(Action_Attack);
        Actions.Add(Action_Skill);
        Actions.Add(TestRepeatedSkill);
    }

    public override void PlayAnimationClip(string name)
    {
        _animator.SetTrigger(WeaponType.Sword.ToString());
    }

    public SkillInfo Action_Attack()
    {
        Debug.Log($"{name} Attack");

        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio = 1f;
        skillInfo.PlayerDamage = CalculateDamage();
        skillInfo.Clip = _animators[5];
        skillInfo.IsRepeated = false;

        return skillInfo;
    }

    public SkillInfo Action_Skill()
    {
        Debug.Log($"{name} Skill");

        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio = 2f;
        skillInfo.PlayerDamage = CalculateDamage();
        skillInfo.Clip = _animators[5];
        skillInfo.IsRepeated = false;

        return skillInfo;
    }

    public SkillInfo TestRepeatedSkill()
    {
        Debug.Log($"{name} Repeated Skill!!!!");

        // 최대 5회 반복, 50퍼센트 확률로 여러번 발동
        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio = 2f;
        skillInfo.Clip = _animators[2];
        skillInfo.IsRepeated = true;
        skillInfo.RepeatProbability = 0.9f;
        skillInfo.MaxRepeatCount = 5;

        return skillInfo;
    }
}
