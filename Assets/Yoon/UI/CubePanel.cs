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
    [SerializeField] Image CubePanelImage;

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
        WeaponCubeStruct weaponCubeStruct = new WeaponCubeStruct(0,0,0);
        weaponCubeStruct._stat = randomStat;
        weaponCubeStruct._tier = tier;
        weaponCubeStruct._option = option;
        return weaponCubeStruct;
    }

    public void SetMyResult(WeaponObj weaponObj)
    {
        int rand = UnityEngine.Random.Range(0, 101);
        WeaponCubeStruct option;
        //일반
        if (rand <= 40)
        {
            option = RandomCubeStruct(WeaponCubeTier.Common);
            weaponObj.weaponStruct._cubeOption.Add(option);

            Grade_Result.color = Grade_Colors[0];
            Result_Text.color = Grade_Colors[0];
            CubePanelImage.color = Grade_Colors[0];

            Result_Text.text = $"일반 {option._stat}+ {option._option}%";
        }
        else if(rand <= 80)
        {
            option = RandomCubeStruct(WeaponCubeTier.Epic);
            weaponObj.weaponStruct._cubeOption.Add(option);
            Grade_Result.color = Grade_Colors[1];
            Result_Text.color = Grade_Colors[1];
            CubePanelImage.color = Grade_Colors[1];

            Result_Text.text = $"에픽 {option._stat}+ {option._option}%";
        }
        else
        {
            option = RandomCubeStruct(WeaponCubeTier.Legendary);
            weaponObj.weaponStruct._cubeOption.Add(option);
            Grade_Result.color = Grade_Colors[2];
            Result_Text.color = Grade_Colors[2];
            CubePanelImage.color = Grade_Colors[2];

            Result_Text.text = $"레전더리 {option._stat}+ {option._option}%";
        }

        print("123");
        GetComponent<Animator>().Play("Cube_Result");
        GameObject.Find("Canvas").GetComponent<ShowClearPanel>().drop_weaponStruct._cubeOption.Add(new WeaponCubeStruct(option._stat, option._tier, option._option));
        GameObject.Find("Canvas").GetComponent<ShowClearPanel>().drop_weaponStruct._cubeOption.RemoveAt(GameObject.Find("Canvas").GetComponent<ShowClearPanel>().drop_weaponStruct._cubeOption.Count - 1);
    }
}
