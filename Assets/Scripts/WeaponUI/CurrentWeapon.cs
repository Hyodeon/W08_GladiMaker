using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeapon : MonoBehaviour
{
    Skill_Effect_Manager SEM;

    [Header("Ƽ�� �÷�")]
    [SerializeField] Color _bossTier;
    [SerializeField] Color _legendaryTier;
    [SerializeField] Color _epicTier;
    [SerializeField] Color _rareTier;
    [SerializeField] Color _commonTier;

    [Header("UI")]
    public Image weaponIcon;
    public TMP_Text weaponName, weaponGrade, weaponType;
    public TMP_Text weaponStat, weaponDesc, weaponSkillDesc;

    [Header("ť��")]
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
                Cube_Text[idx].text = "�����帮 ";
                break;
            case WeaponCubeTier.Epic:
                Cube_Text[idx].color = Color.green;
                Cube_Text[idx].text = "���� ";
                break;
            case WeaponCubeTier.Common:
                Cube_Text[idx].color = Color.grey;
                Cube_Text[idx].text = "�Ϲ� ";
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
        weaponGrade.text = myweapon.GetComponent<WeaponObj>().weapon.Tier.ToString();

        switch (myweapon.GetComponent<WeaponObj>().weapon.Tier)
        {
            case Utils.WeaponTier.Boss:
                weaponGrade.color = _bossTier;
                weaponGrade.text = "���� ";
                break;
            case Utils.WeaponTier.Legendary:
                weaponGrade.color = _legendaryTier;
                weaponGrade.text = "���� ";
                break;
            case Utils.WeaponTier.Epic:
                weaponGrade.color = _epicTier;
                weaponGrade.text = "���� ";
                break;
            case Utils.WeaponTier.Rare:
                weaponGrade.color = _rareTier;
                weaponGrade.text = "��� ";
                break;
            case Utils.WeaponTier.Common:
                weaponGrade.color = _commonTier;
                weaponGrade.text = "�Ϲ� ";
                break;
        }

        for( int i = 0; i < Cube_Text.Length; i++)
        {
            SetMyColor(i);
            Cube_Text[i].text += $"{myweapon.GetComponent<WeaponObj>().weaponStruct._cubeOption[i]._stat} {myweapon.GetComponent<WeaponObj>().weaponStruct._cubeOption[i]._option}%";
        }


        weaponType.text = myweapon.GetComponent<WeaponObj>().weapon.Type.ToString();

        weaponStat.text = $"Att + {myweapon.GetComponent<WeaponObj>().weapon.WeaponDamage}";
        weaponDesc.text = obj._skillInfo.ToString();
    }
}
