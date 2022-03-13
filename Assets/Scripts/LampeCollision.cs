using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampeCollision : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider infoCol)
    {
        if(infoCol.gameObject.tag == "monstre" && Ai_script.InvulnerableEtourdi == false)
        {
            Ai_script.InvulnerableEtourdi = true;
            infoCol.gameObject.GetComponent<Ai_script>().AppelerFonctionResetInvul(); // appel une fonction du script  ai_script
            infoCol.gameObject.GetComponent<Ai_script>().navAgent.speed = 0;
            print("Arr�te toi!");

        }

    }

}
