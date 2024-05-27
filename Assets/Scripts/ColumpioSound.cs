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
        // Si el objeto se está moviendo o rotando y el audio no se está reproduciendo, empezamos la reproducción.
        if ((transform.position != lastPosition || transform.rotation != lastRotation) && !Columpio.isPlaying && !isMoving)
        {
            Columpio.Play();
            isMoving = true;
        }
        // Si el objeto se detuvo y el audio se está reproduciendo, detenemos la reproducción.
        else if (transform.position == lastPosition && transform.rotation == lastRotation && Columpio.isPlaying && isMoving)
        {
            Columpio.Stop();
            isMoving = false;
        }
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }
}
