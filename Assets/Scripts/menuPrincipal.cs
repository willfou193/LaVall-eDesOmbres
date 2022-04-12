using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
    public void Jouer()//void pour load scene de jeu quand on click sur jouer
    {
        SceneManager.LoadScene(1);
    }

    public void quitter()//void pour quitter le jeu entirement 
    {
        Debug.Log("quitter");
        Application.Quit();
    }
}
