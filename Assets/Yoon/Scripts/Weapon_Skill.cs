using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WeaponType
{
    Blunt, Sword, Through, Throw
}

public class Weapon_Skill : MonoBehaviour
{
    [Header("<color=orange>���� ��ġ ����")]
    [SerializeField] Vector2 weaponOffsetPosition;
    [SerializeField] Vector3 weaponOffsetRotation;
    [SerializeField] Vector3 weaponOffsetScale;


    [SerializeField] GameObject Enemy;

    [Header("���� Ÿ��")]
    public WeaponType my_WeaponType;

    [Header("<color=purple>���� ����Ʈ")]
    public GameObject Weapon_Trail;
    public GameObject Weapon_Effect;
    public GameObject Hit_Effect;

    public GameObject Player;

    Animator anim;

    public void Weapon_Initialize()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = Player.GetComponent<Animator>();
        transform.localPosition = weaponOffsetPosition;
        transform.localRotation = Quaternion.Euler(weaponOffsetRotation);
        transform.localScale = weaponOffsetScale;
    }

    //Ÿ��
}
