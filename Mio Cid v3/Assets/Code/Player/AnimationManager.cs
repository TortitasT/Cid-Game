using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    private bool isLocal = false;

    private Rigidbody2D rb;

    private Vector2 vel = Vector2.zero;

    public Vector2 velV2 = Vector2.zero;

    private float velMa = 0f;

    private bool stopped = false;

    private bool isControlling = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!stopped)
        {
            // If the player is moving set the animation, if not, leave it as it was to keep facing that way.
            vel = velV2.normalized;

            GetComponent<AnimatPlayer>().SetIsWalking(true);
        }
        else
        {
            GetComponent<AnimatPlayer>().SetIsWalking(false);
        }

        if (isControlling)
        {
            GetComponent<AnimatPlayer>().SetAnimDirection(vel);
        }
    }

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

    public void LookTowards(Vector2 direction)
    {
        isControlling = false;

        direction = direction.normalized;

        animator.SetFloat("VelX", direction.x);
        animator.SetFloat("VelY", direction.y);
    }

    public void StopLooking()
    {
        isControlling = true;
    }
}
