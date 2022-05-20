using UnityEngine;

public class textLookAt : MonoBehaviour
{
    // Petit script qui active le texte d'un object lorsqu'il s'en rapproche.
    // -WIlliam
    public Transform joueur;
    void Update()
    {
        float dist = Vector3.Distance(transform.position, joueur.position); // distance entre le joueur et le texte
        if(dist < 30f){
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.transform.LookAt(joueur);   
        }
        else{
             gameObject.GetComponent<Renderer>().enabled = false;
        }

        
    }
}
