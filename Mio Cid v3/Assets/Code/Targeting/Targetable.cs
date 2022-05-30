using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    private Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnMouseEnter()
    {
        TargetingManager.instance.SetTarget (gameObject);
    }
}
