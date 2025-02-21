using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public int puntosPerdidos = 2; // Puntos a descontar cuando fallas
    public GameObject efectoExplosionMuro; // Prefab del efecto de explosión
    public float speed = 20f;
    public float lifetime = 2f;
    public float trackingStrength = 5f; // Intensidad de seguimiento al enemigo

    private Animator animator; // Referencia al Animator
    private Transform target; // Objetivo de la bala

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Obtiene el Animator del objeto
    }

    private void OnEnable()
    {
        if (animator != null)
        {
            animator.Play("BulletShoot", 0, 0); // Reproduce la animación de disparo
            Debug.Log("🎯 Animación de la bala activada con Play()");
        }

        Invoke(nameof(Deactivate), lifetime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget; // Asigna el objetivo al que la bala debe dirigirse
    }

    private void Update()
    {
        if (target != null)
        {
            // Calcula la dirección hacia el enemigo y mueve la bala hacia él
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.forward = Vector3.Lerp(transform.forward, direction, trackingStrength * Time.deltaTime);
        }
        else
        {
            // Si no hay objetivo, la bala sigue recto
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void Deactivate()
    {
        GenericPool.Instance.ReturnBullet(this.gameObject); // Devuelve la bala al pool
    }

    private void OnDisable()
    {
        CancelInvoke();
        if (animator != null)
        {
            animator.Rebind(); // Resetea la animación cuando la bala vuelve al pool
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Muro"))
        {
            gameObject.SetActive(false);
            GameController.instance.DescontarPuntos(puntosPerdidos);
            GameObject explosion = Instantiate(efectoExplosionMuro, transform.position, Quaternion.identity);
            Destroy(explosion, 2f); // Destruye la explosión después de 2 segundos
        }
    }
}