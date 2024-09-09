using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingStructList : MonoBehaviour
{
    TrainingUnit[] trainingUnits;
    Player player;

    [Header("Prefabs")]
    [SerializeField] GameObject _comboTextPrefab;

    [Header("Unit Attributes")]
    [SerializeField]List<TrainingStruct> list;
    public string[] _normalStat;
    public string[] _specialStat;

    [Header("Variables")]
    public int _percentage;
    public float _trainDelayTime;
    public float _DelayPlusTime;
    public bool isUsingItem = false;

    int percentChkMinScore;
    int percentChkMaxScore;

    float origin_DelayTime;
    
    private void Awake()
    {
        trainingUnits = this.transform.GetComponentsInChildren<TrainingUnit>();
        origin_DelayTime = _trainDelayTime;

    }

    //Training Unit Color Set
    public void SetTrainingUnits()
    {
        isUsingItem = false;

        percentChkMinScore = 0;
        percentChkMaxScore = 0;

        string specialStatText = _specialStat[Random.Range(0, _specialStat.Length)];
        string normalStatText = _normalStat[Random.Range(0, _normalStat.Length)];


        if (trainingUnits == null) return;
        foreach (TrainingUnit unit in trainingUnits)
        {
            TrainingStruct trainingStruct = list[UnityEngine.Random.Range(0, list.Count)];
            var unitText = !trainingStruct.isSpecial ? normalStatText : specialStatText;
            unit.Set(trainingStruct,unitText);
            percentChkMaxScore += 100;
        }
        percentChkMinScore = percentChkMaxScore - trainingUnits[0]._trainingStruct.percentWeight/3;
        SetPercentage();
    }

    void SetPercentage()
    {
        this._percentage = percentChkMinScore * 100 / percentChkMaxScore;
    }

    //Train Units 
    [ContextMenu("Train")]
    public void Train()
    {
        StartCoroutine(TrainUnitRoutine(trainingUnits));
    }

    IEnumerator TrainUnitRoutine(TrainingUnit[] units)
    {
        _trainDelayTime = origin_DelayTime;
        BuildManager.Instance.ChangeUI(BuildState.isTraining);

        Dictionary<string, float> trainResult = new Dictionary<string, float>();
        int combo = 0;
        float trainingRate = 1;
        bool failChk = false;
        
        foreach (TrainingUnit unit in units)
        {
            var randomValue = UnityEngine.Random.Range(0, 100);
            if (failChk)
            {
                unit.transform.GetChild(1).GetComponent<Animator>().Play("Training_Fail");
                GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("WhyFail");
                GameObject.Find("Flame").GetComponent<Animator>().Play("TrainingFail");
                unit.SetColor(Color.black);
            }
            else if (randomValue <= _percentage)
            {
                // rate as combo
                trainingRate *= ((1+(BuildManager.Instance._playerBuildProperty._trainingRate)));
                trainingRate = (float)System.Math.Round(trainingRate,2);
                var addStatValue = (float)(trainingRate * unit._trainingRate);

                // create Combo Text
                var comboText = Instantiate(_comboTextPrefab,unit.transform);
                comboText.transform.Translate(new Vector3(0,100,0));
                comboText.transform.localScale *= 0.5f+(combo*0.1f);

                combo++;
                if(combo == 10)
                    trainingRate *= 2;
                
                string comboTextInfo = $"Combo {(combo).ToString()}!\nx{trainingRate}(+{addStatValue})";
                comboText.GetComponent<ComboTxt>().Set(comboTextInfo,Color.white);

                // UI
                unit.SetColor(Color.green);
                unit.transform.GetChild(1).GetComponent<Animator>().Play("Training_Success");
                // train result

                if (trainResult.ContainsKey(unit._name))
                    trainResult[unit._name] += addStatValue;
                else
                    trainResult.Add(unit._name, addStatValue);
                

                //set new percentage
                var percentageLossRate = (isUsingItem) ? 0 : unit._trainingStruct.percentWeight / 3;
                percentChkMinScore -= percentageLossRate;
                SetPercentage();

                yield return new WaitForSeconds(_trainDelayTime);
                _trainDelayTime += _DelayPlusTime;
            }
            else
            {   
                failChk = true;
                unit.transform.GetChild(1).GetComponent<Animator>().Play("Training_Fail");
                unit.SetColor(Color.black);
            }
        }

        //training result Text
        if (trainResult.Count != 0)
        {
            var resultText = Instantiate(_comboTextPrefab, this.transform);
            string resultTextInfo = "";
            foreach (KeyValuePair<string, float> kvp in trainResult)
            {
                resultTextInfo = resultTextInfo + $" [{kvp.Key}+{kvp.Value}] ";
                trainStat(kvp.Key, kvp.Value);
            }

            var resultTextComponent = resultText.GetComponent<ComboTxt>();
            resultTextComponent.moveDistance = 100f;
            resultTextComponent.duration = 3f;
            resultTextComponent.Set(resultTextInfo, Color.red);
            resultText.transform.localScale *= 0.75f;

        }
        isUsingItem = false;
        yield return new WaitForSeconds(3f);

        Debug.Log("Stat =============================");
        Debug.Log("HP : "+GameManager.Instance.PlayerStatus.Hp);
        Debug.Log("Att : "+GameManager.Instance.PlayerStatus.Attack);
        Debug.Log("Def : "+GameManager.Instance.PlayerStatus.Guard);
        Debug.Log("Acc : "+GameManager.Instance.PlayerStatus.Accuracy);
        Debug.Log("Eva : "+GameManager.Instance.PlayerStatus.Evade);
        Debug.Log("==================================");

        //Change UI
        if(UnityEngine.Random.Range(0,101)<=50)
            BuildManager.Instance.ChangeUI(BuildState.Event);
        else
            BuildManager.Instance.ChangeUI(BuildState.TurnEnd);
    }

    private void trainStat(string statName, float value)
    {
        switch (statName)
        {
            case "체력":
                GameManager.Instance.EditMaxHp(value);
                break;
            case "공격":
                GameManager.Instance.EditAttack(value);
                break;
            case "방어":
                GameManager.Instance.EditGuard(value);
                break;
            case "명중":
                GameManager.Instance.EditAccuracy(value);
                break;
            case "회피":
                GameManager.Instance.EditEvade(value);
                break;
            case "타격":
                GameManager.Instance.EditStrike(value);
                break;
            case "참격":
                GameManager.Instance.EditSlash(value);
                break;
            case "관통":
                GameManager.Instance.EditPenetration(value);
                break;
            case "투척":
                GameManager.Instance.EditRanged(value);
                break;

        }
    }
}
[System.Serializable]
public class TrainingStruct
{
    public Color color;
    public float trainingRate;
    public int percentWeight;
    public bool isSpecial;
}
