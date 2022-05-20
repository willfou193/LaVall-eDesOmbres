using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPause : MonoBehaviour
{

    //Ce script gère le fonctionnement du menu pause sur le joueur et son environnemnt.
    // -christopher -william

    public static bool JeuPause = false;

    public GameObject menuPauseUI;

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;//unlock la souris quand on press la touch echapper
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

    public void Continuer()//public pour continuer la partie
    {
        menuPauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;//faire d�rouller la partie a temps normale
        JeuPause = false;
        Cursor.lockState = CursorLockMode.Locked;//barr� la souris quand on touch �chapper
    }

    void Pause()//public pour arreter la partie
    {
        menuPauseUI.SetActive(true);
        //Time.timeScale = 0f;
        
        JeuPause = true;
    }

    public void Quitter()//quand click sur le btn quitter ,rapporter le joueur au menu principale
    {
        Debug.Log("au menu");//debug pour voir si le btn marche dans unity
        SceneManager.LoadScene(0);
    }
}
