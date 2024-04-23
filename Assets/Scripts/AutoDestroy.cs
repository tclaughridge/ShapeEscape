using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        float destroyTime = 0f;

        foreach (AnimationClip clip in clips)
        {
            destroyTime = Mathf.Max(destroyTime, clip.length);
        }

        Destroy(gameObject, destroyTime);
    }
}
