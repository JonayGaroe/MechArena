using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAlerta : MonoBehaviour
{
    public float velocidadGiro = 120f;

    public float Busqueda = 4f;

    private float tiempoBuscando;

    public Color colorEstado = Color.yellow;


    private MaquinaDeEstado maquinadeEstados;

    private ControladorVision controladorVision;

   private ControladorNavMesh controladorNavMesh;

    public AudioClip sonidoAlerta;



    void Awake()
    {



        maquinadeEstados = GetComponent<MaquinaDeEstado>();

        controladorNavMesh = GetComponent<ControladorNavMesh>();

        controladorVision = GetComponent<ControladorVision>();
    }


    void OnEnable()
    {

        maquinadeEstados.meshrendererIndicador.material.color = colorEstado;

        controladorNavMesh.DetenernavMeshAgent();

        tiempoBuscando = 0f;




    }









    void Update()
    {
        AudioSource.PlayClipAtPoint(sonidoAlerta, transform.position);


        RaycastHit hit;
        if (controladorVision.PuedeVerAlJugador(out hit))
        {

            controladorNavMesh.perseguirObjetivo = hit.transform;

            maquinadeEstados.ActivarEstado(maquinadeEstados.EstadoPersecuion);

            return;

        }




        // le damos rotacion en eje Y ya que queremos que gire sobre el propio
        transform.Rotate(0, velocidadGiro * Time.deltaTime, 0);

        tiempoBuscando += Time.deltaTime;

        if(tiempoBuscando >= Busqueda)
        {

            maquinadeEstados.ActivarEstado(maquinadeEstados.EstadoPatrulla);

            return;





        }






    }







}
