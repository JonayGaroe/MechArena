using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugadorporsiaca : MonoBehaviour
{
    private bool isBoosted = false; // Si el PowerUp está activo
    void Update()
    {
        // Comprobar si el boost de velocidad ha expirado
       // if //(isBoosted && Time.time > boostEndTime)
        {
            //DeactivateSpeedBoost(); // Desactivar el boost de velocidad si el tiempo ha pasado
        }
    }
    /*
    public void ActivateSpeedBoost(float duration)
    {
        if (!isBoosted)
        {
            isBoosted = true;
            currentSpeed = boostedSpeed; // Aumentamos la velocidad
            boostEndTime = Time.time + duration; // Establecemos el tiempo para desactivar el PowerUp
        }
    }

    private void DeactivateSpeedBoost()
    {
        isBoosted = false;
        currentSpeed = speed; // Restauramos la velocidad normal
    }
    */


}



