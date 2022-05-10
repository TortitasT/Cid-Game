using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public static AlertManager Instance;

    public GameObject alertPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Alert(string message)
    {
        GameObject alert = Instantiate(alertPrefab, transform);

        alert
            .transform
            .GetChild(0)
            .GetComponent<TextMeshProUGUI>()
            .SetText(message);

        LeanTween.scale(alert, Vector2.zero, 0f);
        alert.SetActive(false);

        StartCoroutine(WaitToShow(alert));
        StartCoroutine(DestroyAlert(alert));
    }

    IEnumerator WaitToShow(GameObject alert)
    {
        yield return new WaitForSeconds(0.1f);

        alert.SetActive(true);
        LeanTween
            .scale(alert, Vector2.one, 1f)
            .setEase(LeanTweenType.easeInOutBack);
    }

    IEnumerator DestroyAlert(GameObject alert)
    {
        yield return new WaitForSeconds(3);

        LeanTween
            .scale(alert, Vector2.zero, 1f)
            .setEase(LeanTweenType.easeInBack)
            .setOnComplete(() => Destroy(alert));
    }
}
