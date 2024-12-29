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
    public bool isGameActive = true;
    public GameObject titleScreen;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadSettings();
    }

    public void GameOver()
    {
        isGameActive = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void StartGame()
    {
        isGameActive = true;
        SceneManager.LoadScene("Main");
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
        if (Input.GetKeyDown(KeyCode.Escape) && !settingsMenu.activeSelf && SceneManager.GetActiveScene().name == "Main" && !pauseMenu.activeSelf)
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
            SaveSettings();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Main")
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
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            sound.value = PlayerPrefs.GetFloat("Sound");
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
