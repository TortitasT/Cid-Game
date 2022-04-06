using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    public Player player;

    private void Update() {
        GetComponent<Rigidbody2D>().position = new Vector3(player.pos.x, player.pos.y, 0);
    }
}
