using UnityEngine;

public class WindDirection : MonoBehaviour
{
    public Material leafMaterial;
    public float changeInterval = 5f;

    private float timeSinceLastChange;
    private Vector3 currentWindDirection;

    void Start()
    {
        // Define la direcci�n del viento inicial en una direcci�n aleatoria.
        currentWindDirection = Random.insideUnitSphere;
        leafMaterial.SetVector("_WindDirection", currentWindDirection);
        leafMaterial.SetVector("_WindOffset", Random.insideUnitSphere);
    }

    void Update()
    {
        timeSinceLastChange += Time.deltaTime;

        if (timeSinceLastChange >= changeInterval)
        {
            // Genera una nueva direcci�n de viento aleatoria.
            currentWindDirection = Random.insideUnitSphere;
            leafMaterial.SetVector("_WindDirection", currentWindDirection);

            timeSinceLastChange = 0f;
        }
    }
}