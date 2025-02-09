using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDeOpciones : MonoBehaviour
{
    //public GameController gameController; no esta detectando un archivo con este nombre por eso te da error quiere decir que no tienes un archivo con este nombre en tu project

    float tiempoDePartida = 0.0f;
    public static MenuDeOpciones Instance;

    public GameObject canvasAjustes;
    [SerializeField] GameObject canvasPlay;
    [SerializeField] GameObject canvasGanar;
    [SerializeField] GameObject canvasPerder;

    [SerializeField] TextMeshProUGUI tiempopartida;

    bool isPause;
    float tiempoRestante = 420f; // 7 minutos en segundos
    bool partidaEnCurso = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1;
        partidaEnCurso = true;
    }

    void Update()
    {
        if (partidaEnCurso)
        {
            tiempoDePartida += Time.unscaledDeltaTime; // Usamos tiempo independiente de la escala
            tiempoRestante -= Time.unscaledDeltaTime;

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                FinalizarPartida();
            }

            ActualizarTiempoUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AlternarPausa();
        }
    }

    void ActualizarTiempoUI()
    {
        float minutos = Mathf.FloorToInt(tiempoRestante / 60F);
        float segundos = Mathf.FloorToInt(tiempoRestante % 60F);
        tiempopartida.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    void FinalizarPartida()
    {
        partidaEnCurso = false;
        Time.timeScale = 0; // Pausar el juego
        canvasPerder.SetActive(true);
    }

    void AlternarPausa()
    {
        isPause = !isPause;
        Time.timeScale = isPause ? 0 : 1;
        canvasAjustes.SetActive(isPause);

        if (isPause)
        {
            LeanTween.alphaCanvas(canvasAjustes.GetComponent<CanvasGroup>(), 1, 0.5f).setIgnoreTimeScale(true);
        }
        else
        {
            LeanTween.alphaCanvas(canvasAjustes.GetComponent<CanvasGroup>(), 0, 0.5f).setOnComplete(() =>
            {
                canvasAjustes.SetActive(false);
            }).setIgnoreTimeScale(true);
        }
    }

    public void BotonPlay()
    {
        canvasAjustes.SetActive(false);
        Time.timeScale = 1;
    }

    public void BotonSalir()
    {
        Time.timeScale = 0;
        canvasAjustes.SetActive(true);
        LeanTween.alphaCanvas(canvasAjustes.GetComponent<CanvasGroup>(), 1, 0.5f).setIgnoreTimeScale(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}