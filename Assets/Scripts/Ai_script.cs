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
    
    public float tempsInvulnerable;

    public NavMeshAgent navAgent; //réfère au navMeshAgent
    public Transform[] waypoints; //Tableau des waypoints
    int numWaypoint = 0; //index
  
    public static bool enChasse = false; // indique si le monstre poursuit le joueur
    public static bool InvulnerableEtourdi = false; // indique si le monstre peut être étourdi de nouveau


    void Start()
    {
        enChasse = false; // Au début, il ne le poursuit pas
        // navAgent = GetComponent<NavMeshAgent>(); // assossit le navAgent au component
        navAgent.SetDestination(waypoints[0].position); // comment par le premier waypoint
    }
    void Update()
    {
        // renvoie lien si un object se trouve entre le joueur et le monstre
        RaycastHit lien;
        Physics.Linecast(transform.position, joueur.transform.position, out lien);
        if(enChasse && !(lien.transform.tag =="terrain")){
            navAgent.SetDestination(joueur.transform.position); //poursuit le joueur
        }
        if(InvulnerableEtourdi){
            Invoke("resetInvulnerabiliteEtourdi", tempsInvulnerable);
        }
        
    }
    // Cette fonction se fait appeler après que le monstre ait touché un waypoint
    public void AllerAuProchainPoint() {
    if(!enChasse && numWaypoint <= waypoints.Length){
        navAgent.SetDestination(waypoints[numWaypoint].position); //poursuit le prochain waypoint
        }
    }

    public void AppelerFonctionResetInvul(){
        Invoke("LeMonstreCourt",5f);
    }
    public void resetInvulnerabiliteEtourdi(){
        InvulnerableEtourdi = false;
        print("ÇA MARCHHHHHHHHE");
    }
    public void LeMonstreCourt()
    {
       navAgent.speed = 3f;
    }

    private void OnTriggerEnter(Collider InfoCol) {
        if(InfoCol.gameObject.tag == "waypoint"){ //si le monstre touche un waypoint
            numWaypoint += 1; //index augmente de 1
            if(numWaypoint == waypoints.Length){ //s'il dépace le waypointMax + 1 il revient à 0
                numWaypoint = 0;
            }
            AllerAuProchainPoint(); // appel la fonction AllerAuprochainPoint
        }
    }
}

