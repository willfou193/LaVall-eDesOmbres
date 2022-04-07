using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPause : MonoBehaviour
{

    public static bool JeuPause = false;

    public GameObject menuPauseUI;

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (JeuPause)
            {
                Continuer();
            }
            else
            {
                Pause();
            }
        } 
    }

    public void Continuer()
    {
        menuPauseUI.SetActive(false);
        Time.timeScale = 1f;
        JeuPause = false;
    }

    void Pause()
    {
        menuPauseUI.SetActive(true);
        Time.timeScale = 0f;
        JeuPause = true;
    }

    public void Quitter()
    {
        Debug.Log("au menu");
        SceneManager.LoadScene(0);
    }
}
