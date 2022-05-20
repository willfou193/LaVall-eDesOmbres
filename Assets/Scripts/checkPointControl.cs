using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointControl : MonoBehaviour
{
    // Ce script gère les checkpoint à travers les morts du joueur.
    // -William
    private static checkPointControl instance;
    public Vector3 dernierCheckPoint;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
