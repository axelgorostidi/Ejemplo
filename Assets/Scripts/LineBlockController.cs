using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LineBlockController : MonoBehaviour
{
    public int destroyRoom;
    public GameObject room;
    public int direction; //0 derecha, 1 izquierda, 2 arriba, 3 abajo
    private GameObject camGO;
    private List<GameObject> roomsEasy;
    private List<GameObject> roomsMedium;
    private List<GameObject> roomsBoss;
    private int idxRand;

    // Start is called before the first frame update
    void Start()
    {
        camGO = GameObject.FindGameObjectWithTag("MainCamera");
        roomsEasy = GameManager.game.roomsEasy;
        roomsMedium = GameManager.game.roomsMedium;
        roomsBoss = GameManager.game.roomsBoss;
        idxRand = GameManager.game.prev_idx_room;
        if(GameManager.game.roomToDestroy != null)
        {
            GameManager.game.destroyRoom();
        }
        
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
            GameManager.game.contRoomsPassed++;
            //room.GetComponent<RoomGenericController>().blockDoors();
            int tmp_idx_rand = randIdx();
            while(tmp_idx_rand == idxRand)
            {
                tmp_idx_rand = randIdx();
            }
            GameManager.game.prev_idx_room = tmp_idx_rand;
            GameObject newRoom = null;
            if (randtype() <= 1f && GameManager.game.contRoomsPassed != 10)
            {
                newRoom = roomsEasy[tmp_idx_rand];
            }
            else
            {
                newRoom = roomsMedium[tmp_idx_rand];
            }
            if(GameManager.game.contRoomsPassed == 10)
            {
                newRoom = roomsBoss[0];
            }
            
            //RoomGenericController scriptRoomNew = room.GetComponent<RoomGenericController>();

            float posX = room.transform.position.x;
            float posY = room.transform.position.y;

            Transform rigidBody_cam = camGO.GetComponent<Transform>();

            //newRoom.GetComponent<RoomGenericController>().blockDoors();
            switch (direction)
            {
                case 0:
                    newRoom.GetComponent<RoomGenericController>().blockDoors();
                    GameObject roomInst = Instantiate(newRoom, new Vector3(posX+(30f), posY, 0), Quaternion.identity);
                    Transform rigidbody_roomInst = roomInst.GetComponent<Transform>();
                    rigidBody_cam.position = new Vector3(rigidbody_roomInst.position.x, rigidbody_roomInst.position.y, -10f);
                    break;
                case 1:
                    newRoom.GetComponent<RoomGenericController>().blockDoors();
                    GameObject roomInst1 = Instantiate(newRoom, new Vector3(posX-(30f), posY, 0), Quaternion.identity);
                    Transform rigidbody_roomInst1 = roomInst1.GetComponent<Transform>();
                    rigidBody_cam.position = new Vector3(rigidbody_roomInst1.position.x, rigidbody_roomInst1.position.y, -10f);
                    //rigidBody_cam.position = new Vector3(posX - (30f), posY, -10f);
                    break;
                case 2:
                    newRoom.GetComponent<RoomGenericController>().blockDoors();
                    GameObject roomInst2 = Instantiate(newRoom, new Vector3(posX, posY + (16f), 0), Quaternion.identity);
                    Transform rigidbody_roomInst2 = roomInst2.GetComponent<Transform>();
                    rigidBody_cam.position = new Vector3(rigidbody_roomInst2.position.x, rigidbody_roomInst2.position.y, -10f);
                    //rigidBody_cam.position = new Vector3(posX, posY + (16f), -10f);
                    break;
                case 3:
                    newRoom.GetComponent<RoomGenericController>().blockDoors();
                    GameObject roomInst3 = Instantiate(newRoom, new Vector3(posX, posY - (16f), 0), Quaternion.identity);
                    Transform rigidbody_roomInst3 = roomInst3.GetComponent<Transform>();
                    rigidBody_cam.position = new Vector3(rigidbody_roomInst3.position.x, rigidbody_roomInst3.position.y, -10f);
                    //rigidBody_cam.position = new Vector3(posX, posY - (16f), -10f);
                    break;
            }
            
            Destroy(gameObject, 0f);
            if(destroyRoom == 1)
            {
                GameManager.game.roomToDestroy = room;
            }
            else
            {
                room.GetComponent<RoomGenericController>().blockDoors();
            }
            
        }
    }

    float randtype()
    {
        if (GameManager.game.contRoomsPassed <= 5)
        {
            return 0.5f;
        }
        else if (GameManager.game.contRoomsPassed > 5 && GameManager.game.contRoomsPassed < 10)
        {
            float rand = UnityEngine.Random.Range(0f, 2f);
            Debug.Log("asd: "+rand.ToString());
            return rand;
        }
        else
        {
            return 1.5f;
        }
    }

    int randIdx()
    {
        int rand = UnityEngine.Random.Range(0, 5);
        return rand;
    }
}
