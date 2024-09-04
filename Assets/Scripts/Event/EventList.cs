using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventList : MonoBehaviour
{
    public void EndTurn()
    {
        BuildManager.Instance.ChangeUI(BuildState.TurnEnd);
    }
    // =====================================================================
    public void Event0_Choice0_StrikeUp()
    {
        GameManager.Instance.EditStrike(20);
        EndTurn();
    }
    public void Event0_Choice1_MoneyEarn()
    {
        BuildManager.Instance._playerBuildProperty._money += 5000;
        EndTurn();
    }
    // =====================================================================
    public void Event1_Choice0_AllStat()
    {
        GameManager.Instance.EditMaxHp(10);
        GameManager.Instance.EditAttack(10);
        GameManager.Instance.EditGuard(10);
        GameManager.Instance.EditAccuracy(10);
        GameManager.Instance.EditEvade(10);
        GameManager.Instance.EditSlash(10);
        GameManager.Instance.EditStrike(10);
        GameManager.Instance.EditRanged(10);
        GameManager.Instance.EditPenetration(10);
        EndTurn();
    }
    public void Event1_Choice1_LossHp()
    {
        GameManager.Instance.EditMaxHp(-10);
        EndTurn();
    }
    // =====================================================================
    public void Event2_Choice0_MoneyEarn()
    {
        BuildManager.Instance._playerBuildProperty._money += 3000;
        EndTurn();
    }
    public void Event2_Choice1_AttackUp()
    {
        GameManager.Instance.EditAttack(5);
        EndTurn();
    }
    // =====================================================================
    public void Event3_Choice0_AttackUp()
    {
        GameManager.Instance.EditAttack(10);
        EndTurn();
    }
    public void Event3_Choice1_LossHp()
    {
        GameManager.Instance.EditMaxHp(-10);
        BuildManager.Instance._playerBuildProperty._money += 7500;
        EndTurn();
    }
    // =====================================================================
    public void Event4_Choice0_HpUp()
    {
        GameManager.Instance.EditMaxHp(10);
        EndTurn();
    }
    public void Event4_Choice1_EvaUp()
    {
        GameManager.Instance.EditEvade(10);
        EndTurn();
    }
    // =====================================================================
    public void Event5_Choice0_HpDown()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            GameManager.Instance.EditEvade(50);
        }
        GameManager.Instance.EditMaxHp(-5);
        EndTurn();
    }
    public void Event5_Choice1_GuardUp()
    {
        GameManager.Instance.EditGuard(10);
        EndTurn();
    }
    // =====================================================================
    public void Event6_Choice0_Money()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            BuildManager.Instance._playerBuildProperty._money += 10000;
        }
        EndTurn();
    }
    public void Event6_Choice1_TrainRate()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            BuildManager.Instance._playerBuildProperty._trainingRate += 0.1f;
        }
        EndTurn();
    }
    // =====================================================================
    public void Event7_Choice0_Money()
    {
        int totalMoney = BuildManager.Instance._playerBuildProperty._money;
        BuildManager.Instance._playerBuildProperty._money = 0;
        GameManager.Instance.EditRanged(totalMoney/300);


        EndTurn();
    }
    public void Event7_Choice1_TrainRate()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            BuildManager.Instance._playerBuildProperty._trainingRate += 0.1f;
        }
        EndTurn();
    }
    // =====================================================================
    public void Event8_Choice0_Attack()
    {
        GameManager.Instance.EditAttack(GameManager.Instance.PlayerStatus.Attack*(0.1f));


        EndTurn();
    }
    public void Event8_Choice1_Acc()
    {
        GameManager.Instance.EditAccuracy(20);
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
        GameManager.Instance.EditAttack(5);
        EndTurn();
    }
    public void Event10_Choice1_AllStat()
    {
        if (UnityEngine.Random.Range(0, 101) >= 5)
        {
            GameManager.Instance.EditMaxHp(50);
            GameManager.Instance.EditAttack(50);
            GameManager.Instance.EditGuard(50);
            GameManager.Instance.EditAccuracy(50);
            GameManager.Instance.EditEvade(50);
            GameManager.Instance.EditSlash(50);
            GameManager.Instance.EditStrike(50);
            GameManager.Instance.EditRanged(50);
            GameManager.Instance.EditPenetration(50);
        }
        EndTurn();
    }
    // =====================================================================
    public void Event11_Choice0_Guard()
    {
        GameManager.Instance.EditGuard(30);
        EndTurn();
    }
    public void Event11_Choice1_Hp()
    {
        GameManager.Instance.EditMaxHp(30);
        EndTurn();
    }
    // =====================================================================
    public void Event12_Choice0_Lee()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            GameManager.Instance.EditSlash(100);
        }
        EndTurn();
    }
    public void Event12_Choice1_Park()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            GameManager.Instance.EditStrike(100);
        }
        EndTurn();
    }
    // =====================================================================
    public void Event13_Choice0_Gamble()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            BuildManager.Instance._playerBuildProperty._money += 50000;
        }
        else
        {
            GameManager.Instance.EditMaxHp(-GameManager.Instance.PlayerStatus.MaxHp/2);
        }
        EndTurn();
    }
    // =====================================================================
    public void Event14_Choice0_Hp()
    {
        GameManager.Instance.EditMaxHp(GameManager.Instance.PlayerStatus.MaxHp * (0.1f));
        EndTurn();
    }
    public void Event14_Choice1_Att()
    {
        GameManager.Instance.EditAttack(20);
        EndTurn();
    }
}
