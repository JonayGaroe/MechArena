using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public int puntosPerdidos = 2;
    public GameObject efectoExplosionMuro;
    public float speed = 20f;
    public float lifetime = 2f;
    private Animator animator;
    private Transform target;
    private float detectionRadius;
    private LayerMask enemyLayer;
    private string enemyTag;
    private float trackingStrength;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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

    public void SetBulletProperties(float radius, LayerMask layer, string tag, float tracking)
    {
        detectionRadius = radius;
        enemyLayer = layer;
        enemyTag = tag;
        trackingStrength = tracking;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.forward = Vector3.Lerp(transform.forward, direction, trackingStrength * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(enemyTag))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = hitCollider.transform;
                }
            }
        }

        if (closestTarget != null)
        {
            target = closestTarget;
        }
    }

    private void Deactivate()
    {
        GenericPool.Instance.ReturnBullet(this.gameObject);
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
        if (other.gameObject.CompareTag("Muro"))
        {
            gameObject.SetActive(false);
            GameController.instance.DescontarPuntos(puntosPerdidos);
            GameObject explosion = Instantiate(efectoExplosionMuro, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
        }
    }
}