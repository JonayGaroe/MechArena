using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public float speed = 20f;
    public float lifetime = 2f;

    private Animator animator; // Referencia al Animator

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Obtiene el Animator del objeto
    }

    private void OnEnable()
    {
        if (animator != null)
        {
            animator.Play("BulletShoot", 0, 0); // Reproduce directamente la animación
            Debug.Log("?? Animación de la bala activada con Play()");
        }

        Invoke(nameof(Deactivate), lifetime);
    }

    private void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
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







}
    