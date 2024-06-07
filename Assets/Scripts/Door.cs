using UnityEngine;

public class Door : MonoBehaviour
{
    public int sceneIndexToLoad; // Índice de la escena a cargar

    // Método para ser llamado al interactuar con la puerta
    public void OnGazeSelect()
    {
        if (TransitionManager.Instance != null)
        {
            TransitionManager.Instance.GotoScene(sceneIndexToLoad);
        }
    }
}


