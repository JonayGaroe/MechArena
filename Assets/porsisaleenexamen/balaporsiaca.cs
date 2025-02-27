using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static armaporsiaca;

public class balaporsiaca : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Aseg�rate de que el jugador tiene el tag "Player"
        {
            WeaponController weaponController = other.GetComponent<WeaponController>();

            if (weaponController != null)
            {
                weaponController.AddBullet(); // Recarga una bala
                Destroy(gameObject); // ?? Se destruye la bala despu�s de ser recogida
            }
        }
    }
}

