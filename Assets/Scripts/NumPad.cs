using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NumPad : MonoBehaviour
{
    /*
     Ce script g�re le Numpad dans le monte charge. Le script g�re les boutons de 0 � 9 ainsi que retour et entrer
     */
    string code = "";
    public string bonneReponse;
    int nombreDeNumMax = 4;
    public Text codeText;
    public Text noteCode;
    public GameObject numpad;
    public GameObject monteCharge;
    public GameObject joueur;
    public static bool gagne = false;
    public AudioClip sonbouton;
    public AudioClip musiqueFin;
    // objet à cacher lors de l'animation de fin
    public Image cachePad;
    public GameObject objet1;
    public GameObject objet2;
    public GameObject objet3;
    public Canvas canvas;
    public GameObject textCredit;
    public GameObject imageCredit;

    private void Start()
    {
        //bonneReponse = Random.Range(0,9).ToString();

        //For loop cr�ant un code al�atoire au d�but de la partie
        for (var i = 0; i < nombreDeNumMax;i++)
        {
                bonneReponse += Random.Range(0, 9).ToString(); //On cr�er un chiffre al�atoire et on le concat�ne � la string
        }
        print("la bonne r�ponse est " + bonneReponse);
        noteCode.text = bonneReponse;
        numpad.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            print("Je ferme le menu");
            numpad.SetActive(false); 
            menuPause.JeuPause = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    public void BoutonAppuyer()
    {
        monteCharge.GetComponent<AudioSource>().PlayOneShot(sonbouton);
        string numero = EventSystem.current.currentSelectedGameObject.name; //renvoie le numero du bouton appuy�
        AjouterNum(numero); //Appel de la fonction avec le numero du bouton
    }
    public void AjouterNum(string num) 
    {
        //si le code n'est pas � son maximum,
        if(code.Length < nombreDeNumMax)
        {
            code += num; // on ajoute le numero au code
            codeText.text = code; //et on l'affiche
        }
    }
    public void EnleverNum()
    {
        //Si le joueur clique sur return, on enl�ve un numero du code
        if(code.Length > 0)
        {
            code = code.Remove(code.Length - 1);
            codeText.text = code;
        }
    }
    public void ValiderCode()
    {
        if(code == bonneReponse)
        {
            monteCharge.GetComponent<MonteCharge>().MonterCharge();
            joueur.GetComponent<Animator>().SetBool("animFin", true);
            joueur.GetComponent<Animator>().enabled = true;
            print("Fin du jeu");
            //activer la scin�matique
            gagne = true;
            cachePad.enabled = false;
            codeText.enabled = false;
            objet1.SetActive(false);
            objet2.SetActive(false);
            objet3.SetActive(false);
            Invoke("AppelerMusiqueFin", 33f);
            Invoke("Credits", 55f);
        }
    }

    void AppelerMusiqueFin(){
        monteCharge.GetComponent<AudioSource>().PlayOneShot(musiqueFin);
        Invoke("ChangerScene",  94f);
    }
    void Credits(){
        canvas.GetComponent<Animator>().SetBool("lancerCredit", true);
        textCredit.SetActive(true);
        imageCredit.SetActive(true);
    }
    void ChangerScene(){
        SceneManager.LoadScene(0);
    }
}
