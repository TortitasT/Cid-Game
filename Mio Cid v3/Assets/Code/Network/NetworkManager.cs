using System.Collections;
using System.Collections.Generic;
using Firesplash.UnityAssets.SocketIO;
using Newtonsoft.Json;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [Header("Player character data")]
    [SerializeField]
    private string dataName = "Cid";

    [SerializeField]
    private int dataLevel = 1;

    [SerializeField]
    private float dataLevelProgress = 0f;

    [SerializeField]
    private Vector2Data dataPos = new Vector2Data(0, 0);

    [Header("Misc")]
    [SerializeField]
    private GameObject networkPlayerPrefab;

    private SocketIOCommunicator io = null;

    private string id = "0";

    private Character character;

    private Vector2Data pos;

    private Player player;

    private Transform playerTransform;

    private List<GameObject> networkPlayers = new List<GameObject>();

    private bool isConnected = false;

    public static NetworkManager Instance = null;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        dataPos =
            new Vector2Data(playerTransform.position.x,
                playerTransform.position.y);

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
    }

    private void LoadEvents()
    {
        // Connect(
        //     address: address,
        //     port: port
        // );
        //Events
        io
            .Instance
            .On("connect",
            (response) =>
            {
                //Mock Player
                id = io.Instance.SocketID;
                character =
                    new Character(name: dataName,
                        level: dataLevel,
                        levelProgress: dataLevelProgress);
                player = new Player(id: id, character: character, pos: dataPos);

                // Send player data to server
                io
                    .Instance
                    .Emit("config", JsonConvert.SerializeObject(player), false);
            });

        io
            .Instance
            .On("registered",
            (response) =>
            {
                Registered responsePars =
                    JsonConvert.DeserializeObject<Registered>(response);

                GameObject newPlayer = CreatePlayer(responsePars.player);
                AlertManager
                    .Instance
                    .Alert("<pend>" +
                    responsePars.player.character.name +
                    " connected ????</pend>");
            });

        io
            .Instance
            .On("currentPlayers",
            (response) =>
            {
                CurrentPlayers responsePars =
                    JsonConvert.DeserializeObject<CurrentPlayers>(response);

                foreach (Player player in responsePars.players)
                {
                    if (player.id != null)
                    {
                        CreatePlayer (player);
                    }
                }

                AlertManager
                    .Instance
                    .Alert("<bounce>Connected to server ????</bounce>");
                isConnected = true;
            });

        io
            .Instance
            .On("updated",
            (response) =>
            {
                Updated responsePars =
                    JsonConvert.DeserializeObject<Updated>(response);

                if (GetPlayer(responsePars.id))
                {
                    GetPlayer(responsePars.id)
                        .GetComponent<NetworkPlayer>()
                        .player
                        .pos =
                        new Vector2Data(responsePars.pos.x, responsePars.pos.y);
                }
            });

        io
            .Instance
            .On("disconnected",
            (response) =>
            {
                Disconnected responsePars =
                    JsonConvert.DeserializeObject<Disconnected>(response);

                PlayerLeave(responsePars.id);
            });

        io.Instance.Connect(); // Connect at the end
    }

    private void FixedUpdate()
    {
        if (isConnected)
        {
            //Update local position variable
            dataPos =
                new Vector2Data(playerTransform.position.x,
                    playerTransform.position.y);

            //Send update data
            io
                .Instance
                .Emit("update",
                "{\"pos\":" + JsonConvert.SerializeObject(dataPos) + "}",
                false);
        }
    }

    private GameObject GetPlayer(string id)
    {
        foreach (GameObject player in networkPlayers)
        {
            if (player.GetComponent<NetworkPlayer>().player.id == id)
            {
                return player;
            }
        }
        return null;
    }

    private void PlayerLeave(string id)
    {
        GameObject player = GetPlayer(id);
        if (player != null)
        {
            networkPlayers.Remove (player);
            Destroy (player);
        }
    }

    private GameObject CreatePlayer(Player player)
    {
        GameObject newPlayer =
            GameObject.Instantiate<GameObject>(networkPlayerPrefab);
        newPlayer.GetComponent<NetworkPlayer>().player = player;

        if (!networkPlayers.Contains(newPlayer))
        {
            networkPlayers.Add (newPlayer);
        }

        return newPlayer;
    }

    public void Connect(string address, string port)
    {
        // OK so the plugin won't work so this is the workaround, just deleting the component and reinstanciating.
        if (GetComponent<SocketIOCommunicator>() != null)
        {
            Destroy(GetComponent<SocketIOCommunicator>());
            this.gameObject.AddComponent<SocketIOCommunicator>();
        }
        else
        {
            this.gameObject.AddComponent<SocketIOCommunicator>();
        }

        io = GetComponent<SocketIOCommunicator>();

        io.socketIOAddress = address + ":" + port + "/socket.io/";
        io.autoConnect = false;
        io.autoReconnect = true;
        io.secureConnection = false;

        LoadEvents();
    }

    public void Disconnect()
    {
        isConnected = false;

        if (GetComponent<SocketIOCommunicator>() != null)
        {
            Destroy(GetComponent<SocketIOCommunicator>());
        }
    }
}
