using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BattleManager : MonoBehaviour
{
    private static BattleManager _instance;
    public static BattleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject battleManagerObject = new GameObject("BattleManager");
                _instance = battleManagerObject.AddComponent<BattleManager>();
                DontDestroyOnLoad(battleManagerObject);
            }
            return _instance;
        }
    }

    private ActorBase _player;
    private ActorBase _enemy;


    // Head to next Turn
    private bool _isNextTurn;
    // Player or Enemy Turn
    private bool _isPlayerTurn;
    // is On Fighting
    private bool _isFighting;

    public void InitializeActor(ActorBase player, ActorBase enemy)
    {

        _player = player;
        _enemy = enemy;

        _isNextTurn = false;
        _isPlayerTurn = false;
        _isFighting = false;

        _player.Intialize();
        _enemy.Intialize();

        StartCoroutine(StartBattleInTime(4f));
    }

    private void Update()
    {
        // Quit Process if not Fighting
        if (!_isFighting) return;

        // Turn Changing
        if (_isNextTurn) ChangeTurn();
    }

    private void ChangeTurn()
    {
        _isPlayerTurn = !_isPlayerTurn;

        _isNextTurn = false;

        ProceedAttack();
    }

    private void ProceedAttack()
    {
        if (_isPlayerTurn) StartCoroutine(AttackRoutine(_player.Attack()));
        else StartCoroutine(AttackRoutine(_enemy.Attack()));
    }

    private IEnumerator AttackRoutine(Func<SkillInfo> attackAction)
    {
        yield return null;

        SkillInfo skillInfo = attackAction.Invoke();

        if (skillInfo.IsRepeated)
        {
            int cnt = 0;
            while (cnt < skillInfo.MaxRepeatCount)
            {
                if (UnityEngine.Random.value < skillInfo.RepeatProbability)
                {
                    cnt++;
                }
                else break;
            }

            for (int i = 0; i < cnt - 1; i++)
            {
                // 보여주기용
                attackAction.Invoke();
            }
        }

        yield return new WaitForSeconds(skillInfo.Delay);

        _isNextTurn = true;
    }

    private void StopMoving()
    {
        _player.StopAnimation();
        _enemy.StopAnimation();
    }

    private IEnumerator StartBattleInTime(float time)
    {
        // TODO : 전투 시작 이펙트 적용

        yield return new WaitForSeconds(time);

        _isNextTurn = true;
        _isFighting = true;
    }
}
