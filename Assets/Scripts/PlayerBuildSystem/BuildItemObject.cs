using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildItemObject : MonoBehaviour
{
    [SerializeField] Image _sprite;
    [SerializeField] TMPro.TMP_Text _name;
    [SerializeField] TMPro.TMP_Text _info;
    [SerializeField] TMPro.TMP_Text _price;

    [Header("ButtonAction Reference")]
    [SerializeField] GameObject _trainingRoom;
    [SerializeField] ItemShop _itemShop;

    public TrainingBar[] _trainingBar;

    public BuildItemStruct _buildItemStruct;

    public void Awake()
    {
        _trainingBar = _trainingRoom.gameObject.GetComponentsInChildren<TrainingBar>();
        _itemShop = this.transform.parent.GetComponent<ItemShop>();
    }

    public void Set(BuildItemStruct buildItem)
    {
        _buildItemStruct = buildItem;
        this._sprite.sprite = buildItem._sprite;
        this._name.text = buildItem._name;
        this._info.text = buildItem._info;
        this._price.text = $"Price : {buildItem._price.ToString()}";
    }

    public void ButtonAction()
    {
        if (BuildManager.Instance._playerBuildProperty._money < _buildItemStruct._price) return;
        BuildManager.Instance._playerBuildProperty._money -= _buildItemStruct._price;
        switch (_buildItemStruct._no)
        {
            case 0:
                foreach (var trainingBar in _trainingBar)
                    trainingBar.SetBar();
                break;
            case 1:
                GameManager.Instance.EditMaxHp(30);
                break;
            case 2:
                GameManager.Instance.EditAttack(30);
                break;
            case 3:
                GameManager.Instance.EditGuard(30);
                break;
            case 4:
                GameManager.Instance.EditAccuracy(30);
                break;
            case 5:
                GameManager.Instance.EditEvade(30);
                break;
            case 6:
                foreach (var trainingBar in _trainingBar)
                    trainingBar._trainingStructList.isUsingItem = true;
                break;
            case 7:
                BuildManager.Instance._playerBuildProperty._trainingRate += 0.1f;
                break;
        }
        _itemShop.Reset();
    }
}
