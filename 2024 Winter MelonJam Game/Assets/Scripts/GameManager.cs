using System.Collections;
using UnityEngine;
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

    private bool isDead = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadSettings();
    }

    public void GameOver()
    {
        isDead = true;
        if (playerCamera != null)
        {
            playerCamera.enabled = false;
        }
        deathMenu.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        isDead = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
        DisableAllMenus();
    }

    public void StartGame()
    {
        isDead = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
        DisableAllMenus();
        StartCoroutine(AssignCamera());

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        isDead = false;

        SceneManager.LoadScene("Menu");
        LoadSettings();
        UpdateCameraSensitivities();

        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        if (playerCamera != null)
        {
            playerCamera.enabled = true;
        }
        UpdateCameraSensitivities();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

        SaveSettings();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
    }

    private void UpdateCameraSensitivities()
    {
        if (playerCamera != null)
        {
            playerCamera.xSens = mouseXSens.value * 100f;
            playerCamera.ySens = mouseYSens.value * 100f;
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
        if (!isDead && Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Game")
        {
            if (!pauseMenu.activeSelf)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                if (playerCamera != null)
                {
                    playerCamera.enabled = false;
                }

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Resume();
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

    private void DisableAllMenus()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        titleScreen.SetActive(false);
        deathMenu.SetActive(false);
    }
}
