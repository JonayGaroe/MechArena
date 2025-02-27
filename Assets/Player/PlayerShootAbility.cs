using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEditor.Playables;

using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerShootAbility : MonoBehaviour
{

    // Referencias a los cañones izquierdo y derecho desde donde se disparan las balas
    public Transform leftCannon;
    public Transform rightCannon;

    // Tiempo de espera entre disparos
    public float fireRate = 0.2f;
    private float nextFireTime = 0f; // Tiempo en el que se podrá disparar nuevamente

    // Controlador de animaciones
    private Animator animator;

    // Efecto visual del disparo (muzzle flash)
    public GameObject efectoDisparo;

    // Clips de sonido para disparo y recarga
    public AudioClip musicaDisparo;
    public AudioClip musicaRecarga;

    // Referencias a los sistemas de entrada
    private StarterAssetsInputs startetinput;
    private UICanvasControllerInput inputController;

    private void Start()
    {
        // Se obtienen los componentes necesarios al inicio del juego
        animator = GetComponent<Animator>();
        startetinput = GetComponent<StarterAssetsInputs>();
        inputController = GetComponent<UICanvasControllerInput>();
    }

    private void Update()
    {
        // Verifica si la partida está en curso antes de permitir disparar
        if (MenuDeOpciones.Instance.partidaEnCurso == false)
        {
            return; // Si la partida no está en curso, no hace nada
        }
        else
        {
            Shoot(); // Si la partida está activa, ejecuta la lógica de disparo
        }

        // Reinicia la entrada del disparo después de procesarla
        startetinput.shoot = false;
    }

    private void Shoot()
    {
        // Verifica si el jugador ha presionado el botón de disparo y si ha pasado el tiempo necesario para disparar de nuevo
        if (startetinput.shoot && Time.time >= nextFireTime)
        {
            // Dispara desde ambos cañones
            FireBullet(leftCannon);
            FireBullet(rightCannon);

            // Se podría reproducir el sonido de disparo aquí (descomentando la línea siguiente)
            // AudioSource.PlayClipAtPoint(musicaDisparo, transform.position);

            // Si hay un efecto de disparo (muzzle flash), se instancia en ambos cañones
            if (efectoDisparo != null)
            {
                GameObject flash1 = Instantiate(efectoDisparo, leftCannon.position, leftCannon.rotation);
                GameObject flash2 = Instantiate(efectoDisparo, rightCannon.position, rightCannon.rotation);

                // Se destruyen los efectos después de 0.2 segundos para evitar que queden en la escena
                Destroy(flash1, 0.2f);
                Destroy(flash2, 0.2f);
            }

            // Activa la animación de disparo si existe un Animator asignado
            if (animator != null)
            {
                animator.SetTrigger("Shoot1");
                animator.SetTrigger("Shoot2");

                // Se podría reproducir el sonido de recarga aquí (descomentando la línea siguiente)
                // AudioSource.PlayClipAtPoint(musicaRecarga, transform.position);
            }

            // Se actualiza el tiempo para el siguiente disparo
            nextFireTime = Time.time + fireRate;
        }
    }

    private void FireBullet(Transform cannon)
    {
        // Obtiene una bala del pool de objetos y la instancia en la posición y rotación del cañón
        GameObject bullet = GenericPool.Instance.GetBullet(cannon.position, cannon.rotation * Quaternion.Euler(90, 180, 0));

        // Se obtiene el script de comportamiento de la bala
        BulletBehaviour bulletScript = bullet.GetComponent<BulletBehaviour>();

        // Si hay un sistema de sacudida de cámara, se activa al disparar
        if (CameraShake.Instance != null)
        {
            Debug.Log("Shake Activado con Impulse Source.");
            CameraShake.Instance.Shake(2f); // Ajusta la intensidad de la sacudida de cámara
        }
    }
}