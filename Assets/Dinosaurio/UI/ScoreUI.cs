using UnityEngine;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    bool enableOnStart;
    [SerializeField]
    bool disableOnEnd;

    TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        text.enabled = false;
    }

    private void OnEnable()
    {
        ScoreManager.OnScoreUpdate += Int2Text;
        if (enableOnStart)
        {
            GameManager.OnStart += EnableText;
        }
        else
        {
            GameManager.OnStart += DisableText;
        }
        if(disableOnEnd)
        {
            GameManager.OnEnd += DisableText;
        }
        else
        {
            GameManager.OnEnd += EnableText;
        }
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreUpdate -= Int2Text;
        if (enableOnStart)
        {
            GameManager.OnStart -= EnableText;
        }
        else
        {
            GameManager.OnStart -= DisableText;
        }
        if (disableOnEnd)
        {
            GameManager.OnEnd -= DisableText;
        }
        else
        {
            GameManager.OnEnd -= EnableText;
        }
    }

    public void Int2Text(int Int)
    {
        text.text = Int.ToString();
    }

    public void EnableText()
    {
        if (text) { text.enabled = true; }
    }

    public void DisableText()
    {
        if (text) { text.enabled = false; }
    }
}
