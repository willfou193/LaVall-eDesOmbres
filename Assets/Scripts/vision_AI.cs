using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vision_AI : MonoBehaviour {

    //Script gèrant la vision du monstre ainsi que le temps de réinitialisation
    // avant qu'il retourne sur son chemain de base.
    public GameObject AI; // référence au monstre
    public int tempsReset = 5; // temps avant que le monstre retourne sur son chemain
    
    //lorsque que le monstre détecte le joueur, il est en chasse continuellement.
    private void OnTriggerStay(Collider InfoCol)
    {
        if(InfoCol.gameObject.tag == "Player")
        {
            Ai_script.enChasse = true; // retourne la variable enChasse a true dans le script Ai_script
            CancelInvoke("ReinitialisationChasse"); //rafraîchit le temps avant que le monstre retourne sur son chemain
        }
    }
    //Lorsque le joueur quitte le cone de vision, invoque la fonction qui réinitialise le pathfinder du monstre.
    private void OnTriggerExit(Collider InfoCol)
    {
        if (InfoCol.gameObject.tag == "Player")
        {
            Invoke("ReinitialisationChasse", tempsReset); 
        }
    }
    
    //le monstre à perdu de vision le joueur, il retourne donc à son chemin de ronde.
    void ReinitialisationChasse()
    {
        Ai_script.enChasse = false;
        AI.GetComponent<Ai_script>().AllerAuProchainPoint();
    }
}
