using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NumPad : MonoBehaviour
{
    /*
     Ce script gère le Numpad dans le monte charge. Le script gère les boutons de 0 à 9 ainsi que retour et entrer
     */
    string code = "";
    public string bonneReponse;
    int nombreDeNumMax = 4;
    public Text codeText;
    public GameObject numpad;

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
    private void Start()
    {
        //bonneReponse = Random.Range(0,9).ToString();

        //For loop créant un code aléatoire au début de la partie
        for (var i = 0; i < nombreDeNumMax;i++)
        {
                bonneReponse += Random.Range(0, 9).ToString(); //On créer un chiffre aléatoire et on le concatène à la string
        }
        print("la bonne réponse est " + bonneReponse);
    }
    public void BoutonAppuyer()
    {
        string numero = EventSystem.current.currentSelectedGameObject.name; //renvoie le numero du bouton appuyé
        AjouterNum(numero); //Appel de la fonction avec le numero du bouton
    }
    public void AjouterNum(string num) 
    {
        //si le code n'est pas à son maximum,
        if(code.Length < nombreDeNumMax)
        {
            code += num; // on ajoute le numero au code
            codeText.text = code; //et on l'affiche
        }
    }
    public void EnleverNum()
    {
        //Si le joueur clique sur return, on enlève un numero du code
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
            print("Fin du jeu");
            //activer la scinématique
        }
    }
}
