using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct WeaponStruct
{
    public string _name;
    public string _skillName;
    public string _itemInfo;
    public string _skillInfo;
    public List<WeaponCubeStruct> _cubeOption;

}
[System.Serializable]
public class WeaponCubeStruct
{
    public WeaponCubeStat _stat;
    public WeaponCubeTier _tier;
    public int _option;

    public WeaponCubeStruct(WeaponCubeStat stat, WeaponCubeTier tier, int option)
    {
        _stat = stat;
        _tier = tier;
        _option = option;
    }
}
public enum WeaponCubeStat
{
    ü��,����,���,����,ȸ��,����,Ÿ��,����,��ô
}
public enum WeaponCubeTier
{
    Legendary,Epic,Common
}