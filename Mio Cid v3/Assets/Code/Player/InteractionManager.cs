using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] float radius = 10f;
    Collider2D[] hitColliders;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Collider2D hitCollider in hitColliders)
            {
                IInteract interactable = hitCollider.GetComponent<IInteract>();

                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
    }
}
