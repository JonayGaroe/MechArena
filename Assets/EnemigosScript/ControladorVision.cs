using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorVision : MonoBehaviour
{


    public Transform Ojos;
    public float rangoVision = 20f;

    public Vector3 offset = new Vector3(0f, 0.5f, 0f);


    private ControladorNavMesh ControladorNavMesh;

    private void Awake()
    {
        
        ControladorNavMesh = GetComponent<ControladorNavMesh>();


    }


    public bool PuedeVerAlJugador(out RaycastHit hit, bool mirarHaciaElJugador = false)
    {
        Vector3 vectorDirreccion;
        if(mirarHaciaElJugador)
        {

            vectorDirreccion = (ControladorNavMesh.perseguirObjetivo.position + offset) - Ojos.position;



        }

        else
        {

            vectorDirreccion = Ojos.forward;


        }


        return Physics.Raycast(Ojos.position, vectorDirreccion, out hit, rangoVision) && hit.collider.CompareTag("Player");



    }





}
