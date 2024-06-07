using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public Transform head; // Referencia a la cabeza del jugador
    public float spawnDistance = 2;
    public GameObject menu;
    public GameObject options;
    public GameObject info;
    public InputActionProperty showButton;

    void Start()
    {
        // Inicializa el menú y sus opciones
        menu.SetActive(false);
        options.SetActive(false);
        info.SetActive(false);

        
    }

    void Update()
    {
        // Asigna la referencia a la cabeza si no está asignada
        if (head == null)
        {
            AssignHead();
        }

        if (showButton.action.WasPressedThisFrame())
        {
            // Alternar la visibilidad del menú
            menu.SetActive(!menu.activeSelf);
            options.SetActive(false);
            info.SetActive(false);

            // Posicionar el menú frente a la cabeza del jugador
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            options.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            info.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        // Asegurar que el menú mira al jugador
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
        options.transform.LookAt(new Vector3(head.position.x, options.transform.position.y, head.position.z));
        options.transform.forward *= -1;
        info.transform.LookAt(new Vector3(head.position.x, info.transform.position.y, head.position.z));
        info.transform.forward *= -1;
    }

    private void AssignHead()
    {
        // Encuentra el objeto del jugador y asigna la referencia a la cabeza
        GameObject player = GameObject.FindWithTag("Player"); // Asegúrate de que el jugador tiene la etiqueta "Player"
        if (player != null)
        {
            head = player.transform.Find("Head"); // Cambia "Head" por el nombre exacto del objeto en la jerarquía del jugador
            if (head == null)
            {
                Debug.LogError("No se encontró 'Head' en el jugador.");
            }
        }
        else
        {
            Debug.LogError("Jugador no encontrado en la escena.");
        }
    }
}

