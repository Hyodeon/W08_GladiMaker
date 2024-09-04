using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Serialization;
using DG.Tweening.Core.Easing;
using TMPro;

public enum BuildState{ Event, Train, Battle, TurnEnd}
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

    GameObject[] canvasChildrens;
    public PlayerBuildProperty _playerBuildProperty;

    [Header("Attributes")]
    [SerializeField] int leftTurn;

    BuildState currentState;

    private void Awake()
    {
        if (_instance == null) 
        {
            _instance = this;
         }
        
        canvasChildrens = CharacterBuildObj.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
        _playerBuildProperty = this.GetComponent<PlayerBuildProperty>();
    }
    private void Start()
    {
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
                EnableUI(TrainUIObj);   
                break;
            case BuildState.Battle:
                break;
            case BuildState.TurnEnd:
                leftTurn--;
                ChangeUI(BuildState.Train);
                break;
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

    void TextUpdate()
    {
        LeftTurnUIObj.GetComponentInChildren<TMP_Text>().text = $"Left Turn : {leftTurn.ToString()}";
        BuildingStatUIObj.GetComponentInChildren<TMP_Text>().text = "" +
            $"Money : {_playerBuildProperty._money}\n" +
            $"ComboRate : {_playerBuildProperty._trainingRate}" +
            "";
    }
}
