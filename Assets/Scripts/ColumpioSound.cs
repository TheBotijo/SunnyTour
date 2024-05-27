using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumpioSound : MonoBehaviour
{
    public AudioSource Columpio;
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private bool isMoving = false;


    void Start()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    void Update()
    {
        // Si el objeto se est� moviendo o rotando y el audio no se est� reproduciendo, empezamos la reproducci�n.
        if ((transform.position != lastPosition || transform.rotation != lastRotation) && !Columpio.isPlaying && !isMoving)
        {
            Columpio.Play();
            isMoving = true;
        }
        // Si el objeto se detuvo y el audio se est� reproduciendo, detenemos la reproducci�n.
        else if (transform.position == lastPosition && transform.rotation == lastRotation && Columpio.isPlaying && isMoving)
        {
            Columpio.Stop();
            isMoving = false;
        }
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }
}
