using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YasZ : MonoBehaviour
{
    private void Update() {
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.y/2);
    }
}
