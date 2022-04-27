using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
public class santeMentale : MonoBehaviour
{
    public float rayonCol; // rayon du cercle de collision
    // le nombre inscrit a tendence à rapprocher les dégat/s à 1m, de ce nombre. EX: 30f = 30 degats/s si la distance est 1m
    public float degatMinDistance; 
    // le nombre inscrit a tendance à rapprocher les dégats à MAX distance de ce nombre. EX: 4f = 4 degats/s si la distance est 20m
    public float degatMaxDistance;
    float distancePlusProche;
    public float regenerationSanteMentale; // vitesse auquel le feu redonner de la sante mentale
    public float sanite = 100f; // santé mentale
    float santeMentaleMax = 100f; // maximum que les feux de camps ne peuvent pas dépassé
    public Volume volume; //réfère au Volume post-processing
    public Vignette _Vignette; // Post-processing vignettage
    public FilmGrain _Grain; // Post-processing vignettage
    public Text santeMentaleUi;
    //Son de toutes les musiques de chasse
    public AudioSource audio;
    public AudioClip chasse1;
    public AudioClip chasse2;
    public AudioClip chasse3;
    public AudioClip chasse4;
    public bool sonChassePeutJoue = true;
    bool JoueChasse1;
    bool JoueChasse2;
    bool JoueChasse3;
    bool JoueChasse4;


    public void Start() {
        volume.profile.TryGet<Vignette>(out _Vignette);
        _Vignette.intensity.value = 0;
        volume.profile.TryGet<FilmGrain>(out _Grain);
        _Grain.intensity.value = 0;
    }

    void Update()
    {   
        float distanceEnnemiPlusProche = Mathf.Infinity; // distance "de base"
        Collider ennemiPlusProche = null;
        //créer un cercle autour du joueur et créer un tableau de collider de ce qu'il touche
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
                    if(distance < distanceEnnemiPlusProche){
                        distanceEnnemiPlusProche = distance;
                        ennemiPlusProche = objectTouchee;
                        if (ennemiPlusProche.gameObject.GetComponent<Ai_script>().enChasse == true) {
                            audio.loop = true;
                            print("Je recherche quoi jouer");
                            if (distanceEnnemiPlusProche < rayonCol && distanceEnnemiPlusProche > rayonCol / 4 * 3 && !JoueChasse1) {//le 1/4 le plus loin
                                JoueChasse1 = true;
                                JoueChasse2 = false;
                                JoueChasse3 = false;
                                audio.clip = chasse1;
                                audio.Play();
                                JoueChasse4 = false;
                                print("zone 1 activé");
                            }
                            if (distanceEnnemiPlusProche < rayonCol / 4 * 3 && distanceEnnemiPlusProche > rayonCol / 4 * 2 && !JoueChasse2) {// le 2/4 le plus loin
                                JoueChasse1 = false;
                                JoueChasse2 = true;
                                JoueChasse3 = false;
                                JoueChasse4 = false;
                                audio.clip = chasse2;
                                audio.Play();
                                print("zone 2 activé");
                            }
                            if (distanceEnnemiPlusProche < rayonCol / 4 * 2 && distanceEnnemiPlusProche > rayonCol / 4 * 1 && !JoueChasse3) {// le 2/4 le plus proche
                                JoueChasse1 = false;
                                JoueChasse2 = false;
                                JoueChasse3 = true;
                                JoueChasse4 = false;
                                audio.clip = chasse3;
                                audio.Play();
                                print("zone 3 activé");
                            }
                            if (distanceEnnemiPlusProche < rayonCol / 4 && distanceEnnemiPlusProche > 0.1f && !JoueChasse4) {// le 1/4 le plus proche
                                JoueChasse1 = false;
                                JoueChasse2 = false;
                                JoueChasse3 = false;
                                JoueChasse4 = true;
                                audio.clip = chasse4;
                                audio.Play();
                                print("zone 4 activé");
                            }
                        }
                        else{
                            audio.loop = false;
                        }
                    }
                    if(sanite >= 0f){ // si la santé mentale n'est pas à 0
                        //La santé mentale diminue selon une fonction voir ici: https://www.desmos.com/calculator/2jjemrx9vn?lang=fr
                        //sanite -= ((Mathf.Pow(distance, -.7f)* degatMinDistance) + degatMaxDistance) * Time.deltaTime;
                    }
                }
            }
            if(objectTouchee.gameObject.tag =="effetSable")
            {

                //print("Je touche une zone de sable!");
                objectTouchee.gameObject.SetActive(true);
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


/*RaycastHit lien;
Physics.Linecast(transform.position, objectTouchee.transform.position, out lien); // on trace une ligne entre moi et les monstres dans le cercle
if (!(lien.transform.tag == "terrain")) // s'il n'y a pas de terrain entre nous,
{
    float distance = Vector3.Distance(objectTouchee.transform.position, transform.position); // renvoie la distance entre moi et les monstres
    if (distance < distanceEnnemiPlusProche)
    {
        distanceEnnemiPlusProche = distance;
        ennemiPlusProche = objectTouchee;
        if (ennemiPlusProche.gameObject.GetComponent<Ai_script>().enChasse == true)
        {
            audio.loop = true;
            print("Je recherche quoi jouer");
            if (distanceEnnemiPlusProche < rayonCol && distanceEnnemiPlusProche > rayonCol / 4 * 3 && !audio.clip == chasse1)
            {//le 1/4 le plus loin
                audio.Stop();
                audio.clip = chasse1;
                audio.Play();
            }
            if (distanceEnnemiPlusProche < rayonCol / 4 * 3 && distanceEnnemiPlusProche > rayonCol / 4 * 2 && !audio.clip == chasse2)
            {// le 2/4 le plus loin
                audio.Stop();
                audio.clip = chasse2;
                audio.Play();

            }
            if (distanceEnnemiPlusProche < rayonCol / 4 * 2 && distanceEnnemiPlusProche > rayonCol / 4 * 1 && !audio.clip == chasse3)
            {// le 2/4 le plus proche
                audio.Stop();
                audio.clip = chasse3;
                audio.Play();
            }
            if (distanceEnnemiPlusProche < rayonCol / 4 && distanceEnnemiPlusProche > 0.1f && !audio.clip == chasse4)
            {// le 1/4 le plus proche
                audio.Stop();
                audio.clip = chasse4;
                audio.Play();
            }
        }
        else
        {
            audio.loop = false;
        }*/