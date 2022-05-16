using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lettres : MonoBehaviour
{
    public GameObject[] lettres;
    int numLettre;
    bool lettreActive;

    public void AfficherBonnePage(string lettre){

        lettre.Substring(6,0);
        int.Parse(numLettre);
        lettres[numLettre].SetActive(true);
    }
}
