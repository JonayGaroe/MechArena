using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuDeOpciones : MonoBehaviour
{
    float tiempoDePartida = 0.0f;
    public static MenuDeOpciones Instance;

    public GameObject canvasAjustes;
    public GameObject canvasSetting;

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
        AlternarPausa();
        partidaEnCurso = false;
        canvasAjustes.SetActive(true);
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

        Cursor.visible = isPause;

        Cursor.lockState = isPause ? CursorLockMode.None : CursorLockMode.Locked;




    }

    public void BotonPlay()
    {
        partidaEnCurso = true;
        Time.timeScale = 1;
        canvasAjustes.SetActive(false);
    }
    public void BotonAjustes()
    {
        Time.timeScale = 0;
        canvasSetting.SetActive(true);
        canvasAjustes.SetActive(false);
    }

    public void Menu()
    {

        canvasSetting.SetActive(false);
        canvasAjustes.SetActive(true);



    }

    public void Replay()
    {


        SceneManager.LoadScene("JuegoNormal"); // Cargar la Scena del nombre ejemplo SampleScene




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

    // NUEVO: Método para ganar la partida
    public void GanarPartida()
    {
        if (!partidaEnCurso) return;

        partidaEnCurso = false;
        Time.timeScale = 0; // Pausar el juego
        canvasGanar.SetActive(true); // Muestra el Canvas de Ganar

        // Calcular puntos extra por tiempo restante
        int segundosRestantes = Mathf.FloorToInt(tiempoRestante);
        int puntosExtra = segundosRestantes * 10;

        // Agregar los puntos extra usando GameController
        GameController.instance.AgregarPuntos(puntosExtra);
    }
}