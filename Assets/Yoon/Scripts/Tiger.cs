using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : MonoBehaviour
{
    [SerializeField] GameObject TigerSkill;

    public void ShowSkillEffect()
    {
        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        var effect = Instantiate(TigerSkill, playerPos, Quaternion.identity);
        Destroy(effect, 1f);

    }
}
