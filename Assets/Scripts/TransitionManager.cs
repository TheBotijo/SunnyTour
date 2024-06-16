using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;
    public FadeScreen fadeScreen; // Puede estar vacío inicialmente
    public AudioManager audioManager;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener entre escenas
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados
        }
    }
    void Update()
    {
        // Intentar encontrar y asignar el FadeScreen en el inicio
        if (fadeScreen == null)
        {
            AssignFadeScreen();
            AssignAudioManager();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        // Intentar encontrar y asignar el FadeScreen cuando se cargue una nueva escena
        fadeScreen.FadeIn();
        AssignFadeScreen();
        AssignAudioManager();
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
    public void AssignFadeScreen()
    {
        // Encuentra el jugador en la escena (puedes ajustar este método para encontrar el objeto correcto)
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            // Encuentra el componente FadeScreen en el jugador
            fadeScreen = player.GetComponentInChildren<FadeScreen>();
            if (fadeScreen == null)
            {
                Debug.LogError("FadeScreen no encontrado en el jugador.");
            }
        }
        else
        {
            Debug.LogError("Jugador no encontrado en la escena.");
        }
    }

    public void OnRestartButtonPressed()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(OnRestartButtonPressed(currentSceneIndex));
    }

    IEnumerator OnRestartButtonPressed(int sceneIndex)
    {
        if (fadeScreen != null)
        {
            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeScreen.fadeDuration);
        }

        SceneManager.LoadScene(sceneIndex);
    }

    public void OnExitButtonPressed()
    {
        StartCoroutine(QuitApplicationRoutine());
    }

    IEnumerator QuitApplicationRoutine()
    {
        if (fadeScreen != null)
        {
            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeScreen.fadeDuration);
        }

        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
    public void GotoScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        if (fadeScreen != null)
        {
            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeScreen.fadeDuration);
        }

        // Abre la nueva escena
        SceneManager.LoadScene(sceneIndex);
        if (sceneIndex == 1)
        {
            audioManager.PlayMusic("MainGuitar");
        }
        if (sceneIndex == 2)
        {
            audioManager.PlayMusic("MainPiano");
        }
    }

    public void GotoSceneAsync(int sceneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        if (fadeScreen != null)
        {
            fadeScreen.FadeOut();
            // Abre la nueva escena
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            operation.allowSceneActivation = false;

            float timer = 0;
            while (timer <= fadeScreen.fadeDuration && !operation.isDone)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            fadeScreen.FadeIn();
            operation.allowSceneActivation = true;
        }
    }
}


