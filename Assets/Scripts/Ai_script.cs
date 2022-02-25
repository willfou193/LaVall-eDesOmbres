using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Ai_script : MonoBehaviour
{
    public GameObject MaDestination;
    NavMeshAgent navAgent;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        navAgent.SetDestination(MaDestination.transform.position); //la destination doit être un Vector3 
        print(navAgent.velocity.magnitude);  //imprime la vitesse de déplacement de l’agent
    }
}

