using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Finish!");

            Time.timeScale = 1f; // jaga-jaga kalau sebelumnya pause
            SceneManager.LoadScene(nextSceneName);
        }
    }
}