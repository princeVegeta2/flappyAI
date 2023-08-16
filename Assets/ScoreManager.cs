using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public BirdController bird;
    public Text scoreText;

    private void Start()
    {
        bird.OnScored += UpdateScoreDisplay; // Subscribe to the scoring event
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + bird.Score.ToString();
    }
}

