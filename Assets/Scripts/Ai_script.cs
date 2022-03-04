using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Ai_script : MonoBehaviour
{
    // Script qui gère le comportement des monstres du jeu.
    // Par défault, il suit un chemain et si le joueur apparait
    // dans son champs de vision, il le poursuit. S'il disparait pour
    // plus de X secondes, il revient sur son chemain.
    public GameObject joueur; // réfère au joueur
    public NavMeshAgent navAgent; //réfère au navMeshAgent
    public Transform[] waypoints; //Tableau des waypoints
    int numWaypoint = 0; //index
    public static bool enChasse = false; // indique si le monstre poursuit le joueur

    void Start()
    {
        enChasse = false; // Au début, il ne le poursuit pas
        navAgent = GetComponent<NavMeshAgent>(); // assossit le navAgent au component
        navAgent.SetDestination(waypoints[0].position); // comment par le premier waypoint
    }
    void Update()
    {
        if(enChasse){
            navAgent.SetDestination(joueur.transform.position); //poursuit le joueur
        } 
        
    }
    // Cette fonction se fait appeler après que le monstre ait touché un waypoint
    void AllerAuProchainPoint() {
    if(!enChasse && numWaypoint <= 3){
        navAgent.SetDestination(waypoints[numWaypoint].position); //poursuit le prochain waypoint
        print("Je vais au point" + waypoints[numWaypoint]);
        }
    }

    
    private void OnTriggerEnter(Collider InfoCol) {
        if(InfoCol.gameObject.tag == "waypoint"){ //si le monstre touche un waypoint
            numWaypoint += 1; //index augmente de 1
            if(numWaypoint == 4){ //s'il dépace le waypointMax + 1 il revient à 0
                numWaypoint = 0;
            }
            AllerAuProchainPoint(); // appel la fonction AllerAuprochainPoint
            print(numWaypoint);
    }
        
    }
}

