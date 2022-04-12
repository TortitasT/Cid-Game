using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Window : MonoBehaviour
{
    [SerializeField] private GameObject inputPrefab;
    [SerializeField] private GameObject sliderPrefab;
    [SerializeField] private GameObject buttonsPrefab;

    public Window CreateInput(string label)
    {
        GameObject input = Instantiate(inputPrefab);
        input.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = label;
        input.transform.SetParent(transform);
        input.transform.localPosition = new Vector3(0, 0, 0);

        return this;
    }
    public Window CreateSlider(string label)
    {
        GameObject input = Instantiate(sliderPrefab);
        input.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = label;
        input.transform.SetParent(transform);
        input.transform.localPosition = new Vector3(0, 0, 0);

        return this;
    }
    public Window CreateButtons()
    {
        GameObject input = Instantiate(buttonsPrefab);
        input.transform.SetParent(transform);
        input.transform.localPosition = new Vector3(0, 0, 0);

        return this;
    }
}
