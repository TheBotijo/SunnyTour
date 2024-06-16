using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInit : MonoBehaviour
{
    public GameObject playerPrefab; // Prefab del jugador que se instancia en la nueva escena
    private GameObject playerInstance; // Referencia al objeto del jugador en la escena
    private GameObject spawnPoint;

    private void Start()
    {
        playerInstance = GameObject.FindGameObjectWithTag("Player");
    }
    void OnEnable()
    {
        // Registrar el callback para ser llamado cuando se carga una escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Desregistrar el callback cuando el script se deshabilita
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Código para ejecutar cuando se carga una escena
        Debug.Log("Escena cargada: " + scene.name);
        // Llama a tu método personalizado o script aquí
        Spawn();
    }

    public void Spawn()
    {
        // Encuentra el objeto de punto de spawn en la escena
        spawnPoint = GameObject.Find("SpawnPoint");

        if (spawnPoint != null)
        {
            Debug.Log("SpawnPoint encontrado en la escena: " + spawnPoint.name);

            Vector3 spawnPosition = spawnPoint.transform.position;
            Quaternion spawnRotation = spawnPoint.transform.rotation;
            playerInstance.transform.position = spawnPosition;
            playerInstance.transform.rotation = spawnRotation;
            // Intentar encontrar el objeto del jugador existente


        }
    }
}








