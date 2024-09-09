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
    public List<BuildItemStruct> GetRandomObjects(int count)
    {
        // ���� ����Ʈ�� ����
        List<BuildItemStruct> shuffledList = new List<BuildItemStruct>(list);
        for (int i = 0; i < shuffledList.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledList.Count);
            BuildItemStruct temp = shuffledList[i];
            shuffledList[i] = shuffledList[randomIndex];
            shuffledList[randomIndex] = temp;
        }

        // ���� ����Ʈ���� ó�� count���� ��Ҹ� ��ȯ
        return shuffledList.GetRange(0, Mathf.Min(count, shuffledList.Count));
    }
}
