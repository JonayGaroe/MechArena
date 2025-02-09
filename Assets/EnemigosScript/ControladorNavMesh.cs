using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControladorNavMesh : MonoBehaviour
{
    // para q no salga en inspector
    [HideInInspector]
    public Transform perseguirObjetivo;

    private NavMeshAgent navMeshAgent;


    // Start is called before the first frame update
    void Awake()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();



    }

    // Update is called once per frame
    public void ActualizarPuntoDestinoNavMeshAgent(Vector3 puntoDestino)
    {

        navMeshAgent.destination = puntoDestino;
        navMeshAgent.Resume();



    }

    public void ActualizarPuntoDestinoNavMeshAgent()
    {


        ActualizarPuntoDestinoNavMeshAgent(perseguirObjetivo.position);



    }




     public void DetenernavMeshAgent()
     {

        navMeshAgent.Stop();



     }


    public bool HemosLlegado()
    {
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending;


    }



}
