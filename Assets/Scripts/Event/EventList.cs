using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EventList : MonoBehaviour
{
    [SerializeField] GameObject _comboTextPrefab;

    [ContextMenu("Test")]
    void Test()
    {
        StatUp_HP(1000);
    }


    void EventResultUI(string text)
    {

        // create Combo Text
        var comboText = Instantiate(_comboTextPrefab, this.transform.parent.parent.transform);
        comboText.GetComponent<TMPro.TMP_Text>().fontSize = 150;
        comboText.transform.Translate(new Vector3(0, 0, 0));
        comboText.GetComponent<ComboTxt>().duration = 2f;
        comboText.GetComponent<ComboTxt>().Set(text,Color.red);
    }
    void EarnMoney(int value)
    {
        EventResultUI($"돈 +{value}");
        BuildManager.Instance._playerBuildProperty._money += 5000;
    }
    //========================================================================
    void StatUp_HP(int per)
    {
        string plusMinus = per >= 0 ? "+" : "-";
        float value = (float)System.Math.Round(GameManager.Instance.PlayerStatus.MaxHp * per / 100, 0);
        EventResultUI($"체력 {plusMinus}{value}");
        GameManager.Instance.EditMaxHp(GameManager.Instance.PlayerStatus.MaxHp * per / 100);
    }
    void StatUp_Att(int per)
    {
        string plusMinus = per >= 0 ? "+" : "-";
        float value = (float)System.Math.Round(GameManager.Instance.PlayerStatus.Attack * per / 100, 0);
        EventResultUI($"공격 {plusMinus}{value}");
        GameManager.Instance.EditAttack(GameManager.Instance.PlayerStatus.Attack * per / 100);
    }
    void StatUp_Def(int per)
    {
        string plusMinus = per >= 0 ? "+" : "-";
        float value = (float)System.Math.Round(GameManager.Instance.PlayerStatus.Guard * per / 100, 0);
        EventResultUI($"방어 {plusMinus}{value}");
        GameManager.Instance.EditGuard(GameManager.Instance.PlayerStatus.Guard * per / 100);
    }
    void StatUp_Acc(int per)
    {
        string plusMinus = per >= 0 ? "+" : "-";
        float value = (float)System.Math.Round(GameManager.Instance.PlayerStatus.Accuracy * per / 100, 0);
        EventResultUI($"명중 {plusMinus}{value}");
        GameManager.Instance.EditAccuracy(GameManager.Instance.PlayerStatus.Accuracy * per / 100);
    }
    void StatUp_Eva(int per)
    {
        string plusMinus = per >= 0 ? "+" : "-";
        float value = (float)System.Math.Round(GameManager.Instance.PlayerStatus.Evade * per / 100, 0);
        EventResultUI($"회피 {plusMinus}{value}");
        GameManager.Instance.EditEvade(GameManager.Instance.PlayerStatus.Evade * per / 100);
    }
    //===========================================================================
    void StatUp_Slash(int per)
    {
        string plusMinus = per >= 0 ? "+" : "-";
        float value = (float)System.Math.Round(GameManager.Instance.PlayerStatus.Slash * per / 100, 0);
        EventResultUI($"참격 {plusMinus}{value}");
        GameManager.Instance.EditSlash(GameManager.Instance.PlayerStatus.Slash * per / 100);
    }
    void StatUp_Strike(int per)
    {
        string plusMinus = per >= 0 ? "+" : "-";
        float value = (float)System.Math.Round(GameManager.Instance.PlayerStatus.Strike * per / 100, 0);
        EventResultUI($"타격 {plusMinus}{value}");
        GameManager.Instance.EditStrike(GameManager.Instance.PlayerStatus.Strike * per / 100);
    }
    void StatUp_Penetrate(int per)
    {
        string plusMinus = per >= 0 ? "+" : "-";
        float value = (float)System.Math.Round(GameManager.Instance.PlayerStatus.Penetration * per / 100, 0);
        EventResultUI($"관통 {plusMinus}{value}");
        GameManager.Instance.EditPenetration(GameManager.Instance.PlayerStatus.Penetration * per / 100);
    }
    void StatUp_Range(int per)
    {
        string plusMinus = per >= 0 ? "+" : "-";
        float value = (float)System.Math.Round(GameManager.Instance.PlayerStatus.Ranged * per / 100, 0);
        EventResultUI($"명중 {plusMinus}{value}");
        GameManager.Instance.EditRanged(GameManager.Instance.PlayerStatus.Ranged * per / 100);
    }
    //===================================================================================
    public void EndTurn()
    {
        BuildManager.Instance.ChangeUI(BuildState.TurnEnd);
    }
    // =====================================================================
    public void Event0_Choice0_StrikeUp()
    {
        StatUp_Strike(20);
        EndTurn();
    }
    public void Event0_Choice1_MoneyEarn()
    {
        EarnMoney(5000);
        EndTurn();
    }
    // =====================================================================
    public void Event1_Choice0_AllStat()
    {
        EventResultUI($"전스탯 +10%");
        GameManager.Instance.EditMaxHp(GameManager.Instance.PlayerStatus.MaxHp * 10 / 100);
        GameManager.Instance.EditAttack(GameManager.Instance.PlayerStatus.Attack * 10 / 100);
        GameManager.Instance.EditGuard(GameManager.Instance.PlayerStatus.Guard * 10 / 100);
        GameManager.Instance.EditAccuracy(GameManager.Instance.PlayerStatus.Accuracy * 10 / 100);
        GameManager.Instance.EditEvade(GameManager.Instance.PlayerStatus.Evade * 10 / 100);
        GameManager.Instance.EditSlash(GameManager.Instance.PlayerStatus.Slash * 10 / 100);
        GameManager.Instance.EditStrike(GameManager.Instance.PlayerStatus.Strike * 10 / 100);
        GameManager.Instance.EditRanged(GameManager.Instance.PlayerStatus.Ranged * 10 / 100);
        GameManager.Instance.EditPenetration(GameManager.Instance.PlayerStatus.Penetration * 10 / 100);
        EndTurn();
    }
    public void Event1_Choice1_LossHp()
    {
        StatUp_HP(-10);
        EndTurn();
    }
    // =====================================================================
    public void Event2_Choice0_MoneyEarn()
    {
        EarnMoney(3000);
        EndTurn();
    }
    public void Event2_Choice1_AttackUp()
    {
        StatUp_Att(5);
        EndTurn();
    }
    // =====================================================================
    public void Event3_Choice0_AttackUp()
    {
        StatUp_Att(10);
        EndTurn();
    }
    public void Event3_Choice1_LossHp()
    {
        StatUp_HP(-10);
        BuildManager.Instance._playerBuildProperty._money += 7500;
        EndTurn();
    }
    // =====================================================================
    public void Event4_Choice0_HpUp()
    {
        StatUp_HP(10);
        EndTurn();
    }
    public void Event4_Choice1_EvaUp()
    {
        StatUp_Eva(10);
        EndTurn();
    }
    // =====================================================================
    public void Event5_Choice0_HpDown()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            StatUp_Eva(50);
        }
        GameManager.Instance.EditMaxHp(-GameManager.Instance.PlayerStatus.MaxHp * 5 / 100);
        EndTurn();
    }
    public void Event5_Choice1_GuardUp()
    {
        StatUp_Def(10);
        EndTurn();
    }
    // =====================================================================
    public void Event6_Choice0_Money()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            EarnMoney(10000);
        }
        EndTurn();
    }
    public void Event6_Choice1_TrainRate()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            EventResultUI("훈련 배율 + 0.1");
            BuildManager.Instance._playerBuildProperty._trainingRate += 0.1f;
        }
        EndTurn();
    }
    // =====================================================================
    public void Event7_Choice0_Money()
    {
        float totalMoney = BuildManager.Instance._playerBuildProperty._money;
        BuildManager.Instance._playerBuildProperty._money = 0;
        StatUp_Range((int)totalMoney / 300);


        EndTurn();
    }
    public void Event7_Choice1_TrainRate()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            EventResultUI("훈련 배율 + 0.1");
            BuildManager.Instance._playerBuildProperty._trainingRate += 0.1f;
        }
        EndTurn();
    }
    // =====================================================================
    public void Event8_Choice0_Attack()
    {
        StatUp_Att(20);
        EndTurn();
    }
    public void Event8_Choice1_Acc()
    {
        StatUp_Acc(20);
        EndTurn();
    }
    // =====================================================================
    public void Event9_Choice0()
    {
        EndTurn();
    }
    // =====================================================================
    public void Event10_Choice0_AttackUp()
    {
        StatUp_Att(5);
        EndTurn();
    }
    public void Event10_Choice1_AllStat()
    {
        if (UnityEngine.Random.Range(0, 101) <= 5)
        {
            EventResultUI($"전스탯 +50%");
            GameManager.Instance.EditMaxHp(GameManager.Instance.PlayerStatus.MaxHp * 50 / 100);
            GameManager.Instance.EditAttack(GameManager.Instance.PlayerStatus.Attack * 50 / 100);
            GameManager.Instance.EditGuard(GameManager.Instance.PlayerStatus.Guard * 50 / 100);
            GameManager.Instance.EditAccuracy(GameManager.Instance.PlayerStatus.Accuracy * 50 / 100);
            GameManager.Instance.EditEvade(GameManager.Instance.PlayerStatus.Evade * 50 / 100);
            GameManager.Instance.EditSlash(GameManager.Instance.PlayerStatus.Slash * 50 / 100);
            GameManager.Instance.EditStrike(GameManager.Instance.PlayerStatus.Strike * 50 / 100);
            GameManager.Instance.EditRanged(GameManager.Instance.PlayerStatus.Ranged * 50 / 100);
            GameManager.Instance.EditPenetration(50);
        }
        EndTurn();
    }
    // =====================================================================
    public void Event11_Choice0_Guard()
    {
        StatUp_Def(30);
        EndTurn();
    }
    public void Event11_Choice1_Hp()
    {
        StatUp_HP(30);
        EndTurn();
    }
    // =====================================================================
    public void Event12_Choice0_Lee()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            StatUp_Slash(100);
        }
        EndTurn();
    }
    public void Event12_Choice1_Park()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            StatUp_Strike(100);
        }
        EndTurn();
    }
    // =====================================================================
    public void Event13_Choice0_Gamble()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            EarnMoney(50000);
        }
        else
        {
            StatUp_HP(-50);
        }
        EndTurn();
    }
    // =====================================================================
    public void Event14_Choice0_Hp()
    {
        StatUp_HP(10);
        EndTurn();
    }
    public void Event14_Choice1_Att()
    {
        StatUp_Att(20);
        EndTurn();
    }
}
