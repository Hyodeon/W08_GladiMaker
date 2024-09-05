using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneLoader : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance.CurrentStage == 0)
        {
            Debug.Log("ù��° �� �ε�");
            GameManager.Instance.InitializeBattle_OneTime();
        }
        else
        {
            Debug.Log($"{GameManager.Instance.CurrentStage}° �� �ε�");
            GameManager.Instance.InitializeBattle();
        }
    }
}
