using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
public class santeMentale : MonoBehaviour
{
    public float rayonCol; // rayon du cercle de collision
    // le nombre inscrit a tendence à rapprocher les dégat/s à 1m, de ce nombre. EX: 30f = 30 degats/s si la distance est 1m
    public float degatMinDistance; 
    // le nombre inscrit a tendance à rapprocher les dégats à MAX distance de ce nombre. EX: 4f = 4 degats/s si la distance est 20m
    public float degatMaxDistance;
    public float regenerationSanteMentale; // vitesse auquel le feu redonner de la sante mentale
    float sanite = 100f; // santé mentale
    float santeMentaleMax = 100f; // maximum que les feux de camps ne peuvent pas dépassé
    public Volume volume; //réfère au Volume post-processing
    public Vignette _Vignette; // Post-processing vignettage
    public FilmGrain _Grain; // Post-processing vignettage
    public Text santeMentaleUi;

    public void Start() {
        volume.profile.TryGet<Vignette>(out _Vignette);
        _Vignette.intensity.value = 0;
        volume.profile.TryGet<FilmGrain>(out _Grain);
        _Grain.intensity.value = 0;
    }

    void Update()
    {   //créer un cercle autour du joueur et créer un tableau de collider de ce qu'il touche
        Collider[] objectsDansCercle = Physics.OverlapSphere(gameObject.transform.position, rayonCol); 
        foreach (var objectTouchee in objectsDansCercle) // pour chaque object dans le cercle 
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
                        sanite -= ((Mathf.Pow(distance, -.7f)* degatMinDistance) + degatMaxDistance) * Time.deltaTime;
                    }
                    //print(distance + "Mètre est la distance et la santé mentale est de " + sanite);
                }
            }
            if(objectTouchee.gameObject.tag =="effetSable")
            {
                print("Je touche une zone de sable!");
            }
        }
        _Vignette.intensity.value = -0.008f * sanite + 0.8f; // renvoie le niveau de la santé mentale l'intensité voulu max
        _Grain.intensity.value = -0.01f * sanite + 1f; // renvoie le niveau de la santé mentale l'intensité voulu max
        santeMentaleUi.text = Mathf.RoundToInt(sanite).ToString() + "%"; //On affiche la sante mentale en texte
       


    }//fin du update

    private void OnTriggerStay(Collider infoCol)
    {
        if(infoCol.gameObject.tag == "feuDeCamp")
        {
            if(sanite <= santeMentaleMax)
            {

                //print("je suis dans la zone du feu et ma santé mentale est de " + sanite);
                sanite += regenerationSanteMentale * Time.deltaTime;
            }
        }
    }
}
