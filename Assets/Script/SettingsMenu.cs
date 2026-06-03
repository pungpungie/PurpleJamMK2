using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider musicSlider;
    public AudioSource bgmSource;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        musicSlider.value = volume;
        bgmSource.volume = volume;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetMusicVolume()
    {
        bgmSource.volume = musicSlider.value;

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.Save();
    }
}