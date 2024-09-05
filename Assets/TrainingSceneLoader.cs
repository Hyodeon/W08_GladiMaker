using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingSceneLoader : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.Initialize();
    }

}
