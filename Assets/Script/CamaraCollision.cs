using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraCollision : MonoBehaviour
{
    private float minDistancian = 1;
    private float maxDistancian = 4;
    private float suavidad = 10;
    private float distancia;

    Vector3 direccion;


    // Start is called before the first frame update
   private void Start()
   {
        
        direccion = transform.localPosition.normalized;
        distancia = transform.localPosition.magnitude;


   }

    // Update is called once per frame
   private void Update()
   {
        
        Vector3 posCamara = transform.parent.TransformPoint(direccion * maxDistancian);
        RaycastHit hit;
        if (Physics.Linecast(transform.parent.position,posCamara, out hit))
        {

            distancia = Mathf.Clamp(hit.distance * 0.85f, minDistancian, maxDistancian);

        }
        else 
        {

            distancia = maxDistancian;

        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, direccion * distancia,suavidad * Time.deltaTime);




   }




}
