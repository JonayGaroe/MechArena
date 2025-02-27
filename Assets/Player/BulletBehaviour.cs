using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Cantidad de puntos que se restarán al jugador si la bala impacta un muro.
    public int puntosPerdidos = 2;

    // Efecto de explosión que se genera al impactar un muro.
    public GameObject efectoExplosionMuro;

    // Velocidad de la bala.
    public float speed = 20f;

    // Tiempo de vida de la bala antes de desactivarse automáticamente.
    public float lifetime = 2f;

    // Radio en el que la bala detecta enemigos para hacer seguimiento.
    public float detectionRadius = 20f;

    // Intensidad con la que la bala ajusta su trayectoria hacia el enemigo detectado.
    public float trackingStrength = 40f;

    // Sonido que se reproduce cuando la bala impacta un muro.
    public AudioClip falloMuro;

    // Tag y Layer para identificar a los enemigos.
    public string enemyTag = "Enemy";
    public LayerMask enemyLayer;

    // Referencias internas
    private Animator animator; // Controlador de animaciones.
    private Transform target;  // Objetivo al que se dirigirá la bala.

    private void Awake()
    {
        // Obtiene el componente Animator del objeto.
        animator = GetComponent<Animator>();

        // Se obtiene la layer de los enemigos basada en su nombre definido en el Editor de Unity.
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void OnEnable()
    {
        // Si hay una animación de disparo, se reproduce cuando la bala es activada.
        if (animator != null)
        {
            animator.Play("BulletShoot", 0, 0);
        }

        // Programa la desactivación de la bala después del tiempo de vida establecido.
        Invoke(nameof(Deactivate), lifetime);

        // Intenta encontrar un enemigo como objetivo.
        FindTarget();
    }

    private void Update()
    {
        // Si no hay un objetivo asignado, intenta buscar uno.
        if (target == null)
        {
            FindTarget();
            Debug.Log("Buscando target...");
        }

        if (target != null)
        {
            // Calcula la dirección hacia el objetivo.
            Vector3 desiredDirection = (target.position - transform.position).normalized;

            // Interpola la dirección de la bala para que el seguimiento sea suave.
            Vector3 newDirection = Vector3.Lerp(transform.forward, desiredDirection, trackingStrength * Time.deltaTime).normalized;

            // Ajusta la dirección de la bala hacia el objetivo.
            transform.forward = newDirection;

            // Mueve la bala en la dirección calculada.
            transform.position += newDirection * speed * Time.deltaTime;

            Debug.Log("Target asignado: " + target.name);
        }
        else
        {
            // Si no hay objetivo, la bala se mueve en línea recta en la dirección original.
            transform.Translate(-Vector3.forward * speed * Time.deltaTime, Space.Self);
            Debug.Log("No se encontró target, moviendo en dirección predeterminada.");
        }
    }

    // Método que busca enemigos dentro de un radio de detección.
    private void FindTarget()
    {
        // Obtiene todos los colliders dentro del radio de detección que pertenecen a la capa de enemigos.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        float closestScreenDistance = Mathf.Infinity; // Distancia más corta al centro de la pantalla.
        Transform closestTarget = null; // Variable para almacenar el objetivo más cercano.

        // Se calcula el centro de la pantalla.
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        // Recorre todos los objetos detectados.
        foreach (Collider collider in hitColliders)
        {
            // Verifica si el objeto detectado tiene el tag de enemigo.
            if (collider.CompareTag(enemyTag))
            {
                // Convierte la posición del enemigo a coordenadas de pantalla.
                Vector3 screenPos = Camera.main.WorldToScreenPoint(collider.transform.position);

                // Calcula la distancia del enemigo al centro de la pantalla.
                float distanceToCenter = Vector2.Distance(new Vector2(screenPos.x, screenPos.y), screenCenter);

                // Si este enemigo está más cerca del centro de la pantalla que los anteriores, lo selecciona como target.
                if (distanceToCenter < closestScreenDistance)
                {
                    closestScreenDistance = distanceToCenter;
                    closestTarget = collider.transform;
                }
            }
        }

        // Se asigna el enemigo más cercano como objetivo de la bala.
        target = closestTarget;

        if (target != null)
        {
            Debug.Log("Target encontrado: " + target.name);
        }
        else
        {
            Debug.Log("No se encontró target.");
        }
    }

    // Método para desactivar la bala y devolverla al pool.
    private void Deactivate()
    {
        GenericPool.Instance.ReturnBullet(gameObject);
    }

    private void OnDisable()
    {
        // Cancela cualquier "Invoke" programado cuando la bala es desactivada.
        CancelInvoke();

        // Reinicia la animación para evitar errores visuales en futuras activaciones.
        if (animator != null)
        {
            animator.Rebind();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si la bala ha impactado un enemigo o un muro.
        if (other.CompareTag(enemyTag) || other.CompareTag("Muro"))
        {
            // Llama a la función del GameController para restar puntos al jugador si la bala impacta un muro.
            GameController.instance.DescontarPuntos(puntosPerdidos);

            // Instancia un efecto de explosión en la posición de la bala.
            GameObject explosion = Instantiate(efectoExplosionMuro, transform.position, Quaternion.identity);

            // Destruye el efecto de explosión después de 2 segundos para liberar memoria.
            Destroy(explosion, 2f);

            // Se podría reproducir un sonido de impacto (descomentando la línea siguiente).
            // AudioSource.PlayClipAtPoint(falloMuro, transform.position);

            // Devuelve la bala al pool para su reutilización.
            GenericPool.Instance.ReturnBullet(gameObject);
        }
    }
}