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
