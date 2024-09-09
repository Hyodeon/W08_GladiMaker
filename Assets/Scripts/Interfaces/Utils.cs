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

    [System.Serializable]
    public struct Weapon
    {
        public float WeaponDamage;
        public WeaponTier Tier;
        public WeaponAttackType Type;
        public SkillMechanism Mechanic;

        public float Per_ActivationPercentage;
        public float Per_DamageRatio;

        public int Turn_TurnCount;
        public float Turn_DamageRatio;

        // 생성자 추가
        public Weapon(float weaponDamage, WeaponTier tier, WeaponAttackType type, SkillMechanism mechanic,
                      float perActivationPercentage, float perDamageRatio, int turnTurnCount, float turnDamageRatio)
        {
            WeaponDamage = weaponDamage;
            Tier = tier;
            Type = type;
            Mechanic = mechanic;
            Per_ActivationPercentage = perActivationPercentage;
            Per_DamageRatio = perDamageRatio;
            Turn_TurnCount = turnTurnCount;
            Turn_DamageRatio = turnDamageRatio;
        }
    }

    public enum WeaponAttackType
    {
        Strike = 0, Slash = 1, Penetration = 2, Ranged = 3
    }

    public enum WeaponTier
    {
        Common, Rare, Epic, Legendary, Boss
    }

    public enum SkillMechanism
    {
        TurnBased, Percentage, Special, Non_Skill, Monster
    }

    [System.Serializable]
    public struct StageInfo
    {
        public GameObject EnemyPrefab;

        public ActorBase EnemyInfo;

        public int nextTurnCount;

        public int gold;
    }
}