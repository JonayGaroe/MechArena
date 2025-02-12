using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoLLanta : MonoBehaviour
{

    public float velocidadRotacion = 200f; // Ajusta la velocidad de rotación



    // Start is called before the first frame update
    void Start()
    {
        




    }

    // Update is called once per frame
    void Update()
    {


        // Rotar la rueda en el eje X para que gire hacia adelante
        transform.Rotate(Vector3.right * velocidadRotacion * Time.deltaTime);


    }






}
