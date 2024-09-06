using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class BattleManager : MonoBehaviour
{
    private static BattleManager _instance;
    public static BattleManager Instance => _instance;


    private ActorBase _player;
    private ActorBase _enemy;

    private Queue<float> _damageQueue;

    // Head to next Turn
    private bool _isNextTurn;
    // Player or Enemy Turn
    private bool _isPlayerTurn;

    public bool IsPlayerTurn { get { return _isPlayerTurn; } }

    // is On Fighting
    private bool _isFighting;

    private int _playerTurnCount;
    private int _enemyTurnCount;

    private int _playerAttackCount;
    private int _criticalAttackCount;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    [SerializeField] GameObject ClearMenu;

    public void InitializeActor(ActorBase player, ActorBase enemy)
    {

        _player = player;
        _enemy = enemy;

        _isNextTurn = false;
        _isPlayerTurn = false;
        _isFighting = false;

        _playerTurnCount = 0;
        _enemyTurnCount = 0;

        _criticalAttackCount = 0;
        _playerAttackCount = 0;

        _player.Intialize();
        _enemy.Intialize();

        _damageQueue = new Queue<float>();

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
        if (_isPlayerTurn)
        {
            StartCoroutine(AttackRoutine(_player.Attack(_playerTurnCount)));
            _playerTurnCount++;
            _playerAttackCount++;
        }
        else
        {
            StartCoroutine(AttackRoutine(_enemy.Attack(_enemyTurnCount)));
        }
    }

    private IEnumerator AttackRoutine(Func<SkillInfo> attackAction)
    {
        yield return null;

        SkillInfo skillInfo = attackAction.Invoke();

        _damageQueue.Enqueue(skillInfo.PlayerDamage * skillInfo.DamageRatio);

        if (_isPlayerTurn) _player.PlayAnimationClip(skillInfo.Clip.name);
        else _enemy.PlayAnimationClip(skillInfo.Clip.name);

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
                attackAction.Invoke();
            }
        }

        yield return new WaitForSeconds(skillInfo.Clip.length + 0.5f);

        _isNextTurn = true;
    }

    private void StopMoving()
    {
        _player.StopAnimation();
        _enemy.StopAnimation();
    }

    public void TakeDamage()
    {
        int currentDamage = Mathf.CeilToInt(_damageQueue.Dequeue());

        Debug.Log($"{currentDamage} �� �������� �־����ϴ�!");


        if (!_isPlayerTurn)
        {
            if (_player.GetDamageCheckDead(currentDamage))
            {
                // TODO : ���� ����
                Debug.Log("Game Over");
                EndBattle();
            }
        }
        else
        {
            if (currentDamage > _enemy.Status.MaxHp / 10)
            {
                _criticalAttackCount++;
            }

            if (_enemy.GetDamageCheckDead(currentDamage))
            {
                // TODO : ��� ����
                Debug.Log("Killed Enemy");
                EndBattle();
            }
        }
    }

    private IEnumerator StartBattleInTime(float time)
    {
        // TODO : ���� ���� ����Ʈ ����

        yield return new WaitForSeconds(time);

        _isNextTurn = true;
        _isFighting = true;
    }

    private void EndBattle()
    {
        _isFighting = false;

        if (_isPlayerTurn)
        {
            _enemy.PlayAnimationClip("Die");
        }


        float goldWeight = 1f;

        float attackRate;
        float overKillRate;
        float lowHpRate;
        float criticalRate;

        _player.GetComponent<Animator>().Play("Win");

        // 1. �÷��̾� ���� Ƚ��
        attackRate = 1f + _playerAttackCount / 100;

        // 2. ����ų ����Ʈ
        overKillRate = 1f + Mathf.Abs(_enemy.Status.Hp / _enemy.Status.MaxHp) * 6;

        // 3. �÷��̾� ü�� ����
        lowHpRate = 1f;

        // 4. ������ ����
        criticalRate = 1f + _criticalAttackCount / 10;

        goldWeight *= attackRate * overKillRate * lowHpRate * criticalRate;

        // TODO : ��� �߰�
        GameObject.FindAnyObjectByType<ShowClearPanel>().ConnectUI(goldWeight, attackRate, overKillRate, lowHpRate, criticalRate, _enemy.DropWeapon.GetComponent<WeaponObj>());
    }
}
