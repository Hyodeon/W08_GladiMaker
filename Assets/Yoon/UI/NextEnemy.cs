using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;

public class NextEnemy : MonoBehaviour
{
    [SerializeField] TMP_Text accuracy_Text;
    [SerializeField] TMP_Text Evade_Text;

    public void AlertNextEnemy(List<StageInfo> stages, int stageIdx)
    {
        accuracy_Text.text = $"���� : �˱� ��ƴ�.";
        Evade_Text.text = $"ȸ�� : �� �𸣰ڴ�.";
    }

}
