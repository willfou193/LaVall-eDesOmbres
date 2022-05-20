using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuPrincipal : MonoBehaviour
{
    // script fait pas Christ Et William, lorsque que je joueur appuis sur JOUER, l'animation du d�but du jeu se lance
    // et on d�sactive tout le reste
    public Canvas canvas; // r�f�rence au canva
    public Animator mainCamAnim;
    public AudioClip bruitage;
    public AudioClip musique;
    private void Start() {
        gameObject.GetComponent<AudioSource>().clip = musique;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void Jouer()//void pour load scene de jeu quand on click sur jouer
    {
        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().PlayOneShot(bruitage);
        mainCamAnim.SetBool("animDebut", true);
        Invoke("LancerPartie", 19f); //on appel la fonction pour charger la scene principale
        canvas.enabled = false; // d�sactive le canvas
        
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
