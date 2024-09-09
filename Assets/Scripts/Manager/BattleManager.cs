using DG.Tweening;
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

        // 1. 회피 계산
        var damage = skillInfo.PlayerDamage * skillInfo.DamageRatio;
        if (!CalculateEvade()) damage = 0f;

        _damageQueue.Enqueue(CalculateDamage(damage));


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
        float currentDamage = (float)System.Math.Round(_damageQueue.Dequeue(),0);

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

        StartCoroutine(GetFasterAfterTime());
    }

    private IEnumerator EndBattle()
    {
        Time.timeScale = 1;
        _isFighting = false;

        Camera.main.GetComponent<CameraShake>().startCameraShake(.05f, .15f);

        if (_isPlayerTurn)
        {
            GameObject.Find("Canvas").GetComponent<ShowClearPanel>().ShowDieImage(_isPlayerTurn);
            _enemy.PlayAnimationClip("Die");
            _player.GetComponent<Animator>().Play("Win");
            _player.Status.Hp = _player.Status.MaxHp;
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<ShowClearPanel>().ShowDieImage(_isPlayerTurn);
            _player.GetComponent<Animator>().Play("Die");
            GameOver.SetActive(true);
        }

        Time.timeScale = .2f;
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;

        yield return new WaitForSeconds(2f);

        float goldWeight = 1f;

        float attackRate;
        float overKillRate;
        float lowHpRate;
        float criticalRate;

        // 1. 공격 빈도 배수
        attackRate = 1f + _playerAttackCount / 100;

        // 2. 체력보다 많은 양의 데미지로 죽였을 때의 배수
        overKillRate = 1f + Mathf.Abs((float)_enemy.Status.Hp / (float)_enemy.Status.MaxHp) * 6;
        overKillRate = overKillRate > 20 ? 20 : overKillRate;

        // 3. 체력이 낮을 때 배수
        lowHpRate = 1f + (_player.Status.Hp <= _player.Status.MaxHp * 0.1f ? 3f : 3f * (1f - _player.Status.Hp / _player.Status.MaxHp));

        // 4. 강력한 공격을 수행했을 때의 배수
        criticalRate = 1f + _criticalAttackCount / 2;

        goldWeight *= attackRate * overKillRate * lowHpRate * criticalRate;

        GameManager.Instance.GetComponent<PlayerBuildProperty>()._trainingRate += 0.02f;

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

    private bool CalculateEvade()
    {
        ActorBase attacker, defender;

        if (_isPlayerTurn)
        {
            attacker = _player;
            defender = _enemy;
        }
        else
        {
            attacker = _enemy;
            defender = _player;
        }


        float baseValue = 0.9f;

        if (attacker.Status.Accuracy > defender.Status.Evade)
        {
            return true;
        }
        else if (defender.Status.Evade >= 2 * attacker.Status.Accuracy)
        {
            return UnityEngine.Random.Range(0,1f) <= 0.5f;
        }
        else
        {
            // evade가 accuracy보다 클 때, 선형적으로 값이 변하도록 함
            float t = (defender.Status.Evade - attacker.Status.Accuracy) / attacker.Status.Accuracy;
            Debug.Log("Evade Percent : " + Mathf.Lerp(baseValue, 0.5f, t));
            return UnityEngine.Random.Range(0, 1f) <= Mathf.Lerp(baseValue, 0.5f, t);
        }
    }

    private float CalculateDamage(float damage)
    {
        if (damage == 0)
        {
            return 0;
        }

        ActorBase attacker, defender;

        if (_isPlayerTurn)
        {
            attacker = _player;
            defender = _enemy;
        }
        else
        {
            attacker = _enemy;
            defender = _player;
        }

        float defenderDefence = defender.Status.Guard;

        defenderDefence *= 1f + (defenderDefence * 0.00005f);

        if (damage <= defenderDefence) { return 1; }
        else return damage - defenderDefence;
    }

    private IEnumerator GetFasterAfterTime()
    {
        yield return null;

        while (_isFighting)
        {
            yield return new WaitForSeconds(5f);
            Time.timeScale += 0.5f;
        }

        Time.timeScale = 1;
    }
}
