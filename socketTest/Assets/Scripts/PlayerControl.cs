using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class PlayerControl : MonoBehaviour {

    public GameObject nameBox;
    public PlayerInfo playerData;

    public string playerName
    {
        set
        {
            nameBox.GetComponent<TextMesh>().text = value;
        }
    }
}
