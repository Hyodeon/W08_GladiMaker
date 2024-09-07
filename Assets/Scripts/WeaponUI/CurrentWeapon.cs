using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeapon : MonoBehaviour
{
    Skill_Effect_Manager SEM;

    [Header("Æ¼¾î ÄÃ·¯")]
    [SerializeField] Color _bossTier;
    [SerializeField] Color _legendaryTier;
    [SerializeField] Color _epicTier;
    [SerializeField] Color _rareTier;
    [SerializeField] Color _commonTier;

    [Header("UI")]
    public Image weaponIcon;
    public TMP_Text weaponName, weaponGrade, weaponType;
    public TMP_Text weaponStat, weaponDesc, weaponSkillDesc;

    
    void Start()
    {
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("Player") != null);
        SEM = GameObject.FindGameObjectWithTag("Player").GetComponent<Skill_Effect_Manager>();
        Get_Player_Weapon_Data();
    }


    void Get_Player_Weapon_Data()
    {
        var myweapon = SEM.Current_Weapon;

        weaponIcon.sprite = myweapon.GetComponent<SpriteRenderer>().sprite;

        var obj = myweapon.GetComponent<WeaponObj>().weaponStruct;

        weaponName.text = obj._name;
        weaponGrade.text = myweapon.GetComponent<WeaponObj>().weapon.Tier.ToString();

        switch (myweapon.GetComponent<WeaponObj>().weapon.Tier)
        {
            case Utils.WeaponTier.Boss:
                weaponGrade.color = _bossTier;
                weaponGrade.text = "º¸½º ";
                break;
            case Utils.WeaponTier.Legendary:
                weaponGrade.color = _legendaryTier;
                weaponGrade.text = "Àü¼³ ";
                break;
            case Utils.WeaponTier.Epic:
                weaponGrade.color = _epicTier;
                weaponGrade.text = "¿µ¿õ ";
                break;
            case Utils.WeaponTier.Rare:
                weaponGrade.color = _rareTier;
                weaponGrade.text = "Èñ±Í ";
                break;
            case Utils.WeaponTier.Common:
                weaponGrade.color = _commonTier;
                weaponGrade.text = "ÀÏ¹Ý ";
                break;
        }

        weaponType.text = myweapon.GetComponent<WeaponObj>().weapon.Type.ToString();

        weaponStat.text = $"Att + {myweapon.GetComponent<WeaponObj>().weapon.WeaponDamage}";
        weaponDesc.text = obj._skillInfo.ToString();
    }
}
