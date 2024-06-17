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

    // Referencia a la imagen de UI que mostrar� el PNG
    public Image exitImage;

    private void Start()
    {
        // Buscar autom�ticamente el objeto con el tag "Player" al inicio
        Player = GameObject.FindWithTag("Player");

        if (Player == null)
        {
            Debug.LogError("No se encontr� un objeto con el tag 'Player'. Aseg�rate de que el jugador tiene el tag 'Player'.");
            return;
        }

        // Buscar autom�ticamente SceneInit en la escena
        sceneInit = FindObjectOfType<SceneInit>();
        if (sceneInit == null)
        {
            Debug.LogError("No se encontr� un objeto de tipo 'SceneInit'. Aseg�rate de que est� presente en la escena.");
            return;
        }

        // Buscar autom�ticamente TransitionManager en la escena
        transitionManager = FindObjectOfType<TransitionManager>();
        if (transitionManager == null)
        {
            Debug.LogError("No se encontr� un objeto de tipo 'TransitionManager'. Aseg�rate de que est� presente en la escena.");
            return;
        }

        // Aseg�rate de que la imagen de salida est� inicialmente desactivada
        if (exitImage != null)
        {
            exitImage.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("No se asign� una imagen de salida. Por favor, asigna una en el Inspector.");
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

        // Llama al m�todo de TransitionManager para cualquier acci�n de salida
        if (transitionManager != null)
        {
            transitionManager.OnExitButtonPressed();
        }

        // Cerrar la aplicaci�n
        Application.Quit();

#if UNITY_EDITOR
        // Esto es solo para cerrar el modo de juego en el editor de Unity
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}



