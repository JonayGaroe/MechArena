using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPersecucion : MonoBehaviour
{

    public Color colorEstado = Color.red;


    private MaquinaDeEstado maquinadeEstados;

    private ControladorVision controladorVision;

    private ControladorNavMesh controladorNavMesh;
    //
    public AudioClip sonidoPersecucion;




    void Awake()
    {


        maquinadeEstados = GetComponent<MaquinaDeEstado>();

        controladorNavMesh = GetComponent<ControladorNavMesh>();

        controladorVision = GetComponent<ControladorVision>();



    }



    void OnEnable()
    {

        maquinadeEstados.meshrendererIndicador.material.color = colorEstado;






    }






    void Update()
    {

        AudioSource.PlayClipAtPoint(sonidoPersecucion, transform.position);


        RaycastHit hit;
        // si el enemigo esta mirando hacia arriba 
        if(!controladorVision.PuedeVerAlJugador(out hit, true ))
        {

            maquinadeEstados.ActivarEstado(maquinadeEstados.EstadoAlerta);
            return;

            // los return es para que no se reactive el controlador punto de destino por el navmeshagent
        }



        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent();




    }








}
