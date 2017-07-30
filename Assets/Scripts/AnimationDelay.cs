using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationDelay : MonoBehaviour
{
    [SerializeField]
    private float offset = 0.25f;
    [SerializeField]
    private string startState = "Idle";

    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(startState, 0, offset);
    }
}
