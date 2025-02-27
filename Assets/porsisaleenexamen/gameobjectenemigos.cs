using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameobjectenemigos : MonoBehaviour
{

    public GameObject prefabPowerUp; // Prefab del power-up
    public float probabilidadPowerUp = 0.8f; // Probabilidad de que salga un power-up (20%)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {





    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Enemigos"))
        {


          //  if (Random.value<probabilidadPowerUp3)    
                      // Instanciamos el power-up en la posición del objeto (enemigo)
            //   Instantiate(prefabPowerUp3, transform.position, Quaternion.identity);




        }
    }
}






