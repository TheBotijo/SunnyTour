using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolpeMadera : MonoBehaviour
{

    void OnCollisionEnter(Collision other) // Este método se llama cuando hay una colisión
    {
        Debug.Log("TestMAdera");
        if (other.gameObject.CompareTag("Floor")) // Comprueba si el objeto con el que colisionó tiene la etiqueta "Suelo"
        {
            AudioManager.Instance.PlaySFX("Madera"); // Si es así, reproduce el sonido.
            Debug.Log("TestMAdera");
        }
    }
}
