using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject uiScreen;
    public TMP_Text finalScoreText;

    private bool isGamePaused = false;
    private bool hasHandledGameOver = false;
    private PlayerController player;
    private UI uiScript;
    #endregion

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        uiScript = FindObjectOfType<UI>();
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    void Update()
    {
        // Pause/resume toggle
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        // Handle game over on player death
        if (!hasHandledGameOver && player != null && player.isDead)
        {
            hasHandledGameOver = true;
            StartCoroutine(SlowDownTime(2f));
        }
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0f : 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (pauseScreen != null)
            pauseScreen.SetActive(isGamePaused);
    }

    IEnumerator SlowDownTime(float duration)
    {
        float t = 0f;
        float startScale = Time.timeScale;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / duration;
            Time.timeScale = Mathf.Lerp(startScale, 0f, t);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            yield return null;
        }

        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        EndGame();
    }

    void EndGame()
    {
        uiScreen.SetActive(false);
        gameOverScreen.SetActive(true);

        int currentScore = PlayerController.Instance.currentScore;

        // Load existing high score
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Update high score if needed
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save(); // Save changes
            highScore = currentScore;
        }

        // Display current and high score
        if (finalScoreText != null)
        {
            finalScoreText.text = $"Score: {currentScore}\nHigh Score: {highScore}";
        }
    }

    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}