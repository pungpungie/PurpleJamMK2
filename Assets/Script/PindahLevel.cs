using UnityEngine;
using UnityEngine.SceneManagement; // Ini wajib buat urusan pindah level

public class PindahLevel : MonoBehaviour
{
    // Fungsi ini jalan kalau Player masuk ke area Finish
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek apakah yang nabrak itu Player
        if (collision.CompareTag("Player"))
        {
            // Pindah ke level selanjutnya berdasarkan urutan Build Settings
            int levelSekarang = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(levelSekarang + 1);

            // Catatan: Pastikan Time.timeScale kembali ke 1 kalau sebelumnya di-pause
            Time.timeScale = 1f;
        }
    }
}