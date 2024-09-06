using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameManagerObject = new GameObject("GameManager");
                _instance = gameManagerObject.AddComponent<GameManager>();
                DontDestroyOnLoad(gameManagerObject);
            }
            return _instance;
        }
    }

    private Status _playerStatus;

    public Status PlayerStatus { get { return _playerStatus; } }

    [SerializeField] private GameObject _playerPrefab;

    private int _currentStage = 0;

    public int CurrentStage { get { return _currentStage; } }

    [SerializeField] private List<StageInfo> _stages;

    public StageInfo CurrentStageInfo { get { return _stages[_currentStage]; } }

    private PlayerBuildProperty _playerBuildProperty;

    private WeaponObj _currentWeapon;

    [SerializeField] private WeaponObj _basicWeapon;

    public WeaponObj CurrentWeapon { get { return _currentWeapon; } }

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        _currentStage = 0;

        _playerStatus = new Status();
        InitializePlayerStatus();
    }

    public void SwitchScene(string name)
    {

        SceneManager.LoadScene(name);
    }
    
    // 일반 씬 초기화
    public void Initialize()
    {
        _currentStage++;

        Debug.Log($"현재 스테이지가 {_currentStage}이 됩니다.");

        BuildManager.Instance.LeftTurn = _stages[_currentStage].nextTurnCount;

    }


    public void InitializeBattle()
    {
        GameObject playerSpawnPosition = GameObject.Find("PlayerSpawnPoint");
        GameObject enemySpawnPosition = GameObject.Find("EnemySpawnPoint");

        ActorBase player = Instantiate(_playerPrefab, playerSpawnPosition.transform.position, Quaternion.identity)
            .GetComponent<ActorBase>();
        
        ActorBase monster = Instantiate(_stages[_currentStage].EnemyPrefab, enemySpawnPosition.transform.position, Quaternion.identity)
            .GetComponent<ActorBase>();

        Debug.Log($"{_stages[_currentStage].EnemyPrefab.name} 을 소환해!!!!!!!!!!!!!!!!!!!!!!!!");

        if (_playerStatus == null)
        {
            Debug.Log("[GameManager.cs] Player Status Initialize Error!");
        }

        Debug.Log($"Intialized GameManager.cs {gameObject.GetInstanceID()}");

        BattleManager.Instance.InitializeActor(player, monster);

    }

    public void InitializeBattle_OneTime()
    {
        GameObject playerSpawnPosition = GameObject.Find("PlayerSpawnPoint");
        GameObject enemySpawnPosition = GameObject.Find("EnemySpawnPoint");

        ActorBase player = Instantiate(_playerPrefab,
            playerSpawnPosition.transform.position, Quaternion.identity)
            .GetComponent<ActorBase>();

        ActorBase monster = Instantiate(_stages[CurrentStage].EnemyPrefab,
            enemySpawnPosition.transform.position, Quaternion.identity)
            .GetComponent<ActorBase>();

        if (_playerStatus == null)
        {
            Debug.Log("[GameManager.cs] Player Status Initialize Error!");
        }

        Debug.Log($"Intialized GameManager.cs {gameObject.GetInstanceID()}");

        BattleManager.Instance.InitializeActor(player, monster);
    }


    private void InitializePlayerStatus()
    {
        _playerStatus.MaxHp = 200;
        _playerStatus.Hp = _playerStatus.MaxHp;
        _playerStatus.Attack = 10;
        _playerStatus.Accuracy = 1;
        _playerStatus.Evade = 1;
        _playerStatus.Guard = 1;

        _playerStatus.Strike = 0;
        _playerStatus.Slash = 0;
        _playerStatus.Penetration = 0;
        _playerStatus.Ranged = 0;
        _playerStatus.Counter = 0;

        _playerBuildProperty = GetComponent<PlayerBuildProperty>();

        _currentWeapon = _basicWeapon;
    }

    public void EditGold(int money)
    {
        _playerBuildProperty._money += money;
    }

    public void SwapWeapon(WeaponObj weapon)
    {
        _currentWeapon = weapon;
    }


    #region Status Management

    public void SetStatus(Status status)
    {
        _playerStatus = status;
    }

    public void EditMaxHp(float hp)
    {
        _playerStatus.MaxHp += hp;
    }

    public void EditAttack(float attack)
    {
        _playerStatus.Attack += attack;
    }

    public void EditAccuracy(float accuracy)
    {
        _playerStatus.Accuracy += accuracy;
    }

    public void EditEvade(float evade)
    {
        _playerStatus.Evade += evade;
    }

    public void EditGuard(float guard)
    {
        _playerStatus.Guard += guard;
    }

    public void EditStrike(float strike)
    {
        _playerStatus.Strike += strike;
    }

    public void EditSlash(float slash)
    {
        _playerStatus.Slash += slash;
    }

    public void EditPenetration(float penetration)
    {
        _playerStatus.Penetration += penetration;
    }

    public void EditRanged(float ranged)
    {
        _playerStatus.Ranged += ranged;
    }

    public void EditCounter(float counter)
    {
        _playerStatus.Counter += counter; 
    }

    #endregion
}
