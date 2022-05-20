using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonteCharge : MonoBehaviour
{
    // Ce script active les éléments en lien avec le monte-charge
    // -William
    public AudioClip monteCharge;
    public void MonterCharge(){
        gameObject.GetComponent<AudioSource>().PlayOneShot(monteCharge);
        gameObject.GetComponent<Animator>().SetBool("monteAscenceur", true);
        //Mettre l'animation du monte charge
    }
}
