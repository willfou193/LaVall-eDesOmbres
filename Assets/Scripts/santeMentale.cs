using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class santeMentale : MonoBehaviour
{
    public float rayonCol = 10f; // rayon du cercle de collision
    // le nombre inscrit a tendence à rapprocher les dégat/s à 1m, de ce nombre. EX: 30f = 30 degats/s si la distance est 1m
    public float degatMinDistance; 
    // le nombre inscrit a tendance à rapprocher les dégats à 20m de ce nombre. EX: 4f = 4 degats/s si la distance est 20m
    public float degatMaxDistance;
    float sanite = 100; // santé mentale
    float santeMentaleMax = 100; // maximum que les feux de camps ne peuvent pas dépassé
   
    void Update()
    {   //créer un cercle autour du joueur et créer un tableau de collider de ce qu'il touche
        Collider[] objectsDansCercle = Physics.OverlapSphere(gameObject.transform.position, rayonCol); 
        foreach (var objectTouchee in objectsDansCercle) 
        {
            if(objectTouchee.gameObject.tag == "monstre") // on s'assure que les objets soient des monstres
            {
                RaycastHit lien;
                Physics.Linecast(transform.position, objectTouchee.transform.position, out lien); // on trace une ligne entre moi et les monstres dans le cercle
                if(!(lien.transform.tag == "terrain")) // s'il n'y a pas de terrain entre nous,
                {
                    float distance = Vector3.Distance(objectTouchee.transform.position, transform.position); // renvoie la distance entre moi et les monstres
                    if(sanite >= 0f){ // si la santé mentale n'est pas à 0
                        //La santé mentale diminue selon une fonction voir ici: https://www.desmos.com/calculator/2jjemrx9vn?lang=fr
                        sanite -= (Mathf.Pow(distance, -.7f)* degatMinDistance) + degatMaxDistance * Time.deltaTime;
                    }
                   
                    print(distance + " est la distance et la " + " santé mentale est de " + sanite);
                    //descendre la sant� mental
                }
            }
            
        }
    }//fin du update
}
