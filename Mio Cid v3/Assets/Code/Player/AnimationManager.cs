using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 vel = Vector2.zero;
    [SerializeField] private Animator animator;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        if(Vector2.Distance((Vector2)rb.velocity, Vector2.zero) >= 1) { // If the player is moving set the animation, if not, leave it as it was to keep facing that way.
            vel = rb.velocity;
            animator.SetBool("isWalking", true);
        }
        else {
            animator.SetBool("isWalking", false);
        }

        animator.SetFloat("VelX", vel.x);
        animator.SetFloat("VelY", vel.y);
    }
}
