using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public int puntosPerdidos = 2;
    public GameObject efectoExplosionMuro;
    public float speed = 20f;
    public float lifetime = 2f;
    public float detectionRadius = 20f;
    public float trackingStrength = 40f;

    // Musica
    public AudioClip falloMuro;



    // Se definen manualmente el tag y la layer para los enemigos.
    public string enemyTag = "Enemy";
    public LayerMask enemyLayer;

    private Animator animator;
    private Transform target;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        // Se asigna manualmente la layer "Enemy". Asegúrate de que la layer se llame exactamente "Enemy" en el Editor.
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void OnEnable()
    {
        if (animator != null)
        {
            animator.Play("BulletShoot", 0, 0);
        }
        Invoke(nameof(Deactivate), lifetime);
        FindTarget();
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            Debug.Log("Buscando target...");
        }

        if (target != null)
        {
            // Calcula la dirección deseada hacia el target y realiza una interpolación para suavizar la rotación.
            Vector3 desiredDirection = (target.position - transform.position).normalized;
            Vector3 newDirection = Vector3.Lerp(transform.forward, desiredDirection, trackingStrength * Time.deltaTime).normalized;
            transform.forward = newDirection;
            transform.position += newDirection * speed * Time.deltaTime;
            Debug.Log("Target asignado: " + target.name);
        }
        else
        {
            // Sin target, mueve la bala en la dirección predeterminada. Se usa -Vector3.forward para ajustarla.
            transform.Translate(-Vector3.forward * speed * Time.deltaTime, Space.Self);
            Debug.Log("No se encontró target, moviendo en dirección predeterminada.");
        }
    }

    // Busca enemigos dentro del radio especificado y selecciona el que esté más cerca del centro de la pantalla.
    private void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        float closestScreenDistance = Mathf.Infinity;
        Transform closestTarget = null;
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        foreach (Collider collider in hitColliders)
        {
            // Verifica si el objeto tiene el tag "Enemy".
            if (collider.CompareTag(enemyTag))
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(collider.transform.position);
                float distanceToCenter = Vector2.Distance(new Vector2(screenPos.x, screenPos.y), screenCenter);
                if (distanceToCenter < closestScreenDistance)
                {
                    closestScreenDistance = distanceToCenter;
                    closestTarget = collider.transform;
                }
            }
        }

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

    private void Deactivate()
    {
        GenericPool.Instance.ReturnBullet(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke();
        if (animator != null)
        {
            animator.Rebind();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag("Muro"))
        {
           // gameObject.SetActive(false);
            GameController.instance.DescontarPuntos(puntosPerdidos);
            GameObject explosion = Instantiate(efectoExplosionMuro, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            // AudioSource.PlayClipAtPoint(falloMuro, transform.position);
            GenericPool.Instance.ReturnBullet(gameObject);


        }
    }
}

