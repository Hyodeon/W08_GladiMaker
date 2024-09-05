using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public enum WeaponType
{
    Blunt, Sword, Through, Throw
}

public class Weapon_Skill : MonoBehaviour
{
    [Header("<color=orange>무기 위치 조정")]
    [SerializeField] Vector2 weaponOffsetPosition;
    [SerializeField] Vector3 weaponOffsetRotation;
    [SerializeField] Vector3 weaponOffsetScale;


    [SerializeField] GameObject Enemy;

    [Header("무기 타입")]
    public WeaponType my_WeaponType;

    [Header("<color=purple>무기 이펙트")]
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

    //타격

    //참격

    //관통

    //투척

    int getNumOfBlendTreeMotion()
    {
        var animatorController = anim.runtimeAnimatorController as AnimatorController;

        foreach (var layer in animatorController.layers)
        {
            foreach (var state in layer.stateMachine.states)
            {
                if (state.state.name == my_WeaponType.ToString())
                {
                    var blendTree = state.state.motion as BlendTree;
                    if (blendTree != null)
                    {
                        return blendTree.children.Length;
                    }
                    return 0; 
                }
            }
        }
        return 0;
    }
}
