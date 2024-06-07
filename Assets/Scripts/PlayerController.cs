using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener el jugador entre escenas
        }
        else
        {
            Destroy(gameObject); // Destruir instancias duplicadas
        }
    }

    // A�ade aqu� otros m�todos y propiedades del jugador
}
