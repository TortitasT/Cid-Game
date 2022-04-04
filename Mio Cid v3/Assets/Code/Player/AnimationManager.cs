using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        Vector2 vel = rb.velocity;

        animator.SetFloat("VelX", vel.x);
        animator.SetFloat("VelY", vel.y);
    }
}
