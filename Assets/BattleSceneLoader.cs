using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneLoader : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance.CurrentStage == 0)
        {
            Debug.Log("Ã¹¹øÂ° ¾À ·Îµå");
            GameManager.Instance.InitializeBattle_OneTime();
        }
        else
        {
            Debug.Log($"{GameManager.Instance.CurrentStage}Â° ¾À ·Îµå");
            GameManager.Instance.InitializeBattle();
        }
    }
}
