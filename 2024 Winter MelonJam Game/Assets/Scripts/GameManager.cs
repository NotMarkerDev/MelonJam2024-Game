using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider sound;
    public Slider mouseXSens;
    public Slider mouseYSens;
    private Cam playerCamera;
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject titleScreen;
    public GameObject deathMenu;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadSettings();
    }

    public void GameOver()
    {
        deathMenu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        titleScreen.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        titleScreen.SetActive(false);
        StartCoroutine(AssignCamera());
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        SaveSettings();
        UpdateCameraSensitivities();
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        UpdateCameraSensitivities();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        UpdateCameraSensitivities();
        LoadSettings();
    }

    public void Back()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void UpdateCameraSensitivities()
    {
        if (playerCamera != null)
        {
            playerCamera.xSens = mouseXSens.value * 100f;
            playerCamera.ySens = mouseYSens.value * 100f;
        }
        else
        {
            Debug.LogWarning("Camera not found!");
        }
    }

    private IEnumerator AssignCamera()
    {
        yield return new WaitForSeconds(0.1f);
        playerCamera = GameObject.Find("Player")?.GetComponent<Cam>();
        UpdateCameraSensitivities();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !settingsMenu.activeSelf && SceneManager.GetActiveScene().name == "Game" && !pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
            SaveSettings();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Game")
        {
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Sound", sound.value);
        PlayerPrefs.SetFloat("MouseXSens", mouseXSens.value);
        PlayerPrefs.SetFloat("MouseYSens", mouseYSens.value);
        PlayerPrefs.Save();

        AudioListener.volume = sound.value / 5;
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            sound.value = PlayerPrefs.GetFloat("Sound");
            AudioListener.volume = sound.value / 5;
        }
        if (PlayerPrefs.HasKey("MouseXSens"))
        {
            mouseXSens.value = PlayerPrefs.GetFloat("MouseXSens");
        }
        if (PlayerPrefs.HasKey("MouseYSens"))
        {
            mouseYSens.value = PlayerPrefs.GetFloat("MouseYSens");
        }
    }
}
