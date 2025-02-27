using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEnergiaMaxima : MonoBehaviour
{

    public float duracionEfecto = 30f; // Duracion del efecto en segundos


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Detectar si el jugador toma el power-up
        {
            other.GetComponent<PlayerShootAbility>();

            PlayerShootAbility playershoot = other.GetComponent<PlayerShootAbility>();
            //PlayerShootAbility.PowerUp;


            playershoot.PowerUp();

            Destroy(playershoot, duracionEfecto);
            Destroy(gameObject);

            playershoot.PowerUp();

        }
    }

}
