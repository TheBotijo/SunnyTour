using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolpeMadera : MonoBehaviour
{

    void OnCollisionEnter(Collision other) // Este m�todo se llama cuando hay una colisi�n
    {
        Debug.Log("TestMAdera");
        if (other.gameObject.CompareTag("Floor")) // Comprueba si el objeto con el que colision� tiene la etiqueta "Suelo"
        {
            AudioManager.Instance.PlaySFX("Madera"); // Si es as�, reproduce el sonido.
            Debug.Log("TestMAdera");
        }
    }
}
