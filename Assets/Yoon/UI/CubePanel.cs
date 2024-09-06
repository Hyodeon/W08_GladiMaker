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
        //일반
        if (rand <= 33)
        {
            Grade_Result.color = Grade_Colors[0];
            Result_Text.color = Grade_Colors[0];
            Result_Text.text = "일반";
        }
        else if(rand <= 66)
        {
            Grade_Result.color = Grade_Colors[1];
            Result_Text.color = Grade_Colors[1];
            Result_Text.text = "에픽";
        }
        else
        {
            Grade_Result.color = Grade_Colors[2];
            Result_Text.color = Grade_Colors[2];
            Result_Text.text = "레전드리";
        }


        GetComponent<Animator>().Play("Cube_Result");

    }
}
