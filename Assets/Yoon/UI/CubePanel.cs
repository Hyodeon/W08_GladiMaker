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
        //일반
        if (rand <= 33)
        {
            weaponObj.weaponStruct._cubeOption.Add(RandomCubeStruct(WeaponCubeTier.Common));

            Grade_Result.color = Grade_Colors[0];
            Result_Text.color = Grade_Colors[0];
            Result_Text.text = "일반";
        }
        else if(rand <= 66)
        {
            weaponObj.weaponStruct._cubeOption.Add(RandomCubeStruct(WeaponCubeTier.Epic));
            Grade_Result.color = Grade_Colors[1];
            Result_Text.color = Grade_Colors[1];
            Result_Text.text = "에픽";
        }
        else
        {
            weaponObj.weaponStruct._cubeOption.Add(RandomCubeStruct(WeaponCubeTier.Legendary));
            Grade_Result.color = Grade_Colors[2];
            Result_Text.color = Grade_Colors[2];
            Result_Text.text = "레전드리";
        }


        GetComponent<Animator>().Play("Cube_Result");

    }
}
