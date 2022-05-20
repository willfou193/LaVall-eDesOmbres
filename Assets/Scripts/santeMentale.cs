using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Audio;

public class santeMentale : MonoBehaviour
{
    // Les ennemis font déscendre la santé mentale du joueur (William) et des sons de chasse jouent en fonction de la distance (Julien)
    
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
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public AudioSource audio4;
    //clip de toutes les musiques de chasse
    public AudioClip chasse1;
    public AudioClip chasse2;
    public AudioClip chasse3;
    public AudioClip chasse4;
    //Audio mixer pour les musiques de chasse
    public AudioMixerGroup chs1;
    public AudioMixerGroup chs2;
    public AudioMixerGroup chs3;
    public AudioMixerGroup chs4;
    //floats pour les volumes des musiques de chasse
    float vl1;
    float vl2;
    float vl3;
    float vl4;

    //Bool pour gerer si la musique de chasse peut jouer et quelle musique jouer
    public bool sonChassePeutJoue = true;
    bool JoueChasse1;
    bool JoueChasse2;
    bool JoueChasse3;
    bool JoueChasse4;
    


    public void Start()
    {
        volume.profile.TryGet<Vignette>(out _Vignette);
        _Vignette.intensity.value = 0;
        volume.profile.TryGet<FilmGrain>(out _Grain);
        _Grain.intensity.value = 0;
    }

