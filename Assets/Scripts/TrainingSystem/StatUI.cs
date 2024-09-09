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
        statTextList[0].text = "<color=yellow>ü��</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.MaxHp, 1).ToString();
        statTextList[1].text = "<color=yellow>����</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Attack, 1).ToString();
        statTextList[2].text = "<color=yellow>���</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Guard, 1).ToString();
        statTextList[3].text = "<color=yellow>����</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Accuracy, 1).ToString();
        statTextList[4].text = "<color=yellow>ȸ��</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Evade, 1).ToString();

        statTextList[5].text = "<color=yellow>����</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Slash, 1).ToString();
        statTextList[6].text = "<color=yellow>Ÿ��</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Strike, 1).ToString();
        statTextList[7].text = "<color=yellow>����</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Penetration, 1).ToString();
        statTextList[8].text = "<color=yellow>��ô</color> : " + System.Math.Round(GameManager.Instance.PlayerStatus.Ranged, 1).ToString();
    }
}
