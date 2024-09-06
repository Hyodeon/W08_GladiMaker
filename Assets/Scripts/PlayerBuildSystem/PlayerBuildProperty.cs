using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildProperty : MonoBehaviour
{
    public int _money;
    public float _trainingRate;

    private void Awake()
    {
        _money = 0;
        _trainingRate = 0.1f;
    }
}
