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
    private Animator animator;

    public GameObject efectoDisparo;
    public float detectionRadius = 10f; // Radio para detectar enemigos
    public LayerMask enemyLayer; // Capa de los enemigos
    public string enemyTag = "Enemy"; // Tag de los enemigos
    public float trackingStrength = 5f; // Intensidad de seguimiento al enemigo

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && !IsPointerOverUI())
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        FireBullet(leftCannon);
        FireBullet(rightCannon);

        // Instancia el efecto de disparo en los cañones
        if (efectoDisparo != null)
        {
            GameObject flash1 = Instantiate(efectoDisparo, leftCannon.position, leftCannon.rotation);
            GameObject flash2 = Instantiate(efectoDisparo, rightCannon.position, rightCannon.rotation);
            Destroy(flash1, 0.2f);
            Destroy(flash2, 0.2f);
        }

        // Activa la animación de disparo
        if (animator != null)
        {
            animator.SetTrigger("Shoot1");
            animator.SetTrigger("Shoot2");
        }
    }

    private void FireBullet(Transform cannon)
    {
        GameObject bullet = GenericPool.Instance.GetBullet(cannon.position, cannon.rotation * Quaternion.Euler(90, 180, 0));
        BulletBehaviour bulletScript = bullet.GetComponent<BulletBehaviour>();

        if (bulletScript != null)
        {
            bulletScript.SetBulletProperties(detectionRadius, enemyLayer, enemyTag, trackingStrength);
        }
    }

    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}