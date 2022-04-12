using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WimanManager : MonoBehaviour
{
    [SerializeField] private GameObject wimanPrefab;
    [SerializeField] private GameObject windowPrefab;

    public static WimanManager Instance = null;
    public Window CreateWindow()
    {
        GameObject window = Instantiate(windowPrefab);
        window.transform.SetParent(wimanPrefab.transform);
        window.transform.localPosition = new Vector3(0, 0, 0);

        return window.GetComponent<Window>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
