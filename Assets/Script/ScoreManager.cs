using UnityEngine;
using TMPro; // Pastikan menggunakan TMPro jika teksnya adalah TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText; // Taruh objek Text (TMP) dari dalam Button ke sini
    private int score = 0;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddPoint(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}