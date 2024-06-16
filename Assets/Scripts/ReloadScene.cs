using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public Transform Spawn; // Transform del punto de respawn
    public GameObject Player; // Referencia al objeto del jugador�
    public SceneInit Scene;

    private void Start()
    {
        // Buscar autom�ticamente el objeto con el tag "Player" al inicio
        Player = GameObject.FindWithTag("Player");

        if (Player == null)
        {
            Debug.LogError("No se encontr� un objeto con el tag 'Player'. Aseg�rate de que el jugador tiene el tag 'Player'.");
            return;
        }
    }

    

    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            RestartScene();
            Scene.Spawn();
        }
    }

    public void RestartScene()
    {
        // Obtiene el �ndice de la escena actual y la reinicia
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

