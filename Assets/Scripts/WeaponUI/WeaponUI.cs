using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [Header("Text Color")]
    [SerializeField] Color _bossTier;
    [SerializeField] Color _legendaryTier;
    [SerializeField] Color _epicTier;
    [SerializeField] Color _rareTier;
    [SerializeField] Color _commonTier;

    [Header("Reference")]
    [SerializeField] Image _image;
    [SerializeField] TMPro.TMP_Text _name;
    [SerializeField] TMPro.TMP_Text _tier;
    [SerializeField] TMPro.TMP_Text _info;

    public void Set(GameObject weapon)
    {
        WeaponObj weaponObj = weapon.GetComponent<WeaponObj>();
        SpriteRenderer sr = weaponObj.GetComponent<SpriteRenderer>();

        _name.text = weaponObj.weaponStruct._name;
        switch (weaponObj.weapon.Tier)
        {
            case Utils.WeaponTier.Boss:
                _tier.color = _bossTier;
                _tier.text = "���� ";
                break;
            case Utils.WeaponTier.Legendary:
                _tier.color = _legendaryTier;
                _tier.text = "���� ";
                break;
            case Utils.WeaponTier.Epic:
                _tier.color = _epicTier;
                _tier.text = "���� ";
                break;
            case Utils.WeaponTier.Rare:
                _tier.color = _rareTier;
                _tier.text = "��� ";
                break;
            case Utils.WeaponTier.Common:
                _tier.color = _commonTier;
                _tier.text = "�Ϲ� ";
                break;
        }
        switch (weaponObj.weapon.Type)
        {
            case Utils.WeaponAttackType.Strike:
                _tier.text = _tier.text + "Ÿ�� ����";
                break;
            case Utils.WeaponAttackType.Slash:
                _tier.text = _tier.text + "���� ����";
                break;
            case Utils.WeaponAttackType.Penetration:
                _tier.text = _tier.text + "���� ����";
                break;
            case Utils.WeaponAttackType.Ranged:
                _tier.text = _tier.text + "��ô ����";
                break;

        }
                    

        _image.sprite = sr.sprite;
        _info.text = $"" +
            $"Attack +{weaponObj.weapon.WeaponDamage}\n\n" +
            $"{weaponObj.weaponStruct._skillInfo}"; //+
            //$"<color=red>Radom Option ================</color>\n\n";
    }
}