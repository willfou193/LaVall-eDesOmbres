using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class afficherHistoire : MonoBehaviour
{
    public GameObject[] lettre;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AfficherBonnePage(string nomCarte){
        nomCarte.Substring(6, 1);
        print(nomCarte);
        int numero = int.Parse(nomCarte);
        lettre[numero].SetActive(true);
        

}
}
