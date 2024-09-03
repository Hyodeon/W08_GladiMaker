using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingStructList : MonoBehaviour
{
    [SerializeField]List<TrainingStruct> list;
    TrainingUnit[] trainingUnits;

    [Header("Variables")]
    public int _percentage;
    public int _trainDelayTime;

    int percentChkMinScore;
    int percentChkMaxScore;
    
    
    private void Start()
    {
        trainingUnits = this.transform.GetComponentsInChildren<TrainingUnit>();
    }

    //Training Unit Color Set
    public void SetTrainingUnits()
    {
        percentChkMinScore = 0;
        percentChkMaxScore = 0;
        foreach (TrainingUnit unit in trainingUnits)
        {
            TrainingStruct trainingStruct = list[UnityEngine.Random.Range(0, list.Count)];
            unit.Set(trainingStruct);
            percentChkMaxScore += 100;
            percentChkMinScore += trainingStruct.percentWeight;
        }
        this._percentage = percentChkMinScore*100/percentChkMaxScore;
    }

    //Train Units 
    [ContextMenu("Train")]
    public void Train()
    {
        StartCoroutine(TrainUnitRoutine(trainingUnits));
    }
    IEnumerator TrainUnitRoutine(TrainingUnit[] units)
    {
        foreach (TrainingUnit unit in units)
        {
            var randomValue = UnityEngine.Random.Range(0, 101);
            if (randomValue <= _percentage)
            {
                Debug.Log("Training Success ! : " + unit._trainingRate);
                yield return new WaitForSeconds(_trainDelayTime);
            }
            else
            {
                Debug.Log("Training Fail=============");
                break;
            }
        }
    }
}
[System.Serializable]
public class TrainingStruct
{
    public Color color;
    public string name;
    public float trainingRate;
    public int percentWeight;
}
