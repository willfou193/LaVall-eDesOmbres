using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class afficherHistoire : MonoBehaviour
{
    public GameObject[] lettres; // tableau des lettres (histoire)
    bool lettreActive = false;
    int nombre;

    public void AfficherBonnePage(string lettre)
    {
        menuPause.JeuPause = true;
        lettreActive = true;
        lettre = lettre.Substring(6, 1); //il ne reste que le chiffre
        //print(lettre.Substring(6,1)); retourne 1
        int.TryParse(lettre, out nombre);
        lettres[nombre].SetActive(true);
    }
    private void Update()
    {
         if(lettreActive && Input.GetKeyDown(KeyCode.Escape)){
             lettreActive = false;
             for(int i =0; i < 6; i++){
                 lettres[i].SetActive(false);
             }
        }
    }
}
