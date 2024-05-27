using UnityEngine;

public class SitWithRaycast : MonoBehaviour
{
    public Transform player;
    public string sitObjectName = "SitObject"; // El nombre del objeto en el que quieres sentarte
    public Vector3 sittingPosition;
    public Quaternion sittingRotation;
    
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            if (hit.transform.name == sitObjectName && Input.GetButtonDown("SitButton"))
            {
                player.position = sittingPosition;
                player.rotation = sittingRotation;
            }
        }
    }
}
