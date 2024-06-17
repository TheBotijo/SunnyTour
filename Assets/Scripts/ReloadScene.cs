using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ReloadScene : MonoBehaviour
{
    public Transform Spawn; // Transform del punto de respawn
    public GameObject Player; // Referencia al objeto del jugador
    private SceneInit sceneInit; // Referencia a SceneInit
    private TransitionManager transitionManager; // Referencia a TransitionManager

    // Referencia a la imagen de UI que mostrará el PNG
    public Image exitImage;

    private void Start()
    {
        // Buscar automáticamente el objeto con el tag "Player" al inicio
        Player = GameObject.FindWithTag("Player");

        if (Player == null)
        {
            Debug.LogError("No se encontró un objeto con el tag 'Player'. Asegúrate de que el jugador tiene el tag 'Player'.");
            return;
        }

        // Buscar automáticamente SceneInit en la escena
        sceneInit = FindObjectOfType<SceneInit>();
        if (sceneInit == null)
        {
            Debug.LogError("No se encontró un objeto de tipo 'SceneInit'. Asegúrate de que está presente en la escena.");
            return;
        }

        // Buscar automáticamente TransitionManager en la escena
        transitionManager = FindObjectOfType<TransitionManager>();
        if (transitionManager == null)
        {
            Debug.LogError("No se encontró un objeto de tipo 'TransitionManager'. Asegúrate de que está presente en la escena.");
            return;
        }

        // Asegúrate de que la imagen de salida está inicialmente desactivada
        if (exitImage != null)
        {
            exitImage.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("No se asignó una imagen de salida. Por favor, asigna una en el Inspector.");
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            // Mostrar la imagen durante 5 segundos y luego salir
            StartCoroutine(ShowImageAndExit());
        }
    }

    private IEnumerator ShowImageAndExit()
    {
        // Activar la imagen
        if (exitImage != null)
        {
            exitImage.gameObject.SetActive(true);
        }

        // Esperar 5 segundos
        yield return new WaitForSeconds(5);

        // Llama al método de TransitionManager para cualquier acción de salida
        if (transitionManager != null)
        {
            transitionManager.OnExitButtonPressed();
        }

        // Cerrar la aplicación
        Application.Quit();

#if UNITY_EDITOR
        // Esto es solo para cerrar el modo de juego en el editor de Unity
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}



