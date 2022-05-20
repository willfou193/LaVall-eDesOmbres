using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonRiviere : MonoBehaviour
{   //Declarer une reference du joueur pour acceder a sa position
    public GameObject joueur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //prendre la position en x du joueur et l'affecter à la position en x de l'audioSource de la rivière
        transform.position = new Vector3(joueur.transform.position.x, transform.position.y, transform.position.z);
    }
}
