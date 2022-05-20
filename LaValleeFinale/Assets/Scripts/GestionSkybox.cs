using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionSkybox : MonoBehaviour
{
    public Material sunrise;
    public Material night;
    public Material sunset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //si l scene active est la scene laValleedesOmbres, alors on change la skybox
        if (SceneManager.GetActiveScene().name == "laValleedesOmbres")
        {
            //si la variable gagne est vraie, alors on change la skybox a sunrise
            if (NumPad.gagne == true)
            {
                RenderSettings.skybox = sunrise;
            }
            //si la variable gagne est faux, alors on change la skybox a night
            else
            {
                RenderSettings.skybox = night;
            }
            
        }
        //si la scene active et la scene scene_intro, alors on change la skybox a sunset et gagne devien false
        if (SceneManager.GetActiveScene().name == "scene_intro")
        {
            RenderSettings.skybox = sunset;
            NumPad.gagne = false;
            DeplacementPersoScript.tyroTrouvee = false;
        }
    }
}
