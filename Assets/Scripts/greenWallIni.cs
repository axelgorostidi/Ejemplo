using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class greenWallIni : MonoBehaviour
{
    public GameObject player;
    public GameObject camGO;
    public List<GameObject> roomsEasy;

    // Start is called before the first frame update
    void Start()
    {
        roomsEasy = GameManager.game.roomsEasy;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //RoomGenericController scriptRoom = room.GetComponent<RoomGenericController>();

        if (collision.gameObject.tag == "Player")
        {
            int rand = UnityEngine.Random.Range(0, 5);
            GameObject room = roomsEasy[rand];
            Transform rigidBody_player = player.GetComponent<Transform>();
            Transform rigidBody_cam = camGO.GetComponent<Transform>();
            rigidBody_player.position = new Vector3(100f, 100f, 0f);
            GameObject instRoom = Instantiate(room, new Vector3(100f, 100f, 0), Quaternion.identity);
            rigidBody_cam.position = new Vector3(100f, 100f, -10f);
        }
    }
}