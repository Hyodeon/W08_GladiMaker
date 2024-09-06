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

        statTextList[0].text = "ü�� : " + System.Math.Round(GameManager.Instance.PlayerStatus.MaxHp, 1).ToString();
        statTextList[1].text = "���� : " + System.Math.Round(GameManager.Instance.PlayerStatus.Attack, 1).ToString();
        statTextList[2].text = "��� : " + System.Math.Round(GameManager.Instance.PlayerStatus.Guard, 1).ToString();
        statTextList[3].text = "���� : " + System.Math.Round(GameManager.Instance.PlayerStatus.Accuracy, 1).ToString();
        statTextList[4].text = "ȸ�� : " + System.Math.Round(GameManager.Instance.PlayerStatus.Evade, 1).ToString();

        statTextList[5].text = "���� : " + System.Math.Round(GameManager.Instance.PlayerStatus.Slash, 1).ToString();
        statTextList[6].text = "Ÿ�� : " + System.Math.Round(GameManager.Instance.PlayerStatus.Strike, 1).ToString();
        statTextList[7].text = "���� : " + System.Math.Round(GameManager.Instance.PlayerStatus.Penetration, 1).ToString();
        statTextList[8].text = "��ô : " + System.Math.Round(GameManager.Instance.PlayerStatus.Ranged, 1).ToString();
    }
}
