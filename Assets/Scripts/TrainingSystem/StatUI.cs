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
        statTextList[0].text = "ü�� : "+ GameManager.Instance.PlayerStatus.Hp.ToString();
        statTextList[1].text = "���� : " + GameManager.Instance.PlayerStatus.Attack.ToString();
        statTextList[2].text = "��� : " + GameManager.Instance.PlayerStatus.Guard.ToString();
        statTextList[3].text = "���� : " + GameManager.Instance.PlayerStatus.Accuracy.ToString();
        statTextList[4].text = "ȸ�� : " + GameManager.Instance.PlayerStatus.Evade.ToString();

        statTextList[5].text = "���� : " + GameManager.Instance.PlayerStatus.Slash.ToString();
        statTextList[6].text = "Ÿ�� : " + GameManager.Instance.PlayerStatus.Strike.ToString();
        statTextList[7].text = "���� : " + GameManager.Instance.PlayerStatus.Penetration.ToString();
        statTextList[8].text = "��ô : " + GameManager.Instance.PlayerStatus.Ranged.ToString();
    }
}
