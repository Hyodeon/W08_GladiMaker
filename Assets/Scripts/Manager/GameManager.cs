using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ActorBase _player;
    [SerializeField] ActorBase _monster;

    private void Awake()
    {
        BattleManager.Instance.InitializeActor(
            _player, _monster);
    }
}
