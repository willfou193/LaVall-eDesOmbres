using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuPrincipal : MonoBehaviour
{
    public Canvas canvas;
    public Animator mainCamAnim;

    public void Jouer()//void pour load scene de jeu quand on click sur jouer
    {
        mainCamAnim.SetBool("animDebut", true);
        Invoke("LancerPartie", 19f);
        canvas.enabled = false;
        gameObject.GetComponent<AudioSource>().volume = 0f;
    }

    public void quitter()//void pour quitter le jeu entirement 
    {
        Debug.Log("quitter");
        Application.Quit();
    }
    void LancerPartie() {
        SceneManager.LoadScene(1);
    }
}
