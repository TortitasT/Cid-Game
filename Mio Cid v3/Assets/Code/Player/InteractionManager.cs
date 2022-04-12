using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] float radius = 10f;
    Collider2D[] hitColliders;
    Collider2D closest = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && closest != null)
        {
            IInteract interactable = closest.GetComponent<IInteract>();

            if (interactable != null)
            {
                if (interactable.Interact())
                {
                    GetComponent<AnimationManager>().LookTowards((closest.transform.position - transform.position).normalized);
                    GetComponent<MovementManager>().Stop();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);

        bool onRange = false;

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<IInteract>() != null)
            {
                if (closest == null)
                {
                    closest = hitCollider;
                }
                else
                {
                    if (Vector2.Distance(transform.position, hitCollider.transform.position) < Vector2.Distance(transform.position, closest.transform.position))
                    {
                        closest = hitCollider;
                    }
                }
            }
            if (hitCollider == closest)
            {
                onRange = true;
            }
        }

        if (!onRange)
        {
            closest = null;
        }
    }
}
