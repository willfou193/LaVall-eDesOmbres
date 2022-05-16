using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeplacementPersoScript : MonoBehaviour
{
    //D�claration des variables
    #region VariablesCamFPS
    public GameObject cameraFPS; // camera FPS pour le personnage
    public float vitesseHorizontaleFPS = 2f;   //sensibilit� horizontale de la souris
    public float vitesseVerticaleFPS = 2f; //sensibilit� verticale de la souris
    public float rotationV;  // angle de rotation verticale total en degr� selon le mouvement vertical de la souris

    #endregion
    #region VariablelampeDePoche
    public Collider lumiereCol; //collider de la lampe de poche
    public int TempsAventRecharge;
    public bool lampeUvAllumee; // est-ce que la lampe UV est activee
    public Light lampeUV; // active la lampe UV
    float chargeLampe = 3; // nombr ede charge de la lampe UV
    public Image charge1;
    public Image charge2;
    public Image charge3;
    #endregion
    #region raycastFPS
    public GameObject raycastFPS; // objet source du raycast
    public float distanceActivableLoin; // distance maximale d'activation avec le raycast
    bool mousquetonPossede;
    public GameObject bancActivable;
    public Text nbBarilUi;
    public bool tyroTrouvee = false;
    public TMP_Text bancActivableText;
    public TMP_Text bancRamassableText;
    #endregion
    #region persoStats
    public static bool mort; // savoir si le personnage est mort ou vivant
    public Vector3 posCheckpointActif; // position du checkpoint pr�sentemment actif
    public float vitesseDeplacement; // vitesse du d�placement du personnage
    float jaugeDeSprint = 6f;
    public float jaugeDeSprintMax = 6f;
    bool peutRegagnerEndurance = true;
    Vector3 vitesseDepAnim; // vitesse du d�placement pour l'animator
    Rigidbody rigidbodyPerso; // rigidbody du personnage
    public Animator joueurAnim;
    #endregion
    #region audio
    public AudioClip tyrolienne;
    public AudioClip marcheSon;
    public AudioClip monteChargeSon;
    public AudioClip lampeAllumeeSon;
    public AudioClip lampeFermeeSon;
    public AudioClip objetPrisSon;
    public AudioClip notePapierSon;
    #endregion
    #region UI
    int nombreDeBaril;
    public GameObject baril1;
    public GameObject baril2;
    public GameObject baril3;
    public GameObject numPad;
    public GameObject barreSprint;
    #endregion
    public GameObject[] ennemis;
    private checkPointControl checkPoCtrl;
    // Start is called before the first frame update
    void Start()
    {
        checkPoCtrl = GameObject.FindGameObjectWithTag("checkPoCtrl").GetComponent<checkPointControl>();
        transform.position = checkPoCtrl.dernierCheckPoint;
        mort = false;
        ennemis = GameObject.FindGameObjectsWithTag("monstre");
        rigidbodyPerso = GetComponent<Rigidbody>();
        lumiereCol.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        InvokeRepeating("RedonnerChargeLampe", 1f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit infoObjets;
        if (!mort && !menuPause.JeuPause)
        {
            #region deplacement
            
            float rotationH = Input.GetAxis("Mouse X") * vitesseHorizontaleFPS;
            transform.Rotate(0, rotationH, 0);


            //Ce bloc obtient la variation de la position verticale de la souris et tourne la cam�ra FPS avec des limites
            rotationV += Input.GetAxis("Mouse Y") * vitesseVerticaleFPS;

            // limite la valeur de l�angle de rotation entre une min et une max
            rotationV = Mathf.Clamp(rotationV, -45, 45);

            // on applique les angles de rotation � la cam�ra, 
            cameraFPS.transform.localEulerAngles = new Vector3(-rotationV, 0, 0);

            float vDeplacementFPS = Input.GetAxis("Vertical") * vitesseDeplacement;
            float hDeplacementFPS = Input.GetAxis("Horizontal") * vitesseDeplacement;

            GetComponent<Rigidbody>().velocity = transform.forward * vDeplacementFPS + transform.right * hDeplacementFPS + new Vector3(0, rigidbodyPerso.velocity.y, 0);
            

            //Section course du joueur
            #region Course
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if(jaugeDeSprint > 0f)
                {
                    peutRegagnerEndurance = false;
                    vitesseDeplacement = 25;
                    jaugeDeSprint -= 1 * Time.deltaTime;
                    cameraFPS.GetComponent<Camera>().fieldOfView = 65f;
                    CancelInvoke("RegainDenergie");
                }
                else
                {
                    Invoke("RegainDenergie", 2f);
                    vitesseDeplacement = 20;
                    cameraFPS.GetComponent<Camera>().fieldOfView = 62.5f;
                }
            }
            else {
                Invoke("RegainDenergie", 2f);
                vitesseDeplacement = 20;
                cameraFPS.GetComponent<Camera>().fieldOfView = 62.5f;
            }
            if(jaugeDeSprint < jaugeDeSprintMax && peutRegagnerEndurance)
            {
                jaugeDeSprint += 1 * Time.deltaTime;
            }
            barreSprint.GetComponent<RectTransform>().sizeDelta = new Vector2((400/jaugeDeSprintMax) * jaugeDeSprint ,20); //On modifie en temps réel la longueur du UI en fonction de la jauge de sprint
            #endregion
            #endregion
            #region lampeDePoche
            // On allumer / ferme le collider et la lumière de la lampe de poche en fonction de son �tat
            if (Input.GetKeyDown(KeyCode.F) && lampeUvAllumee == false && chargeLampe >= 1)
            {
                Invoke("FermerLampeUv", 3f);
                chargeLampe -= 1; //enleve une charge
                lampeUvAllumee = true;
                lumiereCol.enabled = true;
                lampeUV.enabled = true;
                gameObject.GetComponent<AudioSource>().PlayOneShot(lampeAllumeeSon);
                MiseAJourLampe(); //appel la fonction de la mise a jour de la lampe
            }
            else if(Input.GetKeyDown(KeyCode.F) && lampeUvAllumee == true)
            {
                lampeUvAllumee = false;
                lumiereCol.enabled = false;
                lampeUV.enabled = false;
                gameObject.GetComponent<AudioSource>().PlayOneShot(lampeFermeeSon);
            }
            #endregion
            #region interaction
            if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(cameraFPS.transform.position, cameraFPS.transform.forward, out infoObjets, distanceActivableLoin))
            {
                if (infoObjets.collider.tag == "gaz")
                {
                    nombreDeBaril += 1;
                    Destroy(infoObjets.collider.gameObject);
                    //nbBarilUi.text = nombreDeBaril.ToString() + "/3"; //On actualise le nombre de baril en texte
                    switch (nombreDeBaril)
                    {
                        case 1:
                            baril1.SetActive(true);
                            return;
                        case 2:
                            baril2.SetActive(true);
                            return;
                        case 3:
                            baril3.SetActive(true);
                            return;
                        default: 
                            baril1.SetActive(false);
                            baril2.SetActive(false);
                            baril3.SetActive(false);
                            return;
                    }
                }
                if(infoObjets.collider.tag == "mousqueton" && tyroTrouvee == true)
                {
                    mousquetonPossede = true;
                    infoObjets.collider.gameObject.SetActive(false);
                    gameObject.GetComponent<AudioSource>().PlayOneShot(objetPrisSon);
                    bancActivableText.GetComponent<TMP_Text>().text = "Appuyez sur E si vous avez l'outil de tyrolienne";
                }
                if(infoObjets.collider.tag == "bancActivable" && mousquetonPossede){
                    bancActivable.SetActive(true);
                }
                if(infoObjets.collider.name == "outilSurTyrolienne"){
                    gameObject.GetComponent<AudioSource>().PlayOneShot(tyrolienne);
                    gameObject.GetComponent<Animator>().enabled = true;
                    gameObject.GetComponent<Animator>().SetBool("activeTyro", true);
                    bancActivable.GetComponent<Animator>().SetBool("activeTyro", true);
                    Invoke("LacherTyro",11f);
                }
                if (infoObjets.collider.name == "numpad")
                {
                    Cursor.lockState = CursorLockMode.None;
                    numPad.SetActive(true);
                    menuPause.JeuPause = true;
                }
                if (infoObjets.collider.tag == "generatrice" && nombreDeBaril == 3)
                {
                    infoObjets.collider.gameObject.GetComponent<barriere>().Invoke("OuvrirPorte",3f);
                    infoObjets.collider.gameObject.GetComponent<barriere>().PartirGenerateur();

                }
                if(infoObjets.collider.tag == "papierReponse")
                {
                    string nomLettre = infoObjets.collider.name;
                    infoObjets.collider.gameObject.GetComponent<afficherHistoire>().AfficherBonnePage(nomLettre);
                }
            }
            #endregion
            if(gameObject.GetComponent<santeMentale>().sanite <0.1f){
                foreach(GameObject ennemi in ennemis)
                {
                    print(ennemi.gameObject.name);
                    ennemi.GetComponent<Ai_script>().AllerAuProchainPoint();
                }
                joueurAnim.enabled = true;
                joueurAnim.SetBool("joueurMort",true);
                Invoke("ReloadScene",5.6f);
                mort = true;
            }

        }
    } // fin du update
    void FermerLampeUv()
    {
        if(lampeUvAllumee == true)
        {
            lampeUvAllumee = false;
            lumiereCol.enabled = false;
            lampeUV.enabled = false;
            gameObject.GetComponent<AudioSource>().PlayOneShot(lampeFermeeSon);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "bancActivable")
        {
            print("changement de texte");
            bancRamassableText.GetComponent<TMP_Text>().text = "Appuyez sur E pour ramasser l'outil de tyrolienne";
            tyroTrouvee = true;
        }      
    }
    // redonne une charge de la lampe à chaque X secondes 
    void RedonnerChargeLampe()
    {
        if (chargeLampe <= 2)
        {
            chargeLampe += 1;
            MiseAJourLampe();
        }
    }
    void LacherTyro(){
        gameObject.GetComponent<Animator>().SetBool("activeTyro",false);
        gameObject.GetComponent<Animator>().enabled = false;
    }
    void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //on relance la scène
    }

    void MiseAJourLampe() // Switch case en fonction du nombre de charge
    {
        switch (chargeLampe)
        {
            case 1:
                charge1.enabled = false;
                charge2.enabled = false;
                charge3.enabled = true;
                return;
            case 2:
                charge1.enabled = false;
                charge2.enabled = true;
                charge3.enabled = true;
                return;
            case 3:
                charge1.enabled = true;
                charge2.enabled = true;
                charge3.enabled = true;
                return;
            default:
                charge1.enabled = false;
                charge2.enabled = false;
                charge3.enabled = false;
                return;
        }
    }
    void RegainDenergie()
    {
        peutRegagnerEndurance = true;
    }
}
