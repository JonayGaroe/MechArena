using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignCameraHolder : MonoBehaviour
{
    public Transform mainCamera;

    void Start()
    {
        if (mainCamera != null)
        {
            transform.position = mainCamera.position;
            transform.rotation = mainCamera.rotation;
            Debug.Log("CameraHolder alineado con Main Camera.");
        }
        else
        {
            Debug.LogError("¡No se asignó la Main Camera en AlignCameraHolder!");
        }
    }
}