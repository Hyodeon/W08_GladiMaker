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

        statTextList[0].text = "체력 : " + System.Math.Round(GameManager.Instance.PlayerStatus.MaxHp, 1).ToString();
        statTextList[1].text = "공격 : " + System.Math.Round(GameManager.Instance.PlayerStatus.Attack, 1).ToString();
        statTextList[2].text = "방어 : " + System.Math.Round(GameManager.Instance.PlayerStatus.Guard, 1).ToString();
        statTextList[3].text = "명중 : " + System.Math.Round(GameManager.Instance.PlayerStatus.Accuracy, 1).ToString();
        statTextList[4].text = "회피 : " + System.Math.Round(GameManager.Instance.PlayerStatus.Evade, 1).ToString();

        statTextList[5].text = "참격 : " + System.Math.Round(GameManager.Instance.PlayerStatus.Slash, 1).ToString();
        statTextList[6].text = "타격 : " + System.Math.Round(GameManager.Instance.PlayerStatus.Strike, 1).ToString();
        statTextList[7].text = "관통 : " + System.Math.Round(GameManager.Instance.PlayerStatus.Penetration, 1).ToString();
        statTextList[8].text = "투척 : " + System.Math.Round(GameManager.Instance.PlayerStatus.Ranged, 1).ToString();
    }
}
