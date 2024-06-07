using System.Collections;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public GameObject head; // Asume que `head` es una referencia al objeto donde deber�a estar `FadeScreen`

    private void Awake()
    {
        // A�ade `FadeScreen` si no existe
        if (head != null && head.GetComponent<FadeScreen>() == null)
        {
            head.AddComponent<FadeScreen>();
        }
    }
}

