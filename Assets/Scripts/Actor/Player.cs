using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Player : ActorBase
{
    [Header("<color=green>�ൿ �ִϸ��̼� Ŭ��")]
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

        // �ִ� 5ȸ �ݺ�, 50�ۼ�Ʈ Ȯ���� ������ �ߵ�
        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio = 2f;
        skillInfo.Clip = _animators[2];
        skillInfo.IsRepeated = true;
        skillInfo.RepeatProbability = 0.9f;
        skillInfo.MaxRepeatCount = 5;

        return skillInfo;
    }
}
