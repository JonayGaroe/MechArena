using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootAbility : MonoBehaviour
{

    public Transform ca�onIzquierdo;  // Punto de disparo del ca��n izquierdo
    public Transform ca�onDerecho;   // Punto de disparo del ca��n derecho
    public float projectileSpeed = 20f;
    public float tiempoRecarga = 1f; // Tiempo entre disparos
    private bool puedeDisparar = true;

    public Animator animator; // Referencia a la animaci�n de disparo

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && puedeDisparar)
        {
            StartCoroutine(Disparar());
        }
    }

    IEnumerator Disparar()
    {
        puedeDisparar = false;

        // Activar animaci�n de disparo
        if (animator != null)
        {
            animator.SetTrigger("Disparar");
        }

        // Obtener proyectiles del pool
        GameObject proyectilIzq = GenericPool.instance.GetProjectile();
        GameObject proyectilDer = GenericPool.instance.GetProjectile();

        if (proyectilIzq != null && proyectilDer != null)
        {
            // Posicionar los proyectiles en los ca�ones
            proyectilIzq.transform.position = ca�onIzquierdo.position;
            proyectilIzq.transform.rotation = ca�onIzquierdo.rotation;

            proyectilDer.transform.position = ca�onDerecho.position;
            proyectilDer.transform.rotation = ca�onDerecho.rotation;

            // Asegurar que los proyectiles disparan hacia adelante
            proyectilIzq.GetComponent<Rigidbody>().velocity = ca�onIzquierdo.transform.forward * projectileSpeed;
            proyectilDer.GetComponent<Rigidbody>().velocity = ca�onDerecho.transform.forward * projectileSpeed;
        }

        // Esperar el tiempo de recarga antes de permitir otro disparo
        yield return new WaitForSeconds(tiempoRecarga);
        puedeDisparar = true;
    }
}