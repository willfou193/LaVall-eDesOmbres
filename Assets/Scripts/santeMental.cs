using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class santeMental : MonoBehaviour
{
    public float rayonCol = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] objectsDansCercle = Physics.OverlapSphere(gameObject.transform.position, rayonCol);
        foreach (var objectTouchee in objectsDansCercle)
        {
            if(objectTouchee.gameObject.tag == "monstre")
            {
                RaycastHit lien;
                Physics.Linecast(transform.position, gameObject.transform.position, out lien);
                if(!(lien.transform.tag == "terrain"))
                {
                    float distance = Vector3.Distance(objectTouchee.transform.position, transform.position);
                    print(distance);
                    //descendre la santé mental
                }




         


            }
            
        }
    }
}
