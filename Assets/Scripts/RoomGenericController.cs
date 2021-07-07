using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenericController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wallAntyEnemyRight;
    public GameObject wallAntyEnemyLeft;
    public GameObject wallAntyEnemyUp;
    public GameObject wallAntyEnemyDown;

    public List<GameObject> Enemies;

    private Color originalWallAntyEnemyColor;
    void Start()
    {
        GameManager.game.contEnemies = Enemies.Count;
        originalWallAntyEnemyColor = wallAntyEnemyRight.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.game.contEnemies <= 0)
        {
            openDoors();
        }
    }
    
    public void blockDoors()
    {
        wallAntyEnemyRight.GetComponent<SpriteRenderer>().color = new Color(111f, 111f, 111f, 1f);
        wallAntyEnemyRight.layer = 0;
        wallAntyEnemyLeft.GetComponent<SpriteRenderer>().color = new Color(111f, 111f, 111f, 1f);
        wallAntyEnemyLeft.layer = 0;
        wallAntyEnemyUp.GetComponent<SpriteRenderer>().color = new Color(111f, 111f, 111f, 1f);
        wallAntyEnemyUp.layer = 0;
        wallAntyEnemyDown.GetComponent<SpriteRenderer>().color = new Color(111f, 111f, 111f, 1f);
        wallAntyEnemyDown.layer = 0;
    }

    public void blockEntry(int dir)
    {
        switch (dir)
        {
            case 0:
                wallAntyEnemyRight.GetComponent<SpriteRenderer>().color = new Color(111f, 111f, 111f, 1f);
                wallAntyEnemyRight.layer = 0;
                break;
            case 1:
                wallAntyEnemyLeft.GetComponent<SpriteRenderer>().color = new Color(111f, 111f, 111f, 1f);
                wallAntyEnemyLeft.layer = 0;
                break;
            case 2:
                wallAntyEnemyUp.GetComponent<SpriteRenderer>().color = new Color(111f, 111f, 111f, 1f);
                wallAntyEnemyUp.layer = 0;
                break;
            case 3:
                wallAntyEnemyDown.GetComponent<SpriteRenderer>().color = new Color(111f, 111f, 111f, 1f);
                wallAntyEnemyDown.layer = 0;
                break;
        }

    }

    public void openDoors()
    {
        Debug.Log("desbloquear puerdoors");
        wallAntyEnemyRight.GetComponent<SpriteRenderer>().color = GameManager.game.colorWallAntiEnemy;
        wallAntyEnemyRight.layer = 7;
        wallAntyEnemyLeft.GetComponent<SpriteRenderer>().color = GameManager.game.colorWallAntiEnemy;
        wallAntyEnemyLeft.layer = 7;
        wallAntyEnemyUp.GetComponent<SpriteRenderer>().color = GameManager.game.colorWallAntiEnemy;
        wallAntyEnemyUp.layer = 7;
        wallAntyEnemyDown.GetComponent<SpriteRenderer>().color = GameManager.game.colorWallAntiEnemy;
        wallAntyEnemyDown.layer = 7;
    }

}
