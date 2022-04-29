using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barriere : MonoBehaviour
{
    public AudioClip generatricePart;
    public AudioClip generatriceEnMarche;
    public void OuvrirPorte()
    {
        print("la porte s'ouvre");
    }
    public void PartirGenerateur(){
        gameObject.GetComponent<AudioSource>().PlayOneShot(generatricePart);
        Invoke("LancerSonIdle", 3f);
    }

    public void LancerSonIdle(){
        gameObject.GetComponent<AudioSource>().clip = generatriceEnMarche;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
