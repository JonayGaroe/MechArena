using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaDeEstado : MonoBehaviour
{
    // es tipo monobegaviour
    public MonoBehaviour EstadoPatrulla;
    public MonoBehaviour EstadoAlerta;
    public MonoBehaviour EstadoPersecuion;
    public MonoBehaviour EstadoInicial;


    private MonoBehaviour estadoActual;


    // Start is called before the first frame update
    void Start()
    {
        ActivarEstado(EstadoInicial);














    }

    // Update is called once per frame
    


    public void ActivarEstado(MonoBehaviour nuevoEstado)
    {

        if(estadoActual!=null) estadoActual.enabled = false;
        estadoActual = nuevoEstado;
        estadoActual.enabled = true;     


    }











}
