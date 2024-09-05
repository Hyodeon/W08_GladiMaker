using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Skill_Effect_Manager : MonoBehaviour
{
    public List<GameObject> L_WeaponList;

    public GameObject Current_Weapon;

    [Header("Right_Hand_Socket")]
    [SerializeField] Transform Right_Hand_Socket;

    [SerializeField] int weapon_idx = 0;

    [Header("<color=red>Àû")]
    public GameObject Current_Enemy;

    private void Start()
    {
        var Enemy = GameObject.FindGameObjectWithTag("Enemy");
        if(Enemy != null) Current_Enemy = Enemy;
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Tab)) {
            ChangeWeapon();
        }

        if (Input.GetKey(KeyCode.CapsLock)) GetComponent<Animator>().Play(GetComponent<Re_Attack>().My_Animations[Random.Range(0, GetComponent<Re_Attack>().My_Animations.Count)].AnimationName);

    }

    public void Spawn_Weapon_Trail()
    {
        if (Current_Weapon.GetComponent<Weapon_Skill>().Weapon_Trail == null) return;
        var effect = Instantiate(Current_Weapon.GetComponent<Weapon_Skill>().Weapon_Trail, Current_Enemy.transform.position, Quaternion.identity);
        Destroy(effect, .5f);
    }

    public void Spawn_Weapon_Effect()
    {
        if (Current_Weapon.GetComponent<Weapon_Skill>().Weapon_Effect == null) return;
        var effect = Instantiate(Current_Weapon.GetComponent<Weapon_Skill>().Weapon_Effect, Current_Enemy.transform.position, Quaternion.identity);
        Destroy(effect, .5f);
    }

    public void Spawn_Hit_Effect()
    {
        if (Current_Weapon.GetComponent<Weapon_Skill>().Hit_Effect == null) return;
        var effect = Instantiate(Current_Weapon.GetComponent<Weapon_Skill>().Hit_Effect, Current_Enemy.transform.position, Quaternion.identity);
        Destroy(effect, .5f);
    }

    public void StartCameraShake()
    {
        Camera.main.GetComponent<CameraShake>().startCameraShake(.05f, 2f);
    }

    public void StartMiniCameraShake()
    {
        Camera.main.GetComponent<CameraShake>().startCameraShake(.02f, .2f);
    }

    void ChangeWeapon()
    {
        weapon_idx = weapon_idx + 1 > L_WeaponList.Count - 1 ? 0 : weapon_idx + 1;
        if(Current_Weapon != null) Destroy(Current_Weapon);
        Current_Weapon = Instantiate(L_WeaponList[weapon_idx], Vector3.zero, Quaternion.identity);
        Current_Weapon.GetComponent<Weapon_Skill>().Player = gameObject;
        Current_Weapon.transform.SetParent(Right_Hand_Socket.transform.GetChild(0));
        Current_Weapon.GetComponent<Weapon_Skill>().Weapon_Initialize();
    }

}
