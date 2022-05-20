using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampeCollision : MonoBehaviour
{
   // Gère la collision de la lampe avec les ennemis en réduisant leur vitesse à 0 lorsque la lampe les touchent
   // -William
    private void OnTriggerEnter(Collider infoCol)
    {
        // si le collider de la lampe touche un monstre et qu'il n'est pas invulnérable à être étourdi,
        if(infoCol.gameObject.tag == "monstre" && infoCol.gameObject.GetComponent<Ai_script>().invulnerableEtourdi == false)
        {
            infoCol.gameObject.GetComponent<Ai_script>().invulnerableEtourdi = true; // on le rend étourdi à nouveau
            infoCol.gameObject.GetComponent<Ai_script>().AppelerFonctionResetInvul(); // appel une fonction du script  ai_script
            infoCol.gameObject.GetComponent<Ai_script>().navAgent.speed = 0; // on réduit sa vitesse à 0

        }

    }

}
