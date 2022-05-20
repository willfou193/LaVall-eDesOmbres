using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    //Ce script gère les checkpoint afin d'attribuer une nouvelle position d'apparition lors de la mort
    // -William
    private checkPointControl checkPoCtrl;
    // Start is called before the first frame update
    void Start()
    {
        checkPoCtrl = GameObject.FindGameObjectWithTag("checkPoCtrl").GetComponent<checkPointControl>(); // se ratache à la variable static qui ne détruit pas lors de la mort
    }
    //Lorsque le joueur entre en contacte avec un nouveau checkpoint, son point de réaparition devient l'emplacement du checkpoint
    private void OnTriggerEnter(Collider infoCol)
    {
        if (infoCol.CompareTag("Player"))
        {
            checkPoCtrl.dernierCheckPoint = transform.position;
        }
    }
}
