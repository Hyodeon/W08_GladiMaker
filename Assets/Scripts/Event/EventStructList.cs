using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class EventStructList : MonoBehaviour
{
    [SerializeField] List<EventStruct> list;
    [SerializeField] GameObject _choiceBtnPrefab;

    [Header("===")]
    [SerializeField] Image _npcImage;
    [SerializeField] TMP_Text _titleText;
    [SerializeField] TMP_Text _infoText;
    [SerializeField] GameObject _gridLayout;


    public void Set(EventStruct eventStruct)
    {
        //Reset
        foreach (Transform child in _gridLayout.transform)
            Destroy(child.gameObject);


        this._npcImage.sprite = eventStruct._npcSprite;
        this._titleText.text = eventStruct._title;
        this._infoText.text = eventStruct._info;

        //Choice Btn Set
        for(int i=0;i<eventStruct.choices.Count;i++)
        {
            var _btn = Instantiate(_choiceBtnPrefab, _gridLayout.transform);
            _btn.GetComponentInChildren<TMP_Text>().text = eventStruct.choicesInfo[i];
            int index = i;
            _btn.GetComponent<Button>().onClick.AddListener(() => { 
                eventStruct.choices[index].Invoke();
            });
        }
    }

}
[System.Serializable]
public struct EventStruct
{
    public Sprite _npcSprite;
    public string _title;
    public string _info;
    public List<UnityEvent> choices;
    public List<string> choicesInfo;
}