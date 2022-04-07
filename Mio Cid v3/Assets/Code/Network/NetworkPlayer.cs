using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    public Player player;
    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private Vector2 target = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        target = new Vector2(player.pos.x, player.pos.y);
        // GetComponent<Rigidbody2D>().MovePosition(
        if (target != Vector2.zero)
        {
            rb.position = Vector2.SmoothDamp(rb.position, target, ref velocity, 0.1f);
            GetComponent<AnimationManager>().velV2 = velocity;
        }
    }
}
