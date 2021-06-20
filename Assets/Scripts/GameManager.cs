using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public List<GameObject> roomsEasy;
    public List<GameObject> roomsMedium;
    private GameObject[] roomsInst;
    public int prev_idx_room;
    public static GameManager game;
    public GameObject player;
    public GameObject roomToDestroy;
    public int contRoomsPassed;
    private void Awake()
    {
        if(GameManager.game == null)
        {
            GameManager.game = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    void Start()
    {
        contRoomsPassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyRoom()
    {
        Destroy(roomToDestroy, 0f);
    }

    public void destroyRooms()
    {
        roomsInst = GameObject.FindGameObjectsWithTag("roomEasy");
        foreach (GameObject i in roomsInst)
        {
            Destroy(i);
        }
        roomsInst = GameObject.FindGameObjectsWithTag("roomMedium");
        foreach (GameObject j in roomsInst)
        {
            Destroy(j);
        }
    }
}
