using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    [SerializeField] List<TMPro.TMP_Text> statTextList;

    private void Update()
    {
        TextUpdate();
    }

    void TextUpdate()
    {
        statTextList[0].text = "<color=yellow>체력</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.MaxHp, 1).ToString();
        statTextList[1].text = "<color=yellow>공격</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Attack, 1).ToString();
        statTextList[2].text = "<color=yellow>방어</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Guard, 1).ToString();
        statTextList[3].text = "<color=yellow>명중</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Accuracy, 1).ToString();
        statTextList[4].text = "<color=yellow>회피</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Evade, 1).ToString();

        statTextList[5].text = "<color=yellow>참격</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Slash, 1).ToString();
        statTextList[6].text = "<color=yellow>타격</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Strike, 1).ToString();
        statTextList[7].text = "<color=yellow>관통</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Penetration, 1).ToString();
        statTextList[8].text = "<color=yellow>투척</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Ranged, 1).ToString();
    }
}
