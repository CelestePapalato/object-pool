using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdate;
    public static event Action<int> OnHighScoreUpdate;
    private static int highScore;

    [SerializeField]
    private float tiempoParaAumentarPuntuacion = .5f;

    private int score = 0;

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
            OnHighScoreUpdate?.Invoke(highScore);
        }
        Invoke(nameof(AumentarPuntuacion), tiempoParaAumentarPuntuacion);
    }

    void DetenerPuntuacion()
    {
        CancelInvoke(nameof(AumentarPuntuacion));
    }
}


