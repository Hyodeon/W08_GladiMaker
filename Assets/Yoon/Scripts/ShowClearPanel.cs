using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowClearPanel : MonoBehaviour
{
    public GameObject ClearPanel;

    public float gold, attackRate, overKillRate, LowHPRate, CriticalRate;

    [SerializeField] TMP_Text Gold;
    [SerializeField] TMP_Text Bonus1;
    [SerializeField] TMP_Text Bonus2;
    [SerializeField] TMP_Text Bonus3;
    [SerializeField] TMP_Text Bonus4;
    [SerializeField] WeaponObj weapon;


    [Header("Àü¸®Ç°")]
    [SerializeField] Image Icon;
    [SerializeField] TMP_Text WeaponName;
    [SerializeField] TMP_Text WeaponGradeType;
    [SerializeField] TMP_Text WeaponStat;
    [SerializeField] TMP_Text WeaponInfo;


    public void ConnectUI(float gol, float atk, float ok, float lo, float crit, WeaponObj weap)
    {
        ClearPanel.SetActive(true);
        gold = gol;
        attackRate = atk;
        overKillRate = ok;
        LowHPRate = lo;
        CriticalRate = crit;
        weapon = weap;

        Gold.text = $"X{gold}";
        Bonus1.text = $"X{atk}";
        Bonus2.text = $"X{ok}";
        Bonus3.text = $"X{lo}";
        Bonus4.text = $"X{crit}";

        Icon.sprite = weap.GetComponent<SpriteRenderer>().sprite;
        WeaponName.text = "<color=black>" + weap.weaponStruct._name;
        WeaponGradeType.text = $"<color=red>{weap.weapon.Tier} | {weap.weapon.Type}";
        WeaponStat.text = "<color=black>" + weap.weaponStruct._skillName;
        WeaponInfo.text = "<color=orange>" + weap.weaponStruct._skillInfo;
    }
}
