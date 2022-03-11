using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPersoScript : MonoBehaviour
{
    //Déclaration des variables
    #region VariablesCamFPS
    public GameObject cameraFPS; // camera FPS pour le personnage
    public float vitesseHorizontaleFPS = 2f;   //sensibilité horizontale de la souris
    public float vitesseVerticaleFPS = 2f; //sensibilité verticale de la souris
    public float rotationV;  // angle de rotation verticale total en degré selon le mouvement vertical de la souris
    #endregion
    public Collider lumiereCol;
    public bool lumiereAllumee;
    #region raycastFPS
    public GameObject raycastFPS; // objet source du raycast
    public float distanceActivableLoin; // distance maximale d'activation avec le raycast
    #endregion

    public float vitesseDeplacement; // vitesse du déplacement du personnage
    public float hauteurSaut; // hauteur du saut du personnage
    Vector3 vitesseDepAnim; // vitesse du déplacement pour l'animator
    Rigidbody rigidbodyPerso; // rigidbody du personnage

    public static bool mort; // savoir si le personnage est mort ou vivant
    public Vector3 posCheckpointActif; // position du checkpoint présentemment actif

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyPerso = GetComponent<Rigidbody>();
        lumiereCol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit infoObjets;
        Physics.Raycast(raycastFPS.transform.position, raycastFPS.transform.forward, out infoObjets, -distanceActivableLoin);



        if (!mort)
        {
            #region deplacement

            float rotationH = Input.GetAxis("Mouse X") * vitesseHorizontaleFPS;
            transform.Rotate(0, rotationH, 0);


            //Ce bloc obtient la variation de la position verticale de la souris et tourne la caméra FPS avec des limites
            rotationV += Input.GetAxis("Mouse Y") * vitesseVerticaleFPS;

            // limite la valeur de l’angle de rotation entre une min et une max
            rotationV = Mathf.Clamp(rotationV, -45, 45);

            // on applique les angles de rotation à la caméra, 
            cameraFPS.transform.localEulerAngles = new Vector3(-rotationV, 0, 0);

            float vDeplacementFPS = Input.GetAxis("Vertical") * vitesseDeplacement;
            float hDeplacementFPS = Input.GetAxis("Horizontal") * vitesseDeplacement;

            GetComponent<Rigidbody>().velocity = transform.forward * vDeplacementFPS + transform.right * hDeplacementFPS + new Vector3(0, rigidbodyPerso.velocity.y, 0);

            #endregion

            #region lampeDePoche
            // On allumer / ferme le collider de la lampe de poche en fonction de son état
            if (Input.GetKeyDown(KeyCode.F) && lumiereAllumee == false)
            {
                lumiereAllumee = true;
                lumiereCol.enabled = true;
            }
            else if(Input.GetKeyDown(KeyCode.F) && lumiereAllumee == true)
            {
                lumiereAllumee = false;
                lumiereCol.enabled = false;
            }
            #endregion
        }
    }
}
