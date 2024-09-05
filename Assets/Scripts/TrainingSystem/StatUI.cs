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
        statTextList[0].text = "체력 : "+ GameManager.Instance.PlayerStatus.Hp.ToString();
        statTextList[1].text = "공격 : " + GameManager.Instance.PlayerStatus.Attack.ToString();
        statTextList[2].text = "방어 : " + GameManager.Instance.PlayerStatus.Guard.ToString();
        statTextList[3].text = "명중 : " + GameManager.Instance.PlayerStatus.Accuracy.ToString();
        statTextList[4].text = "회피 : " + GameManager.Instance.PlayerStatus.Evade.ToString();

        statTextList[5].text = "참격 : " + GameManager.Instance.PlayerStatus.Slash.ToString();
        statTextList[6].text = "타격 : " + GameManager.Instance.PlayerStatus.Strike.ToString();
        statTextList[7].text = "관통 : " + GameManager.Instance.PlayerStatus.Penetration.ToString();
        statTextList[8].text = "투척 : " + GameManager.Instance.PlayerStatus.Ranged.ToString();
    }
}
