using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(AnimationTarget))]
public class AnimationPreview : Editor
{
    private AnimationTarget myTarget;
    private Animator animator;
    private AnimationClip animationClip;

    private void OnEnable()
    {
        myTarget = (AnimationTarget)target;
        animator = myTarget.GetComponent<Animator>();

        if (animator == null)
        {
            animator = myTarget.gameObject.AddComponent<Animator>();
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        animationClip = EditorGUILayout.ObjectField("Animation Clip", myTarget.animationClip, typeof(AnimationClip), false) as AnimationClip;

        if (GUILayout.Button("Play Animation"))
        {
            PlayAnimation(animationClip);
        }
    }

    private void PlayAnimation(AnimationClip clip)
    {
        if (clip != null)
        {
            AnimatorOverrideController overrideController = new AnimatorOverrideController();
            overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;
            overrideController[clip.name] = clip;
            animator.runtimeAnimatorController = overrideController;

            animator.Play(clip.name);
        }
    }
}
