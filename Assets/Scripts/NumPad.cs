using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NumPad : MonoBehaviour
{
    /*
     Ce script g�re le Numpad dans le monte charge. Le script g�re les boutons de 0 � 9 ainsi que retour et entrer
     */
    string code = "";
    public string bonneReponse;
    int nombreDeNumMax = 4;
    public Text codeText;
    public Text bonneReponseText;
    public GameObject numpad;
    public GameObject monteCharge;
    private void Start()
    {
        //bonneReponse = Random.Range(0,9).ToString();

        //For loop cr�ant un code al�atoire au d�but de la partie
        for (var i = 0; i < nombreDeNumMax;i++)
        {
                bonneReponse += Random.Range(0, 9).ToString(); //On cr�er un chiffre al�atoire et on le concat�ne � la string
        }
        print("la bonne r�ponse est " + bonneReponse);
        bonneReponseText.text = bonneReponse.ToString();
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
            print("Fin du jeu");
            //activer la scin�matique
        }
    }
}
