using UnityEngine;
using System.Collections;

public class WaypointMover2 : MonoBehaviour
{
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float distanceThreshold = 0.1f;

    [SerializeField] private float minWaitTime = 2f; // Tiempo mínimo de espera en segundos
    [SerializeField] private float maxWaitTime = 5f; // Tiempo máximo de espera en segundos
    [SerializeField] private float directionChangeProbability = 0.3f; // Probabilidad de cambiar de dirección

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

        // Suavizar la rotación hacia el siguiente waypoint
        SmoothLookAt(currentWaypoint.position);

        // Verificar si está cerca del waypoint actual
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        }
    }

    // Método para suavizar la rotación hacia una posición objetivo
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

            // Decidir al azar si cambiar de dirección o quedarse quieto
            if (Random.value < directionChangeProbability)
            {
                // Cambiar de dirección
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

    // Método para cambiar la dirección del pez
    private void ChangeDirection()
    {
        // Invierte la dirección en el sistema de waypoints
        waypoints.ReverseDirection();

        // Asigna el siguiente waypoint en la nueva dirección
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
    }
}


