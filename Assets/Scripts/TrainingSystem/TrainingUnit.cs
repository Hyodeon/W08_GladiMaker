using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TrainingUnit : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text _text;
    Image _image;
    TrainingStruct _trainingStruct;

    // Training Rate of Unit
    public float _trainingRate => _trainingStruct.trainingRate;

    private void Start()
    {
        _image = GetComponent<Image>(); 
    }

    public void Set(TrainingStruct trainingStruct)
    {
        _trainingStruct = trainingStruct;
        _image.color = _trainingStruct.color;
    }
}

