using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBar : MonoBehaviour
{
    [SerializeField] TrainingStructList _trainingStructList;
    [SerializeField] TMPro.TMP_Text _percentText;

    private void Start()
    {
    }

    [ContextMenu("SetBar")]
    void SetBar()
    {
        _trainingStructList.SetTrainingUnits();
        this._percentText.text = $"Percent : {_trainingStructList._percentage}%";
    }
}
