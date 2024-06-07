using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PickUpObject : MonoBehaviour
{
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    [System.Obsolete]
    public void Update()
    {
        // Obtiene el componente XRGrabInteractable
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onDeactivate.AddListener(EnableGravity);
    }



    void EnableGravity(XRBaseInteractor interactor)
    {
        // Activa la gravedad cuando el objeto es soltado
        rb.useGravity = true;
    }
}
