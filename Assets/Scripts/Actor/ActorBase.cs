using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class ActorBase : MonoBehaviour
{
    private Status _status;

    public Status Status { get { return _status; } }

    private List<Func<SkillInfo>> _actions;

    public List<Func<SkillInfo>> Actions { get { return _actions; } }

    private Animator _animator;
    
    public Animator Animator { get { return _animator; } }

    public virtual void Intialize()
    {
        _status = new Status();
        _actions = new List<Func<SkillInfo>>();
        _animator = GetComponent<Animator>();

        // TODO : Animator Idle로 설정하기

        Debug.Log($"Initialized {name}");
    }

    public Func<SkillInfo> Attack()
    {
        int result = UnityEngine.Random.value < 0.3f ? 2 : 0;

        return Actions[result];
    }

    public bool GetDamageCheckDead(int damage)
    {
        Status.Hp -= damage;
        
        if (Status.Hp < 0)
        {
            return true;
        }

        return false;
    }

    public void StopAnimation()
    {
        _animator.StopPlayback();
    }
}


