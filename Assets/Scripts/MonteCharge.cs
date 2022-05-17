using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonteCharge : MonoBehaviour
{
    public AudioClip monteCharge;
    public void MonterCharge(){
        print("j'arrive ici");
        gameObject.GetComponent<AudioSource>().PlayOneShot(monteCharge);
        gameObject.GetComponent<Animator>().SetBool("monteAscenceur", true);
        //Mettre l'animation du monte charge
    }
}
