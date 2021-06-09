using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBlockController : MonoBehaviour
{
    public int destroyRoom;
    public GameObject room;
    public List<GameObject> rooms;
    public int direction; //0 derecha, 1 izquierda, 2 arriba, 3 abajo

    // Start is called before the first frame update
    void Start()
    {
        
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
            //room.GetComponent<RoomGenericController>().blockDoors();
            GameObject newRoom = rooms[randIdx()];
            //RoomGenericController scriptRoomNew = room.GetComponent<RoomGenericController>();
            
            float posX = room.transform.position.x;
            float posY = room.transform.position.y;
            //newRoom.GetComponent<RoomGenericController>().blockDoors();
            switch (direction)
            {
                case 0:
                    //newRoom.GetComponent<RoomGenericController>().blockEntry(1);
                    Instantiate(newRoom, new Vector3(posX+(19f), posY, 0), Quaternion.identity);
                    break;
                case 1:
                   // newRoom.GetComponent<RoomGenericController>().blockEntry(0);
                    Instantiate(newRoom, new Vector3(posX-(19f), posY, 0), Quaternion.identity);
                    break;
                case 2:
                    //newRoom.GetComponent<RoomGenericController>().blockEntry(3);
                    Instantiate(newRoom, new Vector3(posX, posY + (19f), 0), Quaternion.identity);
                    break;
                case 3:
                    //newRoom.GetComponent<RoomGenericController>().blockEntry(2);
                    Instantiate(newRoom, new Vector3(posX, posY - (19f), 0), Quaternion.identity);
                    break;
            }
            
            Destroy(gameObject, 0f);
            if(destroyRoom == 1)
            {
                Destroy(room, 0f);
            }
            
        }
    }

    int randIdx()
    {
        int rand = UnityEngine.Random.Range(0, 5);
        return rand;
    }
}
