using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonRiviere : MonoBehaviour
{
    public GameObject joueur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(joueur.transform.position.x, transform.position.y, transform.position.z);
    }
}
