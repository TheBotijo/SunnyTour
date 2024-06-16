using UnityEngine;

public class Chopper : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player"; // Tag del jugador
    public AudioManager audioManager;
    public Animator animator;
    public AudioSource audioSource;
    public bool isDancing = false; // Estado para evitar reiniciar la animación

    void Start()
    {
        // Obtener los componentes Animator y AudioSource
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        AssignAudioManager();
        // Debug para asegurar que los componentes están correctamente obtenidos
        if (animator == null)
            Debug.LogError("Animator no encontrado en Chopper.");
        else
            Debug.Log("Animator encontrado en Chopper.");

        if (audioSource == null)
            Debug.LogError("AudioSource no encontrado en Chopper.");
        else
            Debug.Log("AudioSource encontrado en Chopper.");
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
    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entró al trigger tiene el tag correcto
        if (other.CompareTag(playerTag) && !isDancing)
        {
            Debug.Log("Jugador ha entrado en el área de detección. Comenzar a bailar.");
            audioManager.PlayMusic("None");
            StartDancing();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que salió del trigger tiene el tag correcto
        if (other.CompareTag(playerTag) && isDancing)
        {
            Debug.Log("Jugador ha salido del área de detección. Detener el baile.");
            //StopDancing();
        }
    }

    private void StartDancing()
    {
        // Cambiar la animación a bailar y reproducir la canción
        Debug.Log("Iniciando animación de baile.");
        animator.SetBool("Dancing", true);        
        audioSource.Play();
        isDancing = true;
    }

    private void StopDancing()
    {
        // Volver a la animación de Idle y detener la música
        Debug.Log("Deteniendo animación de baile.");
        animator.SetBool("Dancing", false);
        audioSource.Stop();
        isDancing = false;
    }
}




