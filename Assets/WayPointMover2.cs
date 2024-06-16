using UnityEngine;
using System.Collections;

public class WaypointMover2 : MonoBehaviour
{
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float distanceThreshold = 0.1f;

    [SerializeField] private float minWaitTime = 2f; // Tiempo m�nimo de espera en segundos
    [SerializeField] private float maxWaitTime = 5f; // Tiempo m�ximo de espera en segundos
    [SerializeField] private float directionChangeProbability = 0.3f; // Probabilidad de cambiar de direcci�n

    private Transform currentWaypoint;
    private bool isWaiting = false;

    void Start()
    {
        currentWaypoint = waypoints.GetNextWaypoint(null);
        if (currentWaypoint != null)
        {
            transform.position = currentWaypoint.position;
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            if (currentWaypoint != null)
            {
                SmoothLookAt(currentWaypoint.position);
            }
        }

        // Iniciar la rutina de comportamiento aleatorio
        StartCoroutine(RandomBehaviorRoutine());
    }

    private void Update()
    {
        if (currentWaypoint == null || isWaiting) return;

        // Moverse hacia el siguiente waypoint
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        // Suavizar la rotaci�n hacia el siguiente waypoint
        SmoothLookAt(currentWaypoint.position);

        // Verificar si est� cerca del waypoint actual
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        }
    }

    // M�todo para suavizar la rotaci�n hacia una posici�n objetivo
    private void SmoothLookAt(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Rutina para manejar el comportamiento aleatorio
    private IEnumerator RandomBehaviorRoutine()
    {
        while (true)
        {
            // Esperar un tiempo aleatorio entre minWaitTime y maxWaitTime
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            // Decidir al azar si cambiar de direcci�n o quedarse quieto
            if (Random.value < directionChangeProbability)
            {
                // Cambiar de direcci�n
                ChangeDirection();
            }
            else
            {
                // Quedarse quieto por un tiempo aleatorio
                float idleTime = Random.Range(minWaitTime, maxWaitTime);
                isWaiting = true;
                yield return new WaitForSeconds(idleTime);
                isWaiting = false;
            }
        }
    }

    // M�todo para cambiar la direcci�n del pez
    private void ChangeDirection()
    {
        // Invierte la direcci�n en el sistema de waypoints
        waypoints.ReverseDirection();

        // Asigna el siguiente waypoint en la nueva direcci�n
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
    }
}


