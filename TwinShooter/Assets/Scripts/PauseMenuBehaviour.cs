using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MainMenuBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    public void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        UpdateQualityLabel();
        UpdateVolumeLabel();
    }

    public void Update()
    {
       if(Input.GetKeyUp("escape"))
        {
            //if false becomes true and vice-versa
            isPaused = !isPaused;
            //if is Paused is true 0 otherwise 1
            Time.timeScale = (isPaused) ? 0 : 1;
            pauseMenu.SetActive(isPaused);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
    }

    public override void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        ResumeGame();
    }

    public void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel();
        UpdateQualityLabel();
    }

    public void DecreaseQuality()
    {
        QualitySettings.DecreaseLevel();
        UpdateQualityLabel();
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        UpdateVolumeLabel();
    }

    private void UpdateQualityLabel()
    {
        int currentQuality = QualitySettings.GetQualityLevel();
        string qualityName = QualitySettings.names[currentQuality];

        optionsMenu.transform.Find("Quality Level").GetComponent<UnityEngine.UI.Text>().text = "Quality Level - " + qualityName;
    }

    public void UpdateVolumeLabel()
    {
        optionsMenu.transform.Find("Master Volume").GetComponent<UnityEngine.UI.Text>().text = "Master Volume - " + (AudioListener.volume * 100).ToString("f2") + "%";
    }

}
