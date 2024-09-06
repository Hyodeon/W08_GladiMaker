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
        GameManager.Instance.EditStrike(GameManager.Instance.PlayerStatus.Strike*20/100);
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
        GameManager.Instance.EditMaxHp(-GameManager.Instance.PlayerStatus.MaxHp * 10 / 100);
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
        GameManager.Instance.EditAttack(GameManager.Instance.PlayerStatus.Attack * 5 / 100);
        EndTurn();
    }
    // =====================================================================
    public void Event3_Choice0_AttackUp()
    {
        GameManager.Instance.EditAttack(GameManager.Instance.PlayerStatus.Attack * 10 / 100);
        EndTurn();
    }
    public void Event3_Choice1_LossHp()
    {
        GameManager.Instance.EditMaxHp(-GameManager.Instance.PlayerStatus.MaxHp * 10 / 100);
        BuildManager.Instance._playerBuildProperty._money += 7500;
        EndTurn();
    }
    // =====================================================================
    public void Event4_Choice0_HpUp()
    {
        GameManager.Instance.EditMaxHp(GameManager.Instance.PlayerStatus.MaxHp * 10 / 100);
        EndTurn();
    }
    public void Event4_Choice1_EvaUp()
    {
        GameManager.Instance.EditEvade(GameManager.Instance.PlayerStatus.Evade * 10 / 100);
        EndTurn();
    }
    // =====================================================================
    public void Event5_Choice0_HpDown()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            GameManager.Instance.EditEvade(GameManager.Instance.PlayerStatus.Evade * 50 / 100);
        }
        GameManager.Instance.EditMaxHp(-GameManager.Instance.PlayerStatus.MaxHp * 5 / 100);
        EndTurn();
    }
    public void Event5_Choice1_GuardUp()
    {
        GameManager.Instance.EditGuard(GameManager.Instance.PlayerStatus.Guard * 10 / 100);
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
        GameManager.Instance.EditAccuracy(GameManager.Instance.PlayerStatus.Accuracy * 20 / 100);
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
        GameManager.Instance.EditAttack(GameManager.Instance.PlayerStatus.Attack * 5 / 100);
        EndTurn();
    }
    public void Event10_Choice1_AllStat()
    {
        if (UnityEngine.Random.Range(0, 101) >= 5)
        {
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
        GameManager.Instance.EditGuard(GameManager.Instance.PlayerStatus.Guard * 20 / 100);
        EndTurn();
    }
    public void Event11_Choice1_Hp()
    {
        GameManager.Instance.EditMaxHp(GameManager.Instance.PlayerStatus.MaxHp * 20 / 100);
        EndTurn();
    }
    // =====================================================================
    public void Event12_Choice0_Lee()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            GameManager.Instance.EditSlash(GameManager.Instance.PlayerStatus.Slash * 100 / 100);
        }
        EndTurn();
    }
    public void Event12_Choice1_Park()
    {
        if (UnityEngine.Random.Range(0, 101) >= 50)
        {
            GameManager.Instance.EditStrike(GameManager.Instance.PlayerStatus.Strike * 100 / 100);
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
        GameManager.Instance.EditAttack(GameManager.Instance.PlayerStatus.Attack * 20 / 100);
        EndTurn();
    }
}
