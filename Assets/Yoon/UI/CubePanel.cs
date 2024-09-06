using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CubePanel : MonoBehaviour
{
    public Color[] Grade_Colors;
    [SerializeField] Image Grade_Result;
    [SerializeField] TMP_Text Result_Text;

    public void SetMyResult()
    {
        int rand = Random.Range(0, 101);
        //�Ϲ�
        if (rand <= 33)
        {
            Grade_Result.color = Grade_Colors[0];
            Result_Text.color = Grade_Colors[0];
            Result_Text.text = "�Ϲ�";
        }
        else if(rand <= 66)
        {
            Grade_Result.color = Grade_Colors[1];
            Result_Text.color = Grade_Colors[1];
            Result_Text.text = "����";
        }
        else
        {
            Grade_Result.color = Grade_Colors[2];
            Result_Text.color = Grade_Colors[2];
            Result_Text.text = "�����帮";
        }


        GetComponent<Animator>().Play("Cube_Result");

    }
}
