using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health & Score")]
    public int health = 3;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    [Header("UI Elements")]
    public Image[] hearts; // Tarik Heart1, Heart2, Heart3 ke sini
    public GameObject winPanel;
    public GameObject gameOverPanel;

    [Header("Checkpoint")]
    private Vector2 lastCheckpointPos;

    private Rigidbody2D rb;
    private bool canTakeDamage = true;
    public int totalBungaDiLevelIni = 6;
    private int bungaDiambil = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastCheckpointPos = transform.position;

        Time.timeScale = 1f;
        if (winPanel != null) winPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. BUNGA (PASTIKAN TAG-NYA: poin)
        if (other.CompareTag("poin"))
        {
            score += 10;
            bungaDiambil++;
            Destroy(other.gameObject);
            UpdateUI();

            // CEK MENANG
            if (bungaDiambil >= totalBungaDiLevelIni)
            {
                WinGame();
            }
        }

        // 2. CHECKPOINT
        if (other.CompareTag("Checkpoint"))
        {
            lastCheckpointPos = other.transform.position;
        }

        // 3. DEATHZONE (Jatuh)
        if (other.CompareTag("Deathzone"))
        {
            TakeDamage(1);
        }

        if (!canTakeDamage) return;

        // 4. TOXIC FROG
        if (other.CompareTag("ToxicFrog"))
        {
            StartCoroutine(FrogDeathSequence(other.gameObject));
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        UpdateUI();

        if (health <= 0)
        {
            ShowGameOver();
        }
        else
        {
            RespawnAtCheckpoint();
        }
    }

    void RespawnAtCheckpoint()
    {
        transform.position = lastCheckpointPos;
        rb.linearVelocity = Vector2.zero;
    }

    IEnumerator FrogDeathSequence(GameObject frog)
    {
        canTakeDamage = false;
        rb.linearVelocity = Vector2.zero;

        Animator frogAnim = frog.GetComponent<Animator>();
        if (frogAnim != null) frogAnim.SetTrigger("detonate");

        yield return new WaitForSeconds(0.5f);
        health = 0;
        UpdateUI();
        Destroy(frog);
        ShowGameOver();
    }

    void ShowGameOver()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void WinGame()
    {
        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UpdateUI()
    {
        // FORMAT KEMARIN: Hati langsung mati/hilang dari layar
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (scoreText != null) scoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Fungsi untuk tombol Next di Win Panel
    public void NextLevel()
    {
        Time.timeScale = 1f;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Cek apakah masih ada level selanjutnya
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Langsung pindah tanpa simpan progress kunci-kuncian
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Kalau sudah level 5 (level terakhir), balik ke menu
            SceneManager.LoadScene("SampleScene");
        }
    }
}