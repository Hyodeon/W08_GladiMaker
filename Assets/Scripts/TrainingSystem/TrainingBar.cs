using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingBar : MonoBehaviour
{
    public TrainingStructList _trainingStructList;

    [SerializeField] TMPro.TMP_Text _percentText;

    private void Start()
    {
        SetBar();
    }
    private void OnEnable()
    {
        SetBar();
        this.GetComponentInChildren<Button>().enabled = true;
    }

    [ContextMenu("SetBar")]
    public void SetBar()
    {
        _trainingStructList.SetTrainingUnits();
    }

    private void FixedUpdate()
    {
        this._percentText.text = $"¼º°ø  È®·ü  : {_trainingStructList._percentage}%";
    }
}
