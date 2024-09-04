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
    public int _trainDelayTime;
    public bool isUsingItem = false;

    int percentChkMinScore;
    int percentChkMaxScore;

    
    
    private void Awake()
    {
        trainingUnits = this.transform.GetComponentsInChildren<TrainingUnit>();


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
        percentChkMinScore = percentChkMaxScore - trainingUnits[0]._trainingStruct.percentWeight;
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
        Dictionary<string, int> trainResult = new Dictionary<string, int>();
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
                // rate as combo
                var trainingRate =((1+(BuildManager.Instance.gameObject.GetComponent<PlayerBuildProperty>()._trainingRate)*combo));
                var addStatValue = (int)(trainingRate * unit._trainingRate);

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

                // train result

                if (trainResult.ContainsKey(unit._name))
                    trainResult[unit._name] += addStatValue;
                else
                    trainResult.Add(unit._name, addStatValue);
                

                //set new percentage
                var percentageLossRate = (isUsingItem) ? 7 : 3;
                percentChkMinScore -= unit._trainingStruct.percentWeight/ percentageLossRate;
                SetPercentage();

                yield return new WaitForSeconds(_trainDelayTime);
            }
            else
            {   
                failChk = true;
                unit.SetColor(Color.black);
            }
        }

        //training result Text
        if (trainResult.Count != 0)
        {
            var resultText = Instantiate(_comboTextPrefab, this.transform);
            string resultTextInfo = "";
            foreach (KeyValuePair<string, int> kvp in trainResult)
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

    private void trainStat(string statName, int value)
    {
        switch (statName)
        {
            case "Hp":
                GameManager.Instance.EditMaxHp(value);
                break;
            case "Att":
                GameManager.Instance.EditAttack(value);
                break;
            case "Def":
                GameManager.Instance.EditGuard(value);
                break;
            case "Acc":
                GameManager.Instance.EditAccuracy(value);
                break;
            case "Eva":
                GameManager.Instance.EditEvade(value);
                break;
            case "Stk":
                GameManager.Instance.EditStrike(value);
                break;
            case "Sla":
                GameManager.Instance.EditSlash(value);
                break;
            case "Pen":
                GameManager.Instance.EditPenetration(value);
                break;
            case "Ran":
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
