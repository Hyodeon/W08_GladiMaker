using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class CurrentWeapon : MonoBehaviour
{
    Skill_Effect_Manager SEM;

    [Header("티어 컬러")]
    [SerializeField] Color _bossTier;
    [SerializeField] Color _legendaryTier;
    [SerializeField] Color _epicTier;
    [SerializeField] Color _rareTier;
    [SerializeField] Color _commonTier;

    [Header("UI")]
    public Image weaponIcon;
    public TMP_Text weaponName, weaponGrade, weaponType;
    public TMP_Text weaponStat, weaponDesc, weaponSkillDesc;

    [Header("큐브")]
    public TMP_Text[] Cube_Text; 



    void Start()
    {
        Get_Player_Weapon_Data();
    }


    void SetMyColor(int idx)
    {
        var myweapon = GameManager.Instance.CurrentWeapon;
        if (myweapon.GetComponent<WeaponObj>().weaponStruct._cubeOption.Count <= 0) return;
        switch (myweapon.GetComponent<WeaponObj>().weaponStruct._cubeOption[idx]._tier)
        {
            case WeaponCubeTier.Legendary:
                Cube_Text[idx].color = Color.red;
                Cube_Text[idx].text = "레전드리 ";
                break;
            case WeaponCubeTier.Epic:
                Cube_Text[idx].color = Color.green;
                Cube_Text[idx].text = "에픽 ";
                break;
            case WeaponCubeTier.Common:
                Cube_Text[idx].color = Color.grey;
                Cube_Text[idx].text = "일반 ";
                break;
        }
    }

    void Get_Player_Weapon_Data()
    {
        var myweapon = GameManager.Instance.CurrentWeapon;
        if (myweapon.GetComponent<WeaponObj>().weaponStruct._cubeOption.Count <= 0) return;
        weaponIcon.sprite = myweapon.GetComponent<SpriteRenderer>().sprite;

        var obj = myweapon.GetComponent<WeaponObj>().weaponStruct;

        weaponName.text = obj._name;
        weaponGrade.text = $"[{ConvertWeaponGrade(myweapon.GetComponent<WeaponObj>().weapon.Tier)}]";
        weaponType.text = "<color=white>" + ConvertWeaponType(myweapon.GetComponent<WeaponObj>().weapon.Type);

        switch (myweapon.GetComponent<WeaponObj>().weapon.Tier)
        {
            case Utils.WeaponTier.Boss:
                weaponGrade.color = _bossTier;
                weaponGrade.text = "[보스]";
                break;
            case Utils.WeaponTier.Legendary:
                weaponGrade.color = _legendaryTier;
                weaponGrade.text = "[전설]";
                break;
            case Utils.WeaponTier.Epic:
                weaponGrade.color = _epicTier;
                weaponGrade.text = "[영웅]";
                break;
            case Utils.WeaponTier.Rare:
                weaponGrade.color = _rareTier;
                weaponGrade.text = "[희귀]";
                break;
            case Utils.WeaponTier.Common:
                weaponGrade.color = _commonTier;
                weaponGrade.text = "[일반]";
                break;
        }

        for( int i = 0; i < Cube_Text.Length; i++)
        {
            SetMyColor(i);
            Cube_Text[i].text += $"{myweapon.GetComponent<WeaponObj>().weaponStruct._cubeOption[i]._stat} {myweapon.GetComponent<WeaponObj>().weaponStruct._cubeOption[i]._option}%";
        }

        weaponStat.text = $"공격 {myweapon.GetComponent<WeaponObj>().weapon.WeaponDamage}%";
        weaponDesc.text = "<color=white>" + obj._itemInfo.ToString();
        weaponSkillDesc.text = "<color=white>" + obj._skillInfo.ToString();
    }

    public string ConvertWeaponGrade(WeaponTier tier)
    {
        return tier switch
        {
            WeaponTier.Boss => "보스",
            WeaponTier.Legendary => "전설",
            WeaponTier.Epic => "영웅",
            WeaponTier.Rare => "레어",
            WeaponTier.Common => "일반",
            _ => "뭐야 이건"
        };
    }

    public string ConvertWeaponType(WeaponAttackType type)
    {
        return type switch
        {
            WeaponAttackType.Strike => "타격",
            WeaponAttackType.Slash => "참격",
            WeaponAttackType.Penetration => "관통",
            WeaponAttackType.Ranged => "투척",
            _ => "뭐야 이건"
        };
    }
}
