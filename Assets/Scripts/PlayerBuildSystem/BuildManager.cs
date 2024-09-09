using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Serialization;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;

public enum BuildState{ Event, Train,isTraining, Battle, TurnEnd}
public class BuildManager : MonoBehaviour
{
    private static BuildManager _instance;
    public static BuildManager Instance => _instance;

    [Header("UI OBJ")]
    [SerializeField] GameObject CharacterBuildObj;
    [SerializeField] GameObject TrainUIObj;
    [SerializeField] GameObject EventUIObj;
    [SerializeField] GameObject LeftTurnUIObj;
    [SerializeField] GameObject BuildingStatUIObj;
    [SerializeField] GameObject ItemShopUIObj;

    [Header("TEST")]
    [SerializeField] GameObject WeaponUIPrefab;
    [SerializeField] GameObject WeaponTest;

    GameObject[] canvasChildrens;
    public PlayerBuildProperty _playerBuildProperty;

    [Header("Attributes")]
    public int LeftTurn = 9999;

    BuildState currentState;


    [ContextMenu("test")]
    public void test()
    {
        var testObj = Instantiate(WeaponUIPrefab,BuildingStatUIObj.transform);
        testObj.GetComponent<WeaponUI>().Set(WeaponTest);
    }

    private void Awake()
    {
        if (_instance == null) 
        {
            _instance = this;
         }
        
        canvasChildrens = CharacterBuildObj.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
        _playerBuildProperty = GameManager.Instance.GetComponent<PlayerBuildProperty>();

    }

    private void Start()
    {
        LeftTurn = GameManager.Instance.CurrentStageInfo.nextTurnCount;
        ChangeUI(BuildState.Train);
    }

    private void Update()
    {
        TextUpdate();
    }

    public void ChangeUI(BuildState state)
    {
        TextUpdate();
        currentState = state;
        switch (currentState)
        {
            case BuildState.Event:
                ItemShopUIObj.SetActive(false);
                EnableUI(EventUIObj);
                break;
            case BuildState.Train:
                ItemShopUIObj.SetActive(true);
                EnableTrainButton(true);
                EnableUI(TrainUIObj);   
                break;
            case BuildState.isTraining:
                EnableTrainButton(false);
                break;
            case BuildState.Battle:
                break;
            case BuildState.TurnEnd:
                LeftTurn--;
                ChangeUI(BuildState.Train);
                break;
        }

        if (LeftTurn == 0)
        {
            FindAnyObjectByType<Battle_Load_UI>().StartLoading();
        }
    }

    void EnableUI(GameObject obj)
    {
        foreach (GameObject child in canvasChildrens)
        {
            child.SetActive(false);
        }
        obj.SetActive(true);
    }

    void EnableTrainButton(bool enableBtn)
    {
        var btns = TrainUIObj.GetComponentsInChildren<Button>();
        foreach(var btn in btns)
        {
            btn.enabled = enableBtn;
        }
    }

    void TextUpdate()
    {
        var money = System.Math.Round(_playerBuildProperty._money, 0);
        var trainingRate = System.Math.Round(_playerBuildProperty._trainingRate, 0);
        LeftTurnUIObj.GetComponentInChildren<TMP_Text>().text = $"시합까지  <color=yellow>{LeftTurn.ToString()}</color>일";
        BuildingStatUIObj.GetComponentInChildren<TMP_Text>().text = "" +
            $"소지금 : <color=yellow>{money}</color>\n" +
            $"훈련 효율 : <color=green>{trainingRate}</color>" +
            "";
    }
}
