using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Vector2 target;
    private Rigidbody2D rb;
    [SerializeField] private float playerSpeed = 20f;
    [SerializeField] private float deadZone = 3f;
    private void Start()
    {
        target = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(1) && StateManager.Instance.GetState() == StateManager.State.Idle)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            target = new Vector2(mousePos.x, mousePos.y);
        }

        Move();
    }

    private void Move()
    {
        Vector2 moveDir = Vector2.zero;

        if (Vector2.Distance(target, (Vector2)transform.position) > deadZone)
        {
            moveDir = (target - (Vector2)transform.position).normalized;
        }
        else
        {
            moveDir = Vector2.zero;
        }

        rb.velocity = moveDir * playerSpeed;
    }
}
