using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorVision : MonoBehaviour
{


    public Transform Ojos;
    public float rangoVision = 50f;

    public Vector3 offset = new Vector3(0f, 0.5f, 0f);


    public GameObject canvasPerder; // Arrastra aquí el Canvas de "Perder"
    public float distanciaRaycast = 3f; // Distancia del Raycast


    private ControladorNavMesh ControladorNavMesh;

    private void Awake()
    {
        
        ControladorNavMesh = GetComponent<ControladorNavMesh>();


    }

    void Update()
    {
        RaycastHit hit;

        // Lanza un Raycast hacia adelante
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanciaRaycast))
        {
            // Opcional: Ver el objeto detectado en la consola
            Debug.Log("Objeto Detectado: " + hit.collider.gameObject.name);

            // Si el objeto detectado es el jugador (o cualquier otro que definas)
            if (hit.collider.CompareTag("Player")) // Asegúrate de que el jugador tiene el Tag "Player"
            {
                PerderPartida();
            }
        }
    }



    void PerderPartida()
    {
        canvasPerder.SetActive(true); // Muestra el Canvas de perder
        Time.timeScale = 0; // Pausa el juego
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
