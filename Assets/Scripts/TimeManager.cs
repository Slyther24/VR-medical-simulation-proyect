using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public int tiempoLimite = 300; // 5 minutos en segundos
    public TMP_Text temporizadorTexto; // UI Text para el temporizador
    public Slider barraDeSalud; // UI Slider para la barra de salud
    public int saludMaxima = 100; // Salud inicial y máxima del jugador
    public int danoPorFantasma = 20; // Cantidad de salud que pierde al colisionar con un "fantasma"

    public GameObject panelVictoria; // Panel de victoria
    public GameObject panelDerrota; // Panel de derrota
    public Button botonReiniciar; // Botón para reiniciar
    public AudioSource muerte;

    private float tiempoRestante;
    private int saludActual;
    private bool juegoTerminado = false;

    void Start()
    {
        tiempoRestante = tiempoLimite;
        saludActual = saludMaxima;
        barraDeSalud.maxValue = saludMaxima;
        barraDeSalud.value = saludActual;

        // Asegurarse de que los paneles de victoria y derrota estén desactivados al inicio
        panelVictoria.SetActive(false);
        panelDerrota.SetActive(false);

        // Asignar función al botón de reinicio
        botonReiniciar.onClick.AddListener(ReiniciarJuego);
    }

    void Update()
    {
        if (juegoTerminado) return;

        // Actualizar el temporizador
        tiempoRestante -= Time.deltaTime;
        int minutos = Mathf.FloorToInt(tiempoRestante / 60);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60);
        temporizadorTexto.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        // Revisar si se acabó el tiempo
        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            FinDelJuego(true); // Ganas cuando se acaba el tiempo
        }

        // Revisar si se ha quedado sin salud
        if (saludActual <= 0)
        {
            FinDelJuego(false); // Pierdes si la salud llega a 0
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fantasma"))
        {
            muerte.Play();
            ReducirSalud(danoPorFantasma);
        }
    }

    void ReducirSalud(int cantidad)
    {
        saludActual -= cantidad;
        barraDeSalud.value = saludActual;

        if (saludActual <= 0)
        {
            FinDelJuego(false);
        }
    }

    void FinDelJuego(bool victoria)
    {
        juegoTerminado = true;
        Time.timeScale = 0f; // Pausar el juego

        if (victoria)
        {
            panelVictoria.SetActive(true); // Mostrar el panel de victoria
        }
        else
        {
            panelDerrota.SetActive(true); // Mostrar el panel de derrota
        }
    }

    void ReiniciarJuego()
    {
        Time.timeScale = 1f; // Restablecer la escala de tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recargar la escena actual
    }
}


