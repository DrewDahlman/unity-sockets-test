using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

[System.Serializable]
public class RoomData
{
    public string name;
    public PlayerInfo[] players;

    public static RoomData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<RoomData>(jsonString);
    }
}

public class GameManager : MonoBehaviour
{

    private SocketIOComponent socket;
    public GameObject player;
    public GameObject other;
    public GameObject newPlayer;
    public JSONObject playerData;
    public GameObject mainCamera;
    public List<GameObject> others = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        // Assign Socker
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        // Wait and join
        StartCoroutine("SayHello");

        // Listen for events from the socket
        socket.On("welcome", join);
        socket.On("room update", updateRoom);
        socket.On("user left", removeUser);
    }

    public IEnumerator SayHello()
    {
        yield return new WaitForSeconds(1);
        socket.Emit("join");
        StopAllCoroutines();
    }

    public void join(SocketIOEvent e)
    {
        // Get initial Room Data
        RoomData room = RoomData.CreateFromJSON(e.data[0].ToString());

        //  Check for player
        if (newPlayer == null)
        {

            // Create player
            newPlayer = Instantiate(player, player.transform.position, player.transform.rotation);
            playerData = newPlayer.GetComponent<meController>().playerData;
            mainCamera.GetComponent<CameraFollow>().target = newPlayer.transform;

            // Register player
            socket.Emit("register", playerData);

            // Create others
            for (int i = 0; i < room.players.Length; i++)
            {
                createOtherPlayer(room.players[i]);
            }
        }
        else
        {
            // Create others
            for (int i = 0; i < room.players.Length; i++)
            {
                PlayerInfo p = room.players[i];
                GameObject po = GameObject.Find(p.name);
                if( po == null )
                {
                    createOtherPlayer(p);
                }
            }
        }
    }

    public void createOtherPlayer(PlayerInfo otherData)
    {
        Vector3 otherPosition = new Vector3(otherData.position.x, otherData.position.y, otherData.position.z);
        Quaternion otherRotation = new Quaternion(otherData.rotation.x, otherData.rotation.y, otherData.rotation.z, 1);
        GameObject otherPlayer = Instantiate(other);
        otherPlayer.transform.localPosition = otherPosition;
        otherPlayer.transform.localRotation = otherRotation;
        otherPlayer.GetComponent<PlayerControl>().playerName = otherData.name;
        otherPlayer.name = otherData.name;

        others.Add(otherPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (newPlayer != null)
        {
            socket.Emit("update", newPlayer.GetComponent<meController>().playerData);
        }
    }

    public void updateRoom(SocketIOEvent e)
    {
        RoomData room = RoomData.CreateFromJSON(e.data[0].ToString());
        for( int i = 0; i < room.players.Length; i++ )
        {
            PlayerInfo p = room.players[i];

            if( p.name != newPlayer.name)
            {
                GameObject po = GameObject.Find(p.name);
                po.transform.position = p.position;
                po.transform.rotation = p.rotation;
            }
        }
    }

    public void removeUser(SocketIOEvent e)
    {
        PlayerInfo userData = PlayerInfo.CreateFromJSON(e.data[0].ToString());
        GameObject user = GameObject.Find(userData.name);
        Destroy(user);
    }

    void OnDestroy()
    {
        socket.OnDestroy();
    }

}
