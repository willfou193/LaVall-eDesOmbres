using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barriere : MonoBehaviour
{
    public AudioClip generatricePart;
    public AudioClip generatriceEnMarche;
    public GameObject porte;
    public void OuvrirPorte()
    {
        porte.transform.Rotate(0f, 90f, 0f);
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
