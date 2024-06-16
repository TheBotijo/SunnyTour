using UnityEngine;
using System.Collections;

public class WaypointMover2 : MonoBehaviour
{
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float distanceThreshold = 0.1f;

    [SerializeField] private float decisionInterval = 10f; // Intervalo fijo para decidir si se para o cambia de direcci�n
    [SerializeField] private float minIdleTime = 2f; // Tiempo m�nimo que se queda quieto
    [SerializeField] private float maxIdleTime = 5f; // Tiempo m�ximo que se queda quieto
    [SerializeField] private float directionChangeProbability = 0.3f; // Probabilidad de cambiar de direcci�n

    private Transform currentWaypoint;
    private bool isWaiting = false;
    private Animator animator;

    void Start()
    {
        // Obtener el componente Animator
        animator = GetComponent<Animator>();

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

        // Activar la animaci�n solo cuando se est� moviendo
        if (animator != null && !animator.GetBool("IsMoving"))
        {
            animator.SetBool("IsMoving", true);
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
            // Esperar el intervalo fijo antes de tomar una decisi�n
            yield return new WaitForSeconds(decisionInterval);

            // Decidir al azar si cambiar de direcci�n o quedarse quieto
            if (Random.value < directionChangeProbability)
            {
                // Cambiar de direcci�n
                ChangeDirection();
            }
            else
            {
                // Quedarse quieto por un tiempo aleatorio entre minIdleTime y maxIdleTime
                float idleTime = Random.Range(minIdleTime, maxIdleTime);
                isWaiting = true;
                SetAnimationState(false); // Detener la animaci�n al quedar quieto
                yield return new WaitForSeconds(idleTime);
                isWaiting = false;
                SetAnimationState(true); // Reanudar la animaci�n cuando se mueva
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

    // M�todo para controlar el estado de la animaci�n
    private void SetAnimationState(bool isMoving)
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
        }
    }
}






