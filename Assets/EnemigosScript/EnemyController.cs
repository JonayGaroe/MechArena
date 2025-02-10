using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class EnemyController : MonoBehaviour
{

    // bool para los enemigos
    bool broken = true;

    // ANIMATOR
    Animator animator;



   

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


    


   

}



//para que se pare la musica
//   AudioSource audioSource = GetComponent<AudioSource>();

//if (audioSource != null && audioSource.isPlaying)
//  {
//   audioSource.Stop();
// }