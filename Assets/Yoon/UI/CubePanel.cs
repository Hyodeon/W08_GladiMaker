using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CubePanel : MonoBehaviour
{
    public Color[] Grade_Colors;
    [SerializeField] Image Grade_Result;
    [SerializeField] TMP_Text Result_Text;

    WeaponCubeStat GetRandomWeaponCubeStat()
    {
        WeaponCubeStat[] values = (WeaponCubeStat[])System.Enum.GetValues(typeof(WeaponCubeStat));
        int randomIndex = UnityEngine.Random.Range(0, values.Length);
        return values[randomIndex];
    }

    WeaponCubeStruct RandomCubeStruct(WeaponCubeTier tier)
    {
        WeaponCubeStat randomStat = GetRandomWeaponCubeStat();
        WeaponCubeTier weaponCubeTier = tier;
        int option = 0;
        switch (weaponCubeTier)
        {
            case WeaponCubeTier.Common:
                {
                    option = UnityEngine.Random.Range(1, 4);
                    break;
                }
            case WeaponCubeTier.Epic:
                {
                    option = UnityEngine.Random.Range(4, 7);
                    break;
                }
            case WeaponCubeTier.Legendary:
                {
                    option = UnityEngine.Random.Range(7, 10);
                    break;
                }
        }
        WeaponCubeStruct weaponCubeStruct = new WeaponCubeStruct();
        weaponCubeStruct._stat = randomStat;
        weaponCubeStruct._tier = tier;
        weaponCubeStruct._option = option;
        return weaponCubeStruct;
    }

    public void SetMyResult(WeaponObj weaponObj)
    {
        int rand = UnityEngine.Random.Range(0, 101);
        //�Ϲ�
        if (rand <= 40)
        {
            var option = RandomCubeStruct(WeaponCubeTier.Common);
            weaponObj.weaponStruct._cubeOption.Add(option);

            Grade_Result.color = Grade_Colors[0];
            Result_Text.color = Grade_Colors[0];
            Result_Text.text = $"�Ϲ� {option._stat }+{ option._option}%";
        }
        else if(rand <= 80)
        {
            var option = RandomCubeStruct(WeaponCubeTier.Epic);
            weaponObj.weaponStruct._cubeOption.Add(option);
            Grade_Result.color = Grade_Colors[1];
            Result_Text.color = Grade_Colors[1];

            Result_Text.text = $"���� {option._stat}+{option._option}%";
        }
        else
        {
            var option = RandomCubeStruct(WeaponCubeTier.Legendary);
            weaponObj.weaponStruct._cubeOption.Add(option);
            Grade_Result.color = Grade_Colors[2];
            Result_Text.color = Grade_Colors[2];

            Result_Text.text = $"�������� {option._stat}+{option._option}%";
        }


        GetComponent<Animator>().Play("Cube_Result");

    }
}
