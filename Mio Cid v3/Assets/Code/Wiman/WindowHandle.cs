using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowHandle : MonoBehaviour, IDragHandler
{
    Canvas canvas = null;
    Transform window = null;

    bool isMouseInWindow = true;

    private void Start()
    {
        canvas = gameObject.transform.parent.parent.GetComponent<Canvas>();
        window = gameObject.transform.parent;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 temp = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (temp.x < 0 || temp.x > 1 || temp.y < 0 || temp.y > 1)
        {
            isMouseInWindow = false;
        }
        else
        {
            isMouseInWindow = true;
        }

        if (isMouseInWindow)
        {
            Vector3 delta = eventData.delta / canvas.scaleFactor;
            window.localPosition += new Vector3(delta.x, delta.y, 0);
        }
    }
}
