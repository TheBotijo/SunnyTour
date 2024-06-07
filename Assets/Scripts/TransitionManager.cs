using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }
    public FadeScreen fadeScreen; // Puede estar vacío inicialmente

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

    private void Start()
    {
        // Intentar encontrar y asignar el FadeScreen en el inicio
        if (fadeScreen == null)
        {
            AssignFadeScreen();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        // Intentar encontrar y asignar el FadeScreen cuando se cargue una nueva escena
        AssignFadeScreen();
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

            operation.allowSceneActivation = true;
        }
    }
}


