using UnityEngine;

public class Door : MonoBehaviour
{
    public int sceneIndexToLoad; // �ndice de la escena a cargar

    // M�todo para ser llamado al interactuar con la puerta
    public void OnGazeSelect()
    {
        if (TransitionManager.Instance != null)
        {
            TransitionManager.Instance.GotoScene(sceneIndexToLoad);
        }
    }
}


