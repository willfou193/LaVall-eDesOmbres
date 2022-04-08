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
        Time.timeScale = 1f;//faire dérouller la partie a temps normale
        JeuPause = false;
        Cursor.lockState = CursorLockMode.Locked;//barré la souris quand on touch échapper
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
