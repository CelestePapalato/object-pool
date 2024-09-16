using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdate;

    [SerializeField]
    private float tiempoParaAumentarPuntuacion = .5f;

    private int puntuacion = 0;

    private void OnEnable()
    {
        GameManager.OnStart += AumentarPuntuacion;
        GameManager.OnEnd += DetenerPuntuacion;
    }

    private void OnDisable()
    {
        GameManager.OnStart -= AumentarPuntuacion;
        GameManager.OnEnd -= DetenerPuntuacion;
    }
    void AumentarPuntuacion()
    {
        puntuacion++;
        OnScoreUpdate?.Invoke(puntuacion);
        Invoke(nameof(AumentarPuntuacion), tiempoParaAumentarPuntuacion);
    }

    void DetenerPuntuacion()
    {
        CancelInvoke(nameof(AumentarPuntuacion));
    }
}


