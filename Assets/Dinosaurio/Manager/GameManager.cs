using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Variables del juego")]
    [SerializeField]
    private float velocidad = 5f;
    [SerializeField]
    private float incrementoVelocidad = 0.5f;

    private PlayerController jugador;
    private SpawnObstaculos spawner;
    private moverSuelo suelo;

    [Header("Componentes de la interfaz de usuario")]
    [SerializeField]
    private Canvas fin;
    [SerializeField]
    private Canvas inicio;
    [SerializeField]
    private TMP_Text puntaje;
    [SerializeField]
    private TMP_Text fin_puntaje;

    [SerializeField]
    private float tiempoParaAumentarPuntuacion = .5f;

    private int puntuacion;

    bool partidaEnCurso = false;
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        puntaje.enabled = false;
        fin.enabled = false;
        inicio.enabled = true;

        jugador = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        suelo = GameObject.FindWithTag("Suelo").GetComponent<moverSuelo>();
        spawner = GameObject.FindWithTag("Spawner").GetComponent<SpawnObstaculos>();
    }

    private void Update()
    {
        if(inicio.enabled && Input.anyKey) 
        {
           iniciarPartida();
        }
    }

    public void iniciarPartida()
    {
        inicio.enabled = false;
        puntaje.enabled = true;
        manipularComponentes(true);
        Invoke("aumentarPuntuacion", tiempoParaAumentarPuntuacion);
        partidaEnCurso = true;
    }

    void manipularComponentes(bool value)
    {
        jugador.enabled = value;
        spawner.enabled = value;
        suelo.enabled = value;
    }

    void aumentarPuntuacion()
    {
        puntuacion++;
        puntaje.text = puntuacion.ToString();
        if(puntuacion % 100 == 0)
        {
            SoundManager.instance.reproducirSonido("puntaje");
            velocidad += incrementoVelocidad;
        }
        Invoke("aumentarPuntuacion", tiempoParaAumentarPuntuacion);
    }
    public void recargarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void perder()
    {
        fin_puntaje.text = puntaje.text;
        fin.enabled = true;
        manipularComponentes(false);
        partidaEnCurso = false;
        Time.timeScale = 0f;
        CancelInvoke("aumentarPuntuacion");
        SoundManager.instance.reproducirSonido("perder");
    }

    public float getVelocidad()
    {
        return velocidad;
    }

    public bool empezoLaPartida()
    {
        return partidaEnCurso;
    }
}
