using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 vel = Vector2.zero;

    private Vector2 velV2 = Vector2.zero;
    private float velMa = 0f;

    private Vector3 oldPos = Vector3.zero;


    [SerializeField] private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oldPos = transform.position;
    }
    private void Update()
    {
        if (!Mathf.Approximately(velMa, 0))
        { // If the player is moving set the animation, if not, leave it as it was to keep facing that way.
            vel = velV2;
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        animator.SetFloat("VelX", vel.x);
        animator.SetFloat("VelY", vel.y);
    }
    private void FixedUpdate()
    {
        velV2 = (transform.position - oldPos) / Time.fixedDeltaTime;
        velMa = (transform.position - oldPos).magnitude / Time.fixedDeltaTime;
        oldPos = transform.position;
    }
}
