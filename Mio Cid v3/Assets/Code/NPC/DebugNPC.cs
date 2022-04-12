using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugNPC : MonoBehaviour, IInteract
{
    public bool Interact()
    {
        WimanManager.Instance.CreateWindow().CreateInput("Input").CreateSlider("Slider").CreateButtons();
        return true;
    }
}
