using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player info
[System.Serializable]
public class PlayerInfo
{
    public string name;
    public SerializableVector3 position;
    public SerializableQuaternion rotation;

    public static PlayerInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerInfo>(jsonString);
    }
}