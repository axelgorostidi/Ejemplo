using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBlockController : MonoBehaviour
{
    public int destroyRoom;
    public GameObject room;
    public int direction; //0 derecha, 1 izquierda, 2 arriba, 3 abajo
    private GameObject camGO;
    private List<GameObject> roomsEasy;
    private List<GameObject> roomsMedium;
    private int idxRand;

    // Start is called before the first frame update
    void Start()
    {
        camGO = GameObject.FindGameObjectWithTag("MainCamera");
        roomsEasy = GameManager.game.roomsEasy;
        roomsMedium = GameManager.game.roomsMedium;
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
            if (randtype() == 1)
            {
                newRoom = roomsEasy[tmp_idx_rand];
            }
            else
            {
                newRoom = roomsMedium[tmp_idx_rand];
            }
            
            //RoomGenericController scriptRoomNew = room.GetComponent<RoomGenericController>();

            float posX = room.transform.position.x;
            float posY = room.transform.position.y;

            Transform rigidBody_cam = camGO.GetComponent<Transform>();

            //newRoom.GetComponent<RoomGenericController>().blockDoors();
            switch (direction)
            {
                case 0:
                    //newRoom.GetComponent<RoomGenericController>().blockEntry(1);
                    Instantiate(newRoom, new Vector3(posX+(30f), posY, 0), Quaternion.identity);
                    rigidBody_cam.position = new Vector3(posX + (30f), posY, -10f);
                    break;
                case 1:
                   // newRoom.GetComponent<RoomGenericController>().blockEntry(0);
                    Instantiate(newRoom, new Vector3(posX-(30f), posY, 0), Quaternion.identity);
                    rigidBody_cam.position = new Vector3(posX - (30f), posY, -10f);
                    break;
                case 2:
                    //newRoom.GetComponent<RoomGenericController>().blockEntry(3);
                    Instantiate(newRoom, new Vector3(posX, posY + (16f), 0), Quaternion.identity);
                    rigidBody_cam.position = new Vector3(posX, posY + (16f), -10f);
                    break;
                case 3:
                    //newRoom.GetComponent<RoomGenericController>().blockEntry(2);
                    Instantiate(newRoom, new Vector3(posX, posY - (16f), 0), Quaternion.identity);
                    rigidBody_cam.position = new Vector3(posX, posY - (16f), -10f);
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

    int randtype()
    {
        if (GameManager.game.contRoomsPassed <= 5)
        {
            return 1;
        }
        else if (GameManager.game.contRoomsPassed > 5 && GameManager.game.contRoomsPassed <= 10)
        {
            int rand = UnityEngine.Random.Range(1, 2);
            return rand;
        }
        else
        {
            return 2;
        }
    }

    int randIdx()
    {
        int rand = UnityEngine.Random.Range(0, 4);
        return rand;
    }
}
