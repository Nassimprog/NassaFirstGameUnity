using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainmenu;
    public void Start()
    {
        
        
    }
    public void PlayGame()
    {
        
        SceneManager.LoadScene("firstGo");
        RubyController.ammo = 3;
        RubyController.ExtraHealth = 0;
        RubyController.FixedRobots = 0;
        RubyController.HasCrowbar = false;
        RubyController.HasHealthSatchel = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        
    }

   
}
