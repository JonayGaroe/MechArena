using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;

using UnityEngine;
using UnityEngine.EventSystems;


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
        /*
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
        */

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && !IsPointerOverUI())
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

        // Instancia el efecto de disparo en los cañones
        if (efectoDisparo != null)
        {
            GameObject flash1 = Instantiate(efectoDisparo, leftCannon.position, leftCannon.rotation);
            GameObject flash2 = Instantiate(efectoDisparo, rightCannon.position, rightCannon.rotation);

            Destroy(flash1, 0.2f); // Destruye el efecto después de 0.2 segundos
            Destroy(flash2, 0.2f);
        }

        // Activa la animación de disparo
        if (animator != null)
        {
            animator.SetTrigger("Shoot1");
            animator.SetTrigger("Shoot2");
        }
    }

    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0; // Si hay elementos UI detectados, retorna true
    }
}