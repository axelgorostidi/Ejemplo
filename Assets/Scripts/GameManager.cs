using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public List<GameObject> roomsEasy;
    public List<GameObject> roomsMedium;
    public List<GameObject> roomsBoss;
    private GameObject[] roomsInst;
    public int prev_idx_room;
    public static GameManager game;
    public GameObject player;
    public GameObject roomToDestroy;
    public int contRoomsPassed;
    public Color colorWallAntiEnemy;

    public int contEnemies;
    private void Awake()
    {
        
        if (GameManager.game == null)
        {
            GameManager.game = this;
            DontDestroyOnLoad(this);
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    void Start()
    {
        colorWallAntiEnemy = new Color((238f/255f), (111f / 255f), (111f / 255f), (255f / 255f));
        contEnemies = 0;
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

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void changeSceneGameOver()
    {
        SceneManager.LoadScene(3);
    }
}
