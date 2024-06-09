using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Door : MonoBehaviour
{
    private TransitionManager transitionManager;
    public int SceneNumber;


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the TransitionManager in the new scene
        transitionManager = FindObjectOfType<TransitionManager>();

        if (transitionManager == null)
        {
            Debug.LogError("TransitionManager not found in the scene.");
        }
    }

    public void OnGazeSelect()
    {
        TriggerSceneTransition();
    }

    private void TriggerSceneTransition()
    {
        if (transitionManager != null)
        {
            transitionManager.GotoScene(SceneNumber);
        }
        else
        {
            Debug.LogError("TransitionManager is not set.");
        }
    }
}