    void Update()
    {
        //definition de la variable qui indique le volume de chaque audio mixer
        chs1.audioMixer.GetFloat("Volume", out vl1);
        chs2.audioMixer.GetFloat("Volume", out vl2);
        chs3.audioMixer.GetFloat("Volume", out vl3);
        chs4.audioMixer.GetFloat("Volume", out vl4);
        //declarations des coroutines pour faire un fondu sonore sur les etapes des musiques de chasse
        Coroutine fondu1 = StartCoroutine(FonduSonore.StartFade(chs1, 0f, -80f));
        Coroutine fondu2 = StartCoroutine(FonduSonore.StartFade(chs2, 0f, -80f));
        Coroutine fondu3 = StartCoroutine(FonduSonore.StartFade(chs3, 0f, -80f));
        Coroutine fondu4 = StartCoroutine(FonduSonore.StartFade(chs4, 0f, -80f));

        float distanceEnnemiPlusProche = Mathf.Infinity; // distance "de base"
        Collider ennemiPlusProche = null;
        //créer un cercle autour du joueur et créer un tableau de collider de ce qu'il touche
        Collider[] objectsDansCercle = Physics.OverlapSphere(gameObject.transform.position, rayonCol);
        foreach (var objectTouchee in objectsDansCercle) // pour chaque object dans le cercle 
        {

            if (objectTouchee.gameObject.tag == "monstre") // on s'assure que les objets soient des monstres
            {
                RaycastHit lien;
                Physics.Linecast(transform.position, objectTouchee.transform.position, out lien); // on trace une ligne entre moi et les monstres dans le cercle
                if (!(lien.transform.tag == "terrain")) // s'il n'y a pas de terrain entre nous,
                {
                    float distance = Vector3.Distance(objectTouchee.transform.position, transform.position); // renvoie la distance entre moi et les monstres
                    if (distance < distanceEnnemiPlusProche)
                    { // on détecte et on associe le monstre le plus proche à une variable
                        distanceEnnemiPlusProche = distance;
                        ennemiPlusProche = objectTouchee;
                        if (ennemiPlusProche.gameObject.GetComponent<Ai_script>().enChasse == true)
                        { // par la suite, ce monstre sert de référence pour jouer une musique qui change selon la distance
                            //on demare le loop de la musique de chasse pour qu'elle continue de jouer meme quand son volume est a 0
                            audio1.loop = true;
                            audio2.loop = true;
                            audio3.loop = true;
                            audio4.loop = true;

                            if (distanceEnnemiPlusProche < rayonCol && distanceEnnemiPlusProche > (rayonCol / 4) * 3 && !JoueChasse1)
                            {//le 1/4 le plus loin
                                // seul chasse1 peut jouer
                                JoueChasse1 = true;
                                JoueChasse2 = false;
                                JoueChasse3 = false;
                                JoueChasse4 = false;
                                audio1.volume = 1;
                                vl1 = -4f;
                                chs1.audioMixer.SetFloat("Volume", vl1);
                                audio1.Play();
                                print("zone 1 activé");
                            }
                            else if (!JoueChasse1)
                            {
                                //on fais fondre chasse1 un jusqu'au silence
                                StartCoroutine(FonduSonore.StartFade(chs1, 0f, -80f));
                            }
                            if (distanceEnnemiPlusProche < (rayonCol / 4) * 3 && distanceEnnemiPlusProche > (rayonCol / 4) * 2 && !JoueChasse2)
                            {// le 2/4 le plus loin
                                // seul chasse2 peut jouer
                                JoueChasse1 = false;
                                JoueChasse2 = true;
                                JoueChasse3 = false;
                                JoueChasse4 = false;
                                audio2.volume = 1;
                                vl2 = -4f;
                                chs2.audioMixer.SetFloat("Volume", vl2);
                                audio2.Play();
                                print("zone 2 activé");
                            }
                            else if (!JoueChasse2)
                            {
                                //on fais fondre chasse2 un jusqu'au silence
                                StartCoroutine(FonduSonore.StartFade(chs2, 0f, -80f));
                            }
                            if (distanceEnnemiPlusProche < (rayonCol / 4) * 2 && distanceEnnemiPlusProche > (rayonCol / 4) * 1 && !JoueChasse3)
                            {// le 2/4 le plus proche
                                // seul chasse3 peut jouer
                                JoueChasse1 = false;
                                JoueChasse2 = false;
                                JoueChasse3 = true;
                                JoueChasse4 = false;
                                audio3.volume = 1;
                                vl3 = -4f;
                                chs3.audioMixer.SetFloat("Volume", vl3);
                                audio3.Play();
                                print("zone 3 activé");
                            }
                            else if (!JoueChasse3)
                            {
                                //on fais fondre chasse3 un jusqu'au silence
                                StartCoroutine(FonduSonore.StartFade(chs3, 0f, -80f));
                            }
                            if (distanceEnnemiPlusProche < rayonCol / 4 && distanceEnnemiPlusProche > 0.1f && !JoueChasse4)
                            {// le 1/4 le plus proche
                                // seul chasse4 peut jouer
                                JoueChasse1 = false;
                                JoueChasse2 = false;
                                JoueChasse3 = false;
                                JoueChasse4 = true;
                                audio4.volume = 1;
                                vl4 = -4f;
                                chs4.audioMixer.SetFloat("Volume", vl4);
                                audio4.Play();
                                print("zone 4 activé");

                            }
                            else if (!JoueChasse4)
                            {
                                //on fais fondre chasse4 un jusqu'au silence
                                StartCoroutine(FonduSonore.StartFade(chs4, 0f, -80f));
                            }

                        }
                        else
                        {
                            //comme le monstre n'est pas en chasse, on fais fondre toutes les musiques et on les arrete
                            audio1.loop = false;
                            audio2.loop = false;
                            audio3.loop = false;
                            audio4.loop = false;
                            audio1.Stop();
                            audio2.Stop();
                            audio3.Stop();
                            audio4.Stop();
                            StartCoroutine(FonduSonore.StartFade(chs1, 0f, -80f));
                            StartCoroutine(FonduSonore.StartFade(chs2, 0f, -80f));
                            StartCoroutine(FonduSonore.StartFade(chs3, 0f, -80f));
                            StartCoroutine(FonduSonore.StartFade(chs4, 0f, -80f));
                        }
                    }
                    if (sanite >= 0f && distanceEnnemiPlusProche < (rayonCol / 4) * 2)
                    { // si la santé mentale n'est pas à 0
                        //La santé mentale diminue selon une fonction voir ici: https://www.desmos.com/calculator/2jjemrx9vn?lang=fr
                        sanite -= ((Mathf.Pow(distance, -.7f) * degatMinDistance) + degatMaxDistance) * Time.deltaTime;
                    }
                }
            }
        }





        _Vignette.intensity.value = -0.008f * sanite + 0.8f; // renvoie le niveau de la santé mentale l'intensité voulu max
        _Grain.intensity.value = -0.01f * sanite + 1f; // renvoie le niveau de la santé mentale l'intensité voulu max
        santeMentaleUi.text = Mathf.RoundToInt(sanite).ToString() + "%"; //On affiche la sante mentale en texte



    }//fin du update

    public void ArreterCoroutines()
    {
        StopAllCoroutines();
    }
    private void OnTriggerStay(Collider infoCol)
    {
        if (infoCol.gameObject.tag == "feuDeCamp")
        {
            if (sanite <= santeMentaleMax)
            {

                //print("je suis dans la zone du feu et ma santé mentale est de " + sanite);
                sanite += regenerationSanteMentale * Time.deltaTime;
            }
        }
    }
}