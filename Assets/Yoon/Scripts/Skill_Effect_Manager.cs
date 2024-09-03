using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Skill_Effect_Manager : MonoBehaviour
{
    public Weapon_Skill Current_Weapon;

    public UnityEvent my_Weapon_Skill;

    private void Start()
    {
        Current_Weapon = GetComponentInChildren<Weapon_Skill>();
    }

    public void Execute_Skill() => my_Weapon_Skill.Invoke();


}
