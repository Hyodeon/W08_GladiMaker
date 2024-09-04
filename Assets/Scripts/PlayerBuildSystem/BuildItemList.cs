using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItemList : MonoBehaviour
{
    [SerializeField] List<BuildItemStruct> list;
    
    public BuildItemStruct _randomBuildItemStruct()
    {
        return list[Random.Range(0,list.Count)];  
    }
}
