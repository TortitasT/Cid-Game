using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetingManager : MonoBehaviour
{
    public static TargetingManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy (gameObject);
        }
    }

    private GameObject target;

    [SerializeField]
    private GameObject uiName;

    [SerializeField]
    private GameObject uiCanvas;

    private void Update()
    {
        if (target != null)
        {
            if (
                Vector2
                    .Distance(target.transform.position,
                    GameObject.FindWithTag("Player").transform.position) >
                50
            )
            {
                target = null;
            }

            uiCanvas.SetActive(true);
            uiName.GetComponent<TextMeshPro>().SetText(target.name);
        }
        else
        {
            uiCanvas.SetActive(false);
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public void ClearTarget()
    {
        target = null;
    }

    public bool HasTarget()
    {
        return target != null;
    }

    public bool IsTarget(GameObject target)
    {
        return this.target == target;
    }
}
