using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    BuildItemObject[] buildItemObjects;
    BuildItemList buildItemList;

    private void Awake()
    {
        buildItemObjects = GetComponentsInChildren<BuildItemObject>();
        buildItemList = GetComponent<BuildItemList>();
    }

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        List<BuildItemStruct> list = buildItemList.GetRandomObjects(3);
        int i = 0;
        foreach (var buildItemObject in buildItemObjects)
        {
            buildItemObject.Set(list[i]);
            i++;
        }
    }
}
