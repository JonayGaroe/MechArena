using System.Collections;
using System.Collections.Generic;
using StarterAssets;
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

    public AudioClip musicaDisparo;

    public AudioClip musicaRecarga;

    private StarterAssetsInputs startetinput;

    private UICanvasControllerInput inputController;


    private void Start()
    {
        animator = GetComponent<Animator>();
        startetinput = GetComponent<StarterAssetsInputs>();
        inputController = GetComponent<UICanvasControllerInput>();

    
    }


    private void Update()
    {
        if (MenuDeOpciones.Instance.partidaEnCurso == false)
        {
            
        }
        else
        {
            Shoot();
        }
        
        /* if (startetinput.shoot && Time.time >= nextFireTime && !IsPointerOverUI())
         {
             Shoot();

             startetinput.shoot = false;
         }


        if (startetinput != null && startetinput.shoot) // Solo dispara si el botón está presionado
        {
            Shoot();
        }
        */



        /*
                if (Input.GetButton("Fire1") && Time.time >= nextFireTime && !IsPointerOverUI())
                {
                    Shoot();
                    nextFireTime = Time.time + fireRate;




                }*/
        startetinput.shoot = false;
    }

    private void Shoot()
    {
        
        if (startetinput.shoot && Time.time >= nextFireTime)
        {
            FireBullet(leftCannon);
            FireBullet(rightCannon);
            //AudioSource.PlayClipAtPoint(musicaDisparo, transform.position);

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
                //AudioSource.PlayClipAtPoint(musicaRecarga, transform.position);


            }
            nextFireTime = Time.time + fireRate;
        }

    }

    private void FireBullet(Transform cannon)
    {
        GameObject bullet = GenericPool.Instance.GetBullet(cannon.position, cannon.rotation * Quaternion.Euler(90, 180, 0));
        BulletBehaviour bulletScript = bullet.GetComponent<BulletBehaviour>();

       
    }

    /*private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }*/
}