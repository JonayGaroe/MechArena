using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRestaurar : MonoBehaviour
{

    public static PlayerShootAbility instance { get; private set; }

    private PowerUpRestaurar powerUp;

    private PlayerShootAbility playerShootAbility;

    public float duracionEfecto = 7f; // Duracion del efecto en segundos

    // Start is called before the first frame update
    private void Start()
    {
        playerShootAbility = GetComponent<PlayerShootAbility>();

    }
    // Update is called once per frame
    void Update()
    {
        




    }







    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Detectar si el jugador toma el power-up
        {

            PlayerShootAbility playershoot = other.GetComponent<PlayerShootAbility>();

            playershoot.CanShoot();

            // Acceder al script de control del jugador
            // MovimientoJugador controlJugador = other.GetComponent<MovimientoJugador>();
        
                
                // Invertir controles
                //    controlJugador.InvertirControles(duracionInversion);

                // Destruir el power-up
                Destroy(gameObject);
        }
    }
}




