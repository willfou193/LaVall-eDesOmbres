using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
    public void Jouer()
    {
        SceneManager.LoadScene(1);
    }

    public void quitter()
    {
        Debug.Log("quitter");
        Application.Quit();
    }
}
