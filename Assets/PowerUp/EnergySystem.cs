using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using StarterAssets;
using TMPro;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public static EnergySystem instance { get; private set; }

    public float gastoDeDisparo = 33f;

    public float disparoTotal = 100f;

    public float recuperando = 3f;

    public TextMeshProUGUI textoEnergia; // El texto de la UI donde se mostrará el puntaje

    private float RecuperarVida = 0f; // Tiempo en el que se podrá recuperar nuevamente

    public float tiempoRecuperar = 10f; // tiempo Siguiente


    private PlayerShootAbility platerShoot;

    void Awake()
    {

        instance = this;



    }



    // Start is called before the first frame update
    void Start()
    {


        platerShoot = GetComponent<PlayerShootAbility>();




    }

    // Update is called once per frame
    void Update()
    {

       // PlayerShootAbility.instance.recuperando

      //  platerShoot.disparoTotal = ;

      if(PlayerShootAbility.instance.disparoTotal <= 97 && Time.time >=  RecuperarVida)

      { 
            PlayerShootAbility.instance.disparoTotal = PlayerShootAbility.instance.disparoTotal + PlayerShootAbility.instance.recuperando;



            RecuperarVida = Time.time + tiempoRecuperar;


        }

        

        textoEnergia.text = "DisparoTotal " + PlayerShootAbility.instance.disparoTotal;



    }


    public void GastoEnergia()
     {


        PlayerShootAbility.instance.disparoTotal = PlayerShootAbility.instance.disparoTotal - PlayerShootAbility.instance.gastoDeDisparo;







     }
      

   

    
    





}
