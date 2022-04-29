using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonteCharge : MonoBehaviour
{
    public AudioClip monteCharge;
    public void MonterCharge(){
        gameObject.GetComponent<AudioSource>().PlayOneShot(monteCharge);
        //Mettre l'animation du monte charge
    }
}
