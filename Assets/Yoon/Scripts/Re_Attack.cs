using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AnimationInfo {
    public string AnimationName;
    public float LoopStartTime;
}

public class Re_Attack : MonoBehaviour
{
    public Animator animator;
    public List<AnimationInfo> My_Animations;

    private bool isLooping = false;

    [Header("Current Animation Name")]
    public string AnimationName;

    [Header("Check Whether Attacking Again is Possible")]
    public bool canRepeat;

    float findLoopStartTime()
    {
        AnimationInfo currentInfo = My_Animations.Find(x => x.AnimationName == AnimationName);
        return currentInfo.LoopStartTime;
    }

    public void OnLoopStart()
    {
        if (AnimationName == null) return;

        //canRepeat 조건 추가하기

        isLooping = canRepeat;
    }

    public void OnLoopEnd()
    {
        if (AnimationName == null) return;

        if (isLooping)
        {
            animator.Play(AnimationName, -1, findLoopStartTime());
        }
    }
}
