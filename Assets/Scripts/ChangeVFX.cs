using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeVFX : MonoBehaviour
{
    public MonoBehaviour VFX;
    public Slider slider;
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
