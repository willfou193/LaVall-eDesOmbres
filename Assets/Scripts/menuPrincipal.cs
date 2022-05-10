using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuPrincipal : MonoBehaviour
{
    // script fait pas Christ Et William, lorsque que je joueur appuis sur JOUER, l'animation du début du jeu se lance
    // et on désactive tout le reste
    public Canvas canvas; // référence au canva
    public Animator mainCamAnim;

    public void Jouer()//void pour load scene de jeu quand on click sur jouer
    {
        mainCamAnim.SetBool("animDebut", true);
        Invoke("LancerPartie", 19f); //on appel la fonction pour charger la scene principale
        canvas.enabled = false; // désactive le canvas
        gameObject.GetComponent<AudioSource>().volume = 0f;
    }

    public void quitter()//void pour quitter le jeu entirement 
    {
        Debug.Log("quitter");
        Application.Quit(); // on quitte le jeu
    }
    void LancerPartie() {
        SceneManager.LoadScene(1);
    }
}
