using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    void Start()
    {
        UpdateScore();
    }

    void Update()
    {
        
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore();
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

}
