using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootAbility : MonoBehaviour
{
    public Transform leftCannon;
    public Transform rightCannon;
    public float fireRate = 0.2f;

    private float nextFireTime = 0f;
    private Animator animator; // Referencia al Animator

    private void Start()
    {
        animator = GetComponent<Animator>(); // Obtiene el Animator del objeto
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        // Dispara desde ambos cañones
        GenericPool.Instance.GetBullet(leftCannon.position, leftCannon.rotation * Quaternion.Euler(90, 180, 0));
        GenericPool.Instance.GetBullet(rightCannon.position, rightCannon.rotation * Quaternion.Euler(90, 180, 0));

        // Activa la animación de disparo
        if (animator != null)
        {
            animator.SetTrigger("Shoot1");
            animator.SetTrigger("Shoot2");

        }
        else
        {
            Debug.LogWarning("?? No se encontró el Animator en el objeto.");
        }
    }
}