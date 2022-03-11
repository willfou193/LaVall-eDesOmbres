using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampeCollision : MonoBehaviour
{
    public GameObject monstre;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LeMonstreCourt()
    {
        monstre.GetComponent<Ai_script>().navAgent.speed = 3;
    }

    private void OnTriggerEnter(Collider infoCol)
    {
        if(infoCol.gameObject.tag == "monstre")
        {
            print("Arrête toi!");
            monstre.GetComponent<Ai_script>().navAgent.speed = 0;
            Invoke("LeMonstreCourt", 5f);

        }

    }

}
