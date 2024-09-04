using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TrainingUnit : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text _text;
    Image _image;
    public TrainingStruct _trainingStruct;
    public string _name;
    

    // Training Rate of Unit
    public float _trainingRate => _trainingStruct.trainingRate;

    private void Awake()
    {
        _image = GetComponent<Image>(); 
    }

    public void Set(TrainingStruct trainingStruct,string txt)
    {
        _trainingStruct = trainingStruct;
        _name = txt;
        _text.text = txt;
        _image.color = _trainingStruct.color;
    }
    public void SetColor(Color color)
    {
        _image.color = color;
    }
}

