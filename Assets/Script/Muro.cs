using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muro : MonoBehaviour
{

    public GameObject efectoImpacto; // Prefab del efecto de impacto


    // Start is called before the first frame update
    void Start()
    {
        





    }

    // Update is called once per frame
    void Update()
    {
      
        



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BalaFx")) // Verifica si el objeto que choca es una bala
        {
            // Instancia el efecto de impacto en el punto de colisión
            Instantiate(efectoImpacto, collision.contacts[0].point, Quaternion.identity);

            // Destruye la bala
            collision.gameObject.SetActive(false);

            Debug.Log("hola caracola");



        }
    }






}
