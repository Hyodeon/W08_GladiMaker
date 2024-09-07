using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using static UnityEngine.Rendering.DebugUI;

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


    [SerializeField] GameObject GameOver;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

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

        StartCoroutine(StartBattleInTime(1f));
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

        // 방어력 로직 계산


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

        Debug.Log($"{skillInfo.Clip.length + 0.3f}초 뒤에 다음 턴 진행");
        
        yield return new WaitForSeconds(skillInfo.Clip.length + 0.15f);

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

        if (!_isPlayerTurn)
        {
            if (_player.GetDamageCheckDead(currentDamage))
            {
                StartCoroutine(EndBattle());
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
                StartCoroutine(EndBattle());
            }
        }
    }

    private IEnumerator StartBattleInTime(float time)
    {
        yield return new WaitForSeconds(time);

        _isNextTurn = true;
        _isFighting = true;
    }

    private IEnumerator EndBattle()
    {
        _isFighting = false;

        if (_isPlayerTurn)
        {
            _enemy.PlayAnimationClip("Die");
            _player.GetComponent<Animator>().Play("Win");
            _player.Status.Hp = _player.Status.MaxHp;
        }
        else
        {
            _player.GetComponent<Animator>().Play("Die");
            GameOver.SetActive(true);
        }

        yield return new WaitForSeconds(2f);


        float goldWeight = 1f;

        float attackRate;
        float overKillRate;
        float lowHpRate;
        float criticalRate;

        // 1. 공격 빈도 배수
        attackRate = 1f + _playerAttackCount / 100;

        // 2. 체력보다 많은 양의 데미지로 죽였을 때의 배수
        overKillRate = 1f + Mathf.Abs(_enemy.Status.Hp / _enemy.Status.MaxHp) * 6;

        // 3. 체력이 낮을 때 배수
        lowHpRate = 1f + (_player.Status.Hp <= _player.Status.MaxHp * 0.1f ? 3f : 3f * (1f - _player.Status.Hp / _player.Status.MaxHp));

        // 4. 강력한 공격을 수행했을 때의 배수
        criticalRate = 1f + _criticalAttackCount / 2;

        goldWeight *= attackRate * overKillRate * lowHpRate * criticalRate;

        FindAnyObjectByType<ShowClearPanel>().
            ConnectUI(
                goldWeight,
                attackRate,
                overKillRate,
                lowHpRate,
                criticalRate,
                _enemy.DropWeapon.GetComponent<WeaponObj>()
            );
    }
}
