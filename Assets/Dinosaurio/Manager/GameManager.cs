using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static event Action OnStart;
    public static event Action OnEnd;
    public static event Action<float> OnSpeedUpdate;

    [Header("Variables del juego")]
    [SerializeField]
    private float velocidad = 5f;
    [SerializeField]
    private float incrementoVelocidad = 0.5f;

    private PlayerController jugador;
    private SpawnObstaculos spawner;
    private MoverSuelo suelo;

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

    public float Velocidad { get => velocidad; }

    public bool PartidaEnCurso { get => partidaEnCurso; }

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
        suelo = GameObject.FindWithTag("Suelo").GetComponent<MoverSuelo>();
        spawner = GameObject.FindWithTag("Spawner").GetComponent<SpawnObstaculos>();
    }

    private void Update()
    {
        if(inicio.enabled && Input.anyKey) 
        {
           IniciarPartida();
        }
    }

    public void IniciarPartida()
    {
        inicio.enabled = false;
        puntaje.enabled = true;
        ManipularComponentes(true);
        Invoke(nameof(AumentarPuntuacion), tiempoParaAumentarPuntuacion);
        partidaEnCurso = true;
        OnStart?.Invoke();
    }

    void ManipularComponentes(bool value)
    {
        jugador.enabled = value;
        spawner.enabled = value;
        suelo.enabled = value;
    }

    void AumentarPuntuacion()
    {
        puntuacion++;
        puntaje.text = puntuacion.ToString();
        if(puntuacion % 100 == 0)
        {
            SoundManager.instance.ReproducirSonido("puntaje");
            velocidad += incrementoVelocidad;
        }
        Invoke(nameof(AumentarPuntuacion), tiempoParaAumentarPuntuacion);
    }
    public void RecargarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void Perder()
    {
        fin_puntaje.text = puntaje.text;
        fin.enabled = true;
        ManipularComponentes(false);
        partidaEnCurso = false;
        Time.timeScale = 0f;
        CancelInvoke("aumentarPuntuacion");
        SoundManager.instance.ReproducirSonido("perder");
        OnEnd?.Invoke();
    }
}
