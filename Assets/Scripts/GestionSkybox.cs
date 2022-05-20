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
        if (SceneManager.GetActiveScene().name == "laValleedesOmbres")
        {
            if (NumPad.gagne == true)
            {
                RenderSettings.skybox = sunrise;
            }
            if (NumPad.gagne == false)
            {
                RenderSettings.skybox = night;
            }
        }
        
        if (SceneManager.GetActiveScene().name == "scene_intro")
        {
            RenderSettings.skybox = sunset;
            NumPad.gagne = false;
        }
    }
}
