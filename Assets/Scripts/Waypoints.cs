using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [Range(0f, 2f)]
    [SerializeField] private float waypointSize = 1f;

    private int direction = 1; // 1 para avanzar, -1 para retroceder

    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, waypointSize);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null)
        {
            return transform.GetChild(0);
        }

        int currentIndex = currentWaypoint.GetSiblingIndex();
        int nextIndex = currentIndex + direction;

        if (nextIndex >= transform.childCount || nextIndex < 0)
        {
            direction *= -1;
            nextIndex = currentIndex + direction;
        }

        return transform.GetChild(nextIndex);
    }

    // Método para invertir la dirección
    public void ReverseDirection()
    {
        direction *= -1;
    }
}


