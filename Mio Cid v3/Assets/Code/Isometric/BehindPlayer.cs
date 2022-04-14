using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindPlayer : MonoBehaviour
{
    void Update()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        transform.position = new Vector3(transform.position.x, transform.position.y, playerPosition.z + 100);
    }
}
