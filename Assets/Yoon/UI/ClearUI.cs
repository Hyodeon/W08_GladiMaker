using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUI : MonoBehaviour
{
    public void Cube_GO()
    {
        GameObject.Find("Canvas").GetComponent<ShowClearPanel>().SetCubeResult();
    }

    public void TriggerGoldUpdate()
    {
        transform.parent.GetComponent<ShowClearPanel>().StartUpdateGold();
    }
}
