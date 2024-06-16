using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class ChangeVFX : MonoBehaviour
{
    public PostProcessing VFX;
    public Slider slider;

    private void Start()
    {
        AssignVFX();
    }
    private void OnLevelWasLoaded(int level)
    {
        AssignVFX();
    }
    public void AssignVFX()
    {
        GameObject Lighting = GameObject.FindWithTag("Pospo");
        if (Lighting != null)
        {
            // Encuentra el componente FadeScreen en el jugador
            VFX = Lighting.GetComponentInChildren<PostProcessing>();
            if (VFX == null)
            {
                Debug.LogError("VFX no encontrado en el jugador.");
            }
        }
        else
        {
            Debug.LogError("VFX no encontrado en la escena.");
        }
    }
    public void Update()
    {
        if (slider.value == 0) 
                {
                    VFX.enabled = false;
                }
        if (slider.value == 1)
                {
                    VFX.enabled = true;
                }
   }
    

    
}
