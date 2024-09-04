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

        public float Delay = 1f;

        public bool IsRepeated = false;

        public float RepeatProbability = 0;

        public int MaxRepeatCount = 1;
    }
}