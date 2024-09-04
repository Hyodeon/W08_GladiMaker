using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingStructList : MonoBehaviour
{
    [SerializeField] GameObject _comboTextPrefab;
    [SerializeField]List<TrainingStruct> list;
    TrainingUnit[] trainingUnits;

    [Header("Variables")]
    public int _percentage;
    public int _trainDelayTime;

    int percentChkMinScore;
    int percentChkMaxScore;
    
    
    private void Awake()
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
        int combo = 0;
        bool failChk = false;
        foreach (TrainingUnit unit in units)
        {
            var randomValue = UnityEngine.Random.Range(0, 101);
            if (failChk)
            {
                unit.SetColor(Color.black);
            }
            else if (randomValue <= _percentage)
            {
                // create Combo Text
                var comboText = Instantiate(_comboTextPrefab,unit.transform);
                comboText.transform.Translate(new Vector3(0,30,0));
                comboText.transform.localScale *= 0.5f+(combo*0.15f);
                comboText.GetComponent<ComboTxt>().Set(++combo);

                unit.SetColor(Color.green);
                Debug.Log("Training Success ! : " + unit._trainingRate);
                yield return new WaitForSeconds(_trainDelayTime);
            }
            else
            {
                failChk = true;
                unit.SetColor(Color.black);
                Debug.Log("Training Fail=============");
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
