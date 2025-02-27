using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class EnemyController : MonoBehaviour
{

    public GameObject efectoExplosion; // Prefab del efecto de explosi�n


    // bool para los enemigos
    bool broken = true;

    // ANIMATOR
    Animator animator;

    public GameObject canvasPerder; // Arrastra tu Canvas aqu� en el Inspector


    public int puntos = 2; // Puntos que otorga este enemigo cuando es destruido


    public AudioClip matarEnemigo;

    //PowerUp

    public GameObject prefabPowerUp; // Prefab del power-up

    public float probabilidadPowerUp = 0.8f; // Probabilidad de que salga un power-up (20%)

    public GameObject prefabPowerUp2; // Prefab del power-up

    public float probabilidadPowerUp2 = 0.8f; // Probabilidad de que salga un power-up (20%)


    //  public ParticleSystem smokeEffect;
    // Private variables
    [SerializeField]
    TextMeshProUGUI enemieFix;

    // Start is called before the first frame update
    void Start()
    {


        EnemiesContainers.instance.AddEnemie();



    }


    // Update is called every frame
    void Update()
    {

    }

    public void SumarPuntos(int cantidad)
    {


        puntos += cantidad;





    }

    private void OnTriggerEnter(Collider other)
    {





        if (other.gameObject.CompareTag("BalaFx"))
        {

            EnemiesContainers.instance.RemoveEnemie();
            GameObject explosion = Instantiate(efectoExplosion, transform.position, Quaternion.identity);
            Destroy(explosion, 2f); // Destruye la explosi�n despu�s de 2 segundos

            Destroy(gameObject);
            Destroy(other.gameObject);

            GameController.instance.AgregarPuntos(puntos); // A�adir puntos al controlador de puntuaci�n

            AudioSource.PlayClipAtPoint(matarEnemigo, transform.position);

              if (Random.value<probabilidadPowerUp)
              {


              
               // Instanciamos el power-up en la posici�n del objeto (enemigo)
                 Instantiate(prefabPowerUp, transform.position, Quaternion.identity);

              }

            if (Random.value < probabilidadPowerUp2)
            {



                // Instanciamos el power-up en la posici�n del objeto (enemigo)
                Instantiate(prefabPowerUp2, transform.position, Quaternion.identity);

            }


        }

        /*

        if (other.CompareTag("Player")) // Aseg�rate de que tu jugador tiene el Tag "Player"
        {
            canvasPerder.SetActive(true); // Activa el Canvas de perder
            Time.timeScale = 0; // Opcional: Pausa el juego
        }

        */


    }





}



//para que se pare la musica
//   AudioSource audioSource = GetComponent<AudioSource>();

//if (audioSource != null && audioSource.isPlaying)
//  {
//   audioSource.Stop();
// }
