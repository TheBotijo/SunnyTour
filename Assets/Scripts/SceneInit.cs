using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInit : MonoBehaviour
{
    public GameObject Player; // Prefab del jugador que se instancia en la nueva escena

    void OnEnable()
    {
        // Register the callback to be called when a scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unregister the callback when the script is disabled
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Code to execute when a scene is loaded
        Debug.Log("Scene loaded: " + scene.name);
        // Call your custom method or script here
        Spawn();
    }
    public void Spawn()
    {
        // Encuentra el objeto de punto de spawn en la escena
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        Vector3 spawnPosition;
        Quaternion spawnRotation;

        if (spawnPoint != null)
        {
            // Usa la posici�n y rotaci�n del punto de spawn
            spawnPosition = spawnPoint.transform.position;
            spawnRotation = spawnPoint.transform.rotation;
        }
        else
        {
            // Fallback a una posici�n y rotaci�n predeterminadas si no se encuentra el SpawnPoint
            Debug.LogWarning("No se encontr� 'SpawnPoint', usando posici�n predeterminada.");
            spawnPosition = Vector3.zero;
            spawnRotation = Quaternion.identity;
        }

        
            // Si ya existe una instancia del jugador, aseg�rate de actualizar su posici�n o propiedades si es necesario
            PlayerController.Instance.transform.position = spawnPosition;
            PlayerController.Instance.transform.rotation = spawnRotation;
        
    }
}


