using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;

public class Player : ActorBase
{

    private void Start()
    {
        Intialize();
    }

    [Header("<color=green>행동 애니메이션 클립")]
    [SerializeField]
    private List<AnimationClip> _animators = new List<AnimationClip>();

    [Header("<color=red>기본 무기")]
    [SerializeField]
    private GameObject _basicWeapon;

    [SerializeField]
    private GameObject _weaponSocket;


    public override void Intialize()
    {
        base.Intialize();

        _status = GameManager.Instance.PlayerStatus;


        if (_status == null)
        {
            Debug.Log("[Player.cs] Player Status Initialize Error!");
        }

        HealthBar = GameObject.Find("PlayerHP").GetComponent<HealthBar>();
        HealthBar.player = this;
        HealthBar.max_HP = _status.MaxHp;
        HealthBar.Initialize();

        SwitchWeapon(GameManager.Instance.CurrentWeapon);

        BindActions();
    }

    public void BindActions()
    {
        Actions.Add(Action_Attack);
        Actions.Add(Action_Skill);
    }

    public override void SwitchWeapon(WeaponObj weapon)
    {

        if (_weaponObject != null)
        {
            Destroy(_weaponObject.gameObject);
        }

        _weaponObject = 
            Instantiate(weapon.gameObject, Vector3.zero, Quaternion.identity).GetComponent<WeaponObj>();
        _weaponObject.transform.SetParent(_weaponSocket.transform);
        _currentWeapon = weapon.weapon;

        _weaponObject.gameObject.GetComponent<Weapon_Skill>().Weapon_Initialize();
        GetComponent<Skill_Effect_Manager>().Current_Weapon = _weaponObject.gameObject;
        
    }

    public SkillInfo Action_Attack()
    {
        Debug.Log($"{name} Attack");

        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio = 1f;
        skillInfo.PlayerDamage = CalculateDamage();
        skillInfo.Clip = _animators[(int)CurrentWeapon.Type];
        skillInfo.IsRepeated = false;

        return skillInfo;
    }

    public int SkillIndexConverter(string skillName)
    {
        return skillName switch
        {
            "Skill_Strike_ShockWave" => 4,
            "Skill_Strike_SuperSmash" => 5,
            "Skill_Slash_FullMoonSlash" => 6,
            "Skill_Slash_Bleeding" => 7,
            "Skill_Slash_Critical" => 8,
            "Skill_Penetrate_Execution" => 9,
            "Skill_Penetrate_Continuous" => 10,
            "Skill_Throw_SuperThrow" => 11,
            _ => -1
        };
    }

    public SkillInfo Action_Skill()
    {
        Debug.Log($"{name} Skill");

        SkillInfo skillInfo = new SkillInfo();
        skillInfo.DamageRatio =
            CurrentWeapon.Mechanic == SkillMechanism.TurnBased ?
            CurrentWeapon.Turn_DamageRatio : CurrentWeapon.Per_DamageRatio;
        skillInfo.PlayerDamage = CalculateDamage();
        skillInfo.Clip = _animators[SkillIndexConverter(_weaponObject.weaponStruct._skillName)];
        skillInfo.IsRepeated = false;

        return skillInfo;
    }
}
