using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeController : MonoBehaviour {

    public float posY;
    public float posX;
    public float posZ;
    public string playerName;
    public GameObject nameBox;

    private System.Guid uuid = System.Guid.NewGuid();
    private float speed = 300f;

    // Use this for initialization
    void Start () {
        playerName = "" + uuid;
        gameObject.name = playerName;
        nameBox.GetComponent<TextMesh>().text = "" + uuid;
    }

    // Update is called once per frame
    void Update() {
        Rigidbody body = gameObject.GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.UpArrow))
        {
           posZ += .1f;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
           posZ -= .1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
           posX += .1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
           posX -= .1f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
           body.AddForce(transform.up * speed);
        }

        // Update position
        transform.position = new Vector3(posX, transform.position.y, posZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Killbox")
        {
            posX = 0.0f;
            posY = 23.0f;
            posZ = 0.0f;

            // Reset
            transform.position = new Vector3(posX, posY, posZ);
        }
    }

    public JSONObject playerData
    {
        get
        {
            JSONObject player = JSONObject.Create();
            player.AddField("name", "" + uuid);
            player.AddField("position", JSONTemplates.FromVector3(transform.position));
            player.AddField("rotation", JSONTemplates.FromQuaternion(transform.rotation));
            return player;
        }
    }

}
