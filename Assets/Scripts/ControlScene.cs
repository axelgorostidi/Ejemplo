using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControlScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLevel(int idx)
    {
        if (idx == 4)
        {
            Application.Quit();
            return;
        }
        SceneManager.LoadScene(idx);
    }
}
