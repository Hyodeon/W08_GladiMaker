using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowClearPanel : MonoBehaviour
{
    public GameObject ClearPanel;

    public float gold, attackRate, overKillRate, LowHPRate, CriticalRate;

    [Header("적 이름")]
    [SerializeField] TMP_Text EnemyNameText;


    [SerializeField] TMP_Text Gold;
    [SerializeField] TMP_Text Bonus1;
    [SerializeField] TMP_Text Bonus2;
    [SerializeField] TMP_Text Bonus3;
    [SerializeField] TMP_Text Bonus4;
    [SerializeField] WeaponObj weapon;


    [Header("전리품")]
    [SerializeField] Image Icon;
    [SerializeField] TMP_Text WeaponName;
    [SerializeField] TMP_Text WeaponGradeType;
    [SerializeField] TMP_Text WeaponStat;
    [SerializeField] TMP_Text WeaponInfo;

    private void Start()
    {
        StartCoroutine(FindEnemyName());
    }


    IEnumerator FindEnemyName()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("Enemy") != null);
        EnemyNameText.text = GameObject.FindGameObjectWithTag("Enemy").GetComponent<ActorBase>().Name;
    }

    public void ConnectUI(float gol, float atk, float ok, float lo, float crit, WeaponObj weap)
    {
        ClearPanel.SetActive(true);
        gold = gol;
        attackRate = atk;
        overKillRate = ok;
        LowHPRate = lo;
        CriticalRate = crit;
        weapon = weap;

        Gold.text = $"X{gold.ToString("N2")}";
        Bonus1.text = $"X{atk.ToString("N2")}";
        Bonus2.text = $"X{ok.ToString("N2")}";
        Bonus3.text = $"X{lo.ToString("N2")}";
        Bonus4.text = $"X{crit.ToString("N2")}";

        Icon.sprite = weap.GetComponent<SpriteRenderer>().sprite;
        WeaponName.text = "<color=black>" + weap.weaponStruct._name;
        WeaponGradeType.text = $"<color=red>{weap.weapon.Tier} | {weap.weapon.Type}";
        WeaponStat.text = "<color=black>Att +" + weap.weapon.WeaponDamage;
        WeaponInfo.text = "<color=orange>" + weap.weaponStruct._skillInfo;
    }

    public void SwitchScene()
    {
        GameManager.Instance.SwitchScene("TrainingScene");
    }
}
