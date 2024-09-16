using UnityEngine;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    bool enableOnStart;
    [SerializeField]
    bool disableOnEnd;
    [SerializeField]
    TMP_Text scoreText;
    [SerializeField]
    TMP_Text highScoreText;

    Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    private void OnEnable()
    {
        ScoreManager.OnScoreUpdate += UpdateScore;
        ScoreManager.OnHighScoreUpdate += UpdateHighScore;
        if (enableOnStart)
        {
            GameManager.OnStart += EnableCanvas;
        }
        else
        {
            GameManager.OnStart += DisableCanvas;
        }
        if(disableOnEnd)
        {
            GameManager.OnEnd += DisableCanvas;
        }
        else
        {
            GameManager.OnEnd += EnableCanvas;
        }
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreUpdate -= UpdateScore;
        ScoreManager.OnHighScoreUpdate -= UpdateHighScore;
        if (enableOnStart)
        {
            GameManager.OnStart -= EnableCanvas;
        }
        else
        {
            GameManager.OnStart -= DisableCanvas;
        }
        if (disableOnEnd)
        {
            GameManager.OnEnd -= DisableCanvas;
        }
        else
        {
            GameManager.OnEnd -= EnableCanvas;
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
    
    public void UpdateHighScore(int highScore)
    {
        highScoreText.text = highScore.ToString();
    }

    public void EnableCanvas()
    {
        if (canvas) { canvas.enabled = true; }
    }

    public void DisableCanvas()
    {
        if (canvas) { canvas.enabled = false; }
    }
}
