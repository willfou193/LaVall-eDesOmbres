using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    private checkPointControl checkPoCtrl;
    // Start is called before the first frame update
    void Start()
    {
        checkPoCtrl = GameObject.FindGameObjectWithTag("checkPoCtrl").GetComponent<checkPointControl>();
    }

    private void OnTriggerEnter(Collider infoCol)
    {
        if (infoCol.CompareTag("Player"))
        {
            checkPoCtrl.dernierCheckPoint = transform.position;
        }
    }
}
