using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [SerializeField] ActorBase _player;
    [SerializeField] ActorBase _monster;

    private Status _playerStatus;

    public Status PlayerStatus { get { return _playerStatus; } } 

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<ActorBase>();
        _monster = GameObject.Find("Monster").GetComponent<ActorBase>();

        BattleManager.Instance.InitializeActor(
            _player, _monster);

        Initialize();
    }

    public void Initialize()
    {
        _playerStatus = new Status();
    }

    #region Status Management

    public void SetStatus(Status status)
    {
        _playerStatus = status;
    }

    public void EditMaxHp(float hp)
    {
        _playerStatus.MaxHp = hp;
    }

    public void EditAttack(float attack)
    {
        _playerStatus.Attack = attack;
    }

    public void EditAccuracy(float accuracy)
    {
        _playerStatus.Accuracy = accuracy;
    }

    public void EditEvade(float evade)
    {
        _playerStatus.Evade = evade;
    }

    public void EditGuard(float guard)
    {
        _playerStatus.Guard = guard;
    }

    public void EditStrike(float strike)
    {
        _playerStatus.Strike = strike;
    }

    public void EditSlash(float slash)
    {
        _playerStatus.Slash = slash;
    }

    public void EditPenetration(float penetration)
    {
        _playerStatus.Penetration = penetration;
    }

    public void EditRanged(float ranged)
    {
        _playerStatus.Ranged = ranged;
    }

    public void EditCounter(float counter)
    {
        _playerStatus.Counter = counter; 
    }

    #endregion
}
