using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private bool isLocal = false;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private Vector2 vel = Vector2.zero;
    public Vector2 velV2 = Vector2.zero;
    private float velMa = 0f;
    private bool stopped = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!stopped)
        { // If the player is moving set the animation, if not, leave it as it was to keep facing that way.
            vel = velV2.normalized;
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        animator.SetFloat("VelX", vel.x);
        animator.SetFloat("VelY", vel.y);
    }
    // void OnGUI()
    // {
    //     if (!isLocal)
    //     {
    //         GUILayout.Label(vel.ToString());
    //     }
    // }

    private void FixedUpdate()
    {
        if (isLocal)
        {
            velV2 = rb.velocity;
        } // If it's a networkPlayer the script will set velV2

        velMa = Mathf.Round(velV2.magnitude * 10f);

        if (Mathf.Approximately(velMa, 0f))
        {
            stopped = true;
        }
        else
        {
            stopped = false;
        }
    }
}
