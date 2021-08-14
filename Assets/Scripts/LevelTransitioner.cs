using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransitioner : MonoBehaviour
{

    public string level;
    public int RequiredRobots = 0;

   

    

    public void ChangeScene()
    {
            SceneManager.LoadScene(level);
    }
}
