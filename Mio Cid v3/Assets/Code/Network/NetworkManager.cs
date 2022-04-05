using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firesplash.UnityAssets.SocketIO;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private string address = "localhost";
    [SerializeField] private string port = "28962";
    private string socketIOAddress = "";
    private SocketIOCommunicator io;

    private void Start()
    {
        io = GetComponent<SocketIOCommunicator>();

        socketIOAddress = address + ":" + port;
        io.socketIOAddress = socketIOAddress;

        io.Instance.Connect();

        io.Instance.On("connect", (a) =>
        {
            io.Instance.Emit("config", "{character: {name:clo}, pos:{x:1, y:2}}");
        });

    }
}