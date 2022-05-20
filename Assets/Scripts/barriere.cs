using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barriere : MonoBehaviour
{
    public AudioClip generatricePart; // son de départ de la génératrice
    public AudioClip generatriceEnMarche; // son qui boucle
    public GameObject porte; // la porte qui ouvre
    public GameObject[] lumieres; // les lumière de la scène
    public void OuvrirPorte()
    {
        porte.transform.Rotate(0f, 90f, 0f); // On tourne la cloture pour laisser passer le joueur
    }
    public void PartirGenerateur(){
        gameObject.GetComponent<AudioSource>().PlayOneShot(generatricePart);
        Invoke("LancerSonIdle", 3f); // appel de la fonction qui joue la musique en boucle
    }

    public void LancerSonIdle(){
        gameObject.GetComponent<AudioSource>().clip = generatriceEnMarche;
        gameObject.GetComponent<AudioSource>().Play();
        //On active toutes les lumières
        for(int i =0; i < 10; i++){
                 lumieres[i].SetActive(true);
             }
    }
}
