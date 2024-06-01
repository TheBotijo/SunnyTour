using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChange : MonoBehaviour
{
    public GameObject player; // Referencia al objeto del jugador
    public MonoBehaviour continuousMovementScript; // Referencia al script de movimiento continuo
    public MonoBehaviour snapTurnMovementScript; // Referencia al script de giro por pasos

    void Start()
    {
        // Puedes inicializar el movimiento aquí según tus preferencias o configuraciones guardadas
        SetMovementMode(PlayerPrefs.GetString("MovementMode", "Continuous"));
    }

    public void SetMovementMode(string mode)
    {
        switch (mode)
        {
            case "Continuous":
                continuousMovementScript.enabled = true;
                snapTurnMovementScript.enabled = false;
                PlayerPrefs.SetString("MovementMode", "Continuous");
                break;
            case "SnapTurn":
                continuousMovementScript.enabled = false;
                snapTurnMovementScript.enabled = true;
                PlayerPrefs.SetString("MovementMode", "SnapTurn");
                break;
            default:
                Debug.LogWarning("Modo de movimiento desconocido: " + mode);
                break;
        }
    }
}
