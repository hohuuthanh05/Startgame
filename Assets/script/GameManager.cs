using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUI;
    private bool isGameOver = false;

    void Start()
    {
        UpdateScore();
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        
    }

    public void AddScore(int points)
    {
        if (isGameOver) 
        {
        score += points;
        UpdateScore();
        }
    }

    private void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        else
        {
            Debug.LogError("scoreText is not assigned in GameManager!");
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        score = 0;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {   
        isGameOver = false;
        score = 0;
        UpdateScore();
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene("Game");
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
}
