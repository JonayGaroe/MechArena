using StarterAssets;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armaporsiaca : MonoBehaviour
{
    public class WeaponController : MonoBehaviour
    {
        public Transform leftCannon;
        public Transform rightCannon;
        public float fireRate = 0.2f;
        private float nextFireTime = 0f;
        private Animator animator;

        public GameObject efectoDisparo;
        public AudioClip musicaDisparo;
        public AudioClip musicaRecarga;

        private StarterAssetsInputs startetinput;
        private UICanvasControllerInput inputController;

        // ?? Variables para limitar el número de disparos
        public int maxBullets = 3; // Máximo de balas que puede tener el jugador
        private int currentBullets; // Balas actuales disponibles

        private void Start()
        {
            animator = GetComponent<Animator>();
            startetinput = GetComponent<StarterAssetsInputs>();
            inputController = GetComponent<UICanvasControllerInput>();

            currentBullets = maxBullets; // Se inicia con el máximo de balas disponibles
        }

        private void Update()
        {
            if (!MenuDeOpciones.Instance.partidaEnCurso)
            {
                return;
            }

            if (currentBullets > 0) // ?? Solo puede disparar si tiene balas disponibles
            {
                Shoot();
            }

            startetinput.shoot = false;
        }

        private void Shoot()
        {
            if (startetinput.shoot && Time.time >= nextFireTime && currentBullets > 0) // ?? Verifica que haya balas antes de disparar
            {
                FireBullet(leftCannon);
                FireBullet(rightCannon);

                if (efectoDisparo != null)
                {
                    GameObject flash1 = Instantiate(efectoDisparo, leftCannon.position, leftCannon.rotation);
                    GameObject flash2 = Instantiate(efectoDisparo, rightCannon.position, rightCannon.rotation);
                    Destroy(flash1, 0.2f);
                    Destroy(flash2, 0.2f);
                }

                if (animator != null)
                {
                    animator.SetTrigger("Shoot1");
                    animator.SetTrigger("Shoot2");
                }

                nextFireTime = Time.time + fireRate;
                currentBullets--; // ?? Reduce la cantidad de balas al disparar
            }
        }

        private void FireBullet(Transform cannon)
        {
            GameObject bullet = GenericPool.Instance.GetBullet(cannon.position, cannon.rotation * Quaternion.Euler(90, 180, 0));
            BulletBehaviour bulletScript = bullet.GetComponent<BulletBehaviour>();

            if (CameraShake.Instance != null)
            {
                CameraShake.Instance.Shake(2f);
            }
        }

        // ?? Método para recargar balas al recogerlas del suelo
        public void AddBullet()
        {
            if (currentBullets < maxBullets) // Evita que tenga más balas que el máximo permitido
            {
                currentBullets++;
                Debug.Log("Bala recogida! Balas disponibles: " + currentBullets);
            }
        }
    }
}
