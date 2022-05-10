using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Sprite[] frames;

    int i = 0;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            frames = AnimatManager.instance.GetAnimation("cid_idle");
            GetComponent<SpriteRenderer>().sprite = frames[i];
            i++;
        }
    }
}
