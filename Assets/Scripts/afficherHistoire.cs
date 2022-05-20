using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class afficherHistoire : MonoBehaviour
{
    //Ce script gère l'affichage de la bonne histoire en fonction de la lettre prise dans le jeu.
    // -Julien et William
    public GameObject[] lettres; // tableau des lettres (histoire)
    bool lettreActive = false;
    int nombre;

    public void AfficherBonnePage(string lettre)
    {
        menuPause.JeuPause = true;
        lettreActive = true;
        lettre = lettre.Substring(6, 1); //il ne reste que le chiffre
        //print(lettre.Substring(6,1)); retourne 1
        int.TryParse(lettre, out nombre); // passe de string à int
        lettres[nombre].SetActive(true); // active la bonne lettre.
    }
    private void Update()
    {
        //
         if(lettreActive && Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)){
             lettreActive = false;
             for(int i =0; i < 6; i++){
                 lettres[i].SetActive(false);
             }
        }
    }
}
