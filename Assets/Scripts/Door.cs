using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Door : MonoBehaviour
{
    public TransitionManager transitionManager;
    public AudioManager audioManager;
    public int SceneNumber;

    private XRBaseInteractable interactable;

    private void Update()
    {
        AssignAudioManager();
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelectEntered);
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnSelectEntered);
        }
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

    private void OnSelectEntered(SelectEnterEventArgs args)
    {        
        TriggerSceneTransition();
    }

    private void TriggerSceneTransition()
    {
        if (transitionManager != null)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex == 2)
            {
                audioManager.PlaySFX("PuertaC");
                Debug.LogError("Puerta");
            }
            if (currentSceneIndex == 1)
            {
                audioManager.PlaySFX("PuertaA");
                Debug.LogError("Puerta");
            }

            transitionManager.GotoScene(SceneNumber);
        }
        else
        {
            Debug.LogError("TransitionManager is not set.");
        }
    }
    public void AssignAudioManager()
    {
        // Encuentra el jugador en la escena (puedes ajustar este método para encontrar el objeto correcto)
        GameObject AudioManager = GameObject.FindWithTag("AudioManager");
        if (AudioManager != null)
        {
            // Encuentra el componente FadeScreen en el jugador
            audioManager = AudioManager.GetComponent<AudioManager>();
            if (audioManager == null)
            {
                Debug.LogError("Audio Manager no encontrado en el jugador.");
            }
        }
        else
        {
            Debug.LogError("Audio Manager no encontrado en la escena.");
        }
    }
}



