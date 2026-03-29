using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UI : MonoBehaviour
{
    #region Variables
    public TMP_Text healthText;
    public TMP_Text energyText;
    public TMP_Text timerText;
    public TMP_Text scoreText;

    private PlayerController playerController;
    private float gameTime = 0f;
    private int score;
    #endregion

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        UpdateHealthText();
        UpdateEnergyText();
        UpdateTimerText();
        scoreText.text = "" + PlayerController.Instance.currentScore.ToString();
    }

    void UpdateHealthText()
    {
        float healthPercent = playerController.GetHealthPercent();
        healthText.text = $"{Mathf.FloorToInt(healthPercent * 100)}";
    }

    void UpdateEnergyText()
    {
        float energyPercent = playerController.GetEnergyPercent();
        energyText.text = $"{Mathf.FloorToInt(energyPercent * 100)}";
    }

    void UpdateTimerText()
    {
        gameTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public int GetScore()
    {
        return PlayerController.Instance.currentScore;
    }
}