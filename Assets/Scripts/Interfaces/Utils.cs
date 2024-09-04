using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

namespace Utils
{
    public class Utils : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }


    public class Status
    {
        // Basic Stats
        public float Hp;
        public float MaxHp;

        public float Attack;

        public float Accuracy;

        public float Evade;

        public float Guard;

        // Specific Stats
        public float Strike;

        public float Slash;

        public float Penetration;

        public float Ranged;

        public float Counter;
    }

    public class SkillInfo
    {
        public float DamageRatio = 1f;

        public float PlayerDamage = 1f;

        public AnimationClip Clip = null;

        public bool IsRepeated = false;

        public float RepeatProbability = 0;

        public int MaxRepeatCount = 1;
    }

    public class Weapon
    {
        public float WeaponDamage = 1f;

        public WeaponTier Tier = WeaponTier.Common;

        public WeaponAttackType Type = WeaponAttackType.Strike;

        public SkillMechanism Mechanic = SkillMechanism.Percentage;

        public float    Per_ActivationPercentage = 0f;
        public float    Per_DamageRatio = 0f;

        public int      Turn_TurnCount = 0;
        public float    Turn_DamageRatio = 0f;
    }

    public enum WeaponAttackType
    {
        Strike, Slash, Penetration, Ranged
    }

    public enum WeaponTier
    {
        Common, Rare, Epic, Legendary, Boss
    }

    public enum SkillMechanism
    {
        TurnBased, Percentage, Special
    }
}