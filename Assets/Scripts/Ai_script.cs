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
<<<<<<< HEAD
        if(enChasse){
            navAgent.SetDestination(joueur.transform.position); //poursuit le joueur
        } 
        
    }
    // Cette fonction se fait appeler après que le monstre ait touché un waypoint
    void AllerAuProchainPoint() {
    if(!enChasse && numWaypoint <= waypoints.Length){
        navAgent.SetDestination(waypoints[numWaypoint].position); //poursuit le prochain waypoint
        print("Je vais au point" + waypoints[numWaypoint]);
        }
    }

    
    private void OnTriggerEnter(Collider InfoCol) {
        if(InfoCol.gameObject.tag == "waypoint"){ //si le monstre touche un waypoint
            numWaypoint += 1; //index augmente de 1
            if(numWaypoint == waypoints.Length)
            { //s'il dépace le waypointMax + 1 il revient à 0
                numWaypoint = 0;
            }
            AllerAuProchainPoint(); // appel la fonction AllerAuprochainPoint
            print(numWaypoint);
    }
        
=======
        navAgent.SetDestination(MaDestination.transform.position); //la destination doit �tre un Vector3 
        print(navAgent.velocity.magnitude);  //imprime la vitesse de d�placement de l�agent
>>>>>>> parent of 29593d9 (AI suit waypoints)
    }
}

