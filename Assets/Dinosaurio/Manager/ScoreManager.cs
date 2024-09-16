using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdate;
    public static event Action<int> OnHighScoreUpdate;

    [SerializeField]
    private float tiempoParaAumentarPuntuacion = .5f;

    private int score = 0;
    private int highScore;

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void OnEnable()
    {
        GameManager.OnStart += InicializarPuntuacion;
        GameManager.OnEnd += DetenerPuntuacion;
    }

    private void OnDisable()
    {
        GameManager.OnStart -= InicializarPuntuacion;
        GameManager.OnEnd -= DetenerPuntuacion;
    }

    void InicializarPuntuacion()
    {
        AumentarPuntuacion();
        OnHighScoreUpdate?.Invoke(highScore);
    }

    void AumentarPuntuacion()
    {
        score++;
        OnScoreUpdate?.Invoke(score);
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            OnHighScoreUpdate?.Invoke(highScore);
        }
        Invoke(nameof(AumentarPuntuacion), tiempoParaAumentarPuntuacion);
    }

    void DetenerPuntuacion()
    {
        CancelInvoke(nameof(AumentarPuntuacion));
    }
}


