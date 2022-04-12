using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugNPC : MonoBehaviour, IInteract
{
    Window wiman;

    public bool Interact()
    {
        wiman = WimanManager.Instance.CreateWindow("Connect to server").CreateInput("Ip").CreateButtons(connect);
        return true;
    }

    public void connect()
    {
        StateManager.Instance.gameObject.GetComponent<AnimationManager>().StopLooking();
        string ip = wiman.transform.GetChild(1).GetChild(1).GetComponent<TMP_InputField>().text;
        Debug.Log(ip);
        NetworkManager.Instance.Connect(ip, "28962");
    }
}
