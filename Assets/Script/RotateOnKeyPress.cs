using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnKeyPress : MonoBehaviour
{
    // Start is called before the first frame update


    private Quaternion initialRotation;  // Rotación inicial
    public float rotationAngle = 10f;    // Ángulo de rotación en X
    public float rotationSpeed = 5f;     // Velocidad de interpolación


    void Start()
    {
        initialRotation = transform.rotation;  // Guarda la rotación inicial
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Rotar a 10° en el eje X
            Quaternion targetRotation = Quaternion.Euler(rotationAngle, transform.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            // Volver a la rotación inicial
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime * rotationSpeed);
        }
    }
}

