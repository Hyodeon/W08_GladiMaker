using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Battle_Load_UI : MonoBehaviour
{

    public Image my_Enemy;
    public TMP_Text Enemy_Name;

    private void Awake() => DontDestroyOnLoad(gameObject);

    public void StartLoading() => GetComponent<Animator>().Play("Battle_Start");


    public void Get_Enemy_Info()
    {
        my_Enemy.sprite = GameManager.Instance.Stages[GameManager.Instance.CurrentStage].EnemyPrefab.GetComponent<ActorBase>().EnemyAvatar;
        Enemy_Name.text = GameManager.Instance.Stages[GameManager.Instance.CurrentStage].EnemyPrefab.GetComponent<ActorBase>().Name;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("BattleScene");

        /*
        if (GameManager.Instance.CurrentStage == 0)
        {
            GameManager.Instance.InitializeBattle_OneTime();
        }
        else
        {
            GameManager.Instance.InitializeBattle();
        }
        */
        GetComponent<Animator>().Play("Battle_Loading_End");
    }
}
