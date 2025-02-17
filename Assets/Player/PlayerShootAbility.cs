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

    public GameObject efectoDisparo;


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
        // Dispara desde ambos ca�ones
        GenericPool.Instance.GetBullet(leftCannon.position, leftCannon.rotation * Quaternion.Euler(90, 180, 0));
        GenericPool.Instance.GetBullet(rightCannon.position, rightCannon.rotation * Quaternion.Euler(90, 180, 0));

        // Instancia el efecto de disparo en los ca�ones
        if (efectoDisparo != null)
        {
            GameObject flash1 = Instantiate(efectoDisparo, leftCannon.position, leftCannon.rotation);
            GameObject flash2 = Instantiate(efectoDisparo, rightCannon.position, rightCannon.rotation);

            Destroy(flash1, 0.2f); // Destruye el efecto despu�s de 0.2 segundos
            Destroy(flash2, 0.2f);
        }

        // Activa la animaci�n de disparo
        if (animator != null)
        {
            animator.SetTrigger("Shoot1");
            animator.SetTrigger("Shoot2");
        }
    }
}