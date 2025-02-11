using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPatrulla : MonoBehaviour
{

    public Transform[] wayPoint;    

    private ControladorNavMesh controladorNavMesh;

    private int siguientewayPoint;

    private MaquinaDeEstado maquinadeEstados;

    private ControladorVision controladorVision;

    //

    // Start is called before the first frame update
    void Awake()
    {
        
        controladorNavMesh = GetComponent<ControladorNavMesh>();
        maquinadeEstados = GetComponent<MaquinaDeEstado>();
        controladorVision = GetComponent<ControladorVision>();



    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        if(controladorVision.PuedeVerAlJugador(out hit))
        {

            controladorNavMesh.perseguirObjetivo = hit.transform;

            maquinadeEstados.ActivarEstado(maquinadeEstados.EstadoPersecuion);

            return;
                    
        }   




        if(controladorNavMesh.HemosLlegado())
        {


          siguientewayPoint = (siguientewayPoint + 1) % wayPoint.Length;
            ActualizaWayPointDestino();


        }

         



    }

    private void OnEnable()
    {

        //siguientewayPoint = 0;
        ActualizaWayPointDestino();



    }

    void ActualizaWayPointDestino()
    {


        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent(wayPoint[siguientewayPoint].position);




    }


    public void OnTriggerEnter(Collider other)
    {
        

        if(other.gameObject.CompareTag("Player"))
        {



            maquinadeEstados.ActivarEstado(maquinadeEstados.EstadoAlerta);







        }





    }






}
