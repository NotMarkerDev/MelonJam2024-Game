using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider sound;
    public Slider mouseXSens;
    public Slider mouseYSens;
    private Cam camera;
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public bool isGameActive = true;
    public GameObject titleScreen;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
        //titleScreen.gameObject.SetActive(false);
        SceneManager.LoadScene("Main");
        camera = GameObject.Find("Player").GetComponent<Cam>();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        camera.xSens *= mouseXSens.value;
        camera.ySens *= mouseYSens.value;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        camera.xSens *= mouseXSens.value;
        camera.ySens *= mouseYSens.value;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        camera.xSens *= mouseXSens.value;
        camera.ySens *= mouseYSens.value;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape") && !settingsMenu.activeSelf && SceneManager.GetActiveScene().name == "Main" && !pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
            //display pause UI
        }
        else if(Input.GetKeyDown("escape") && settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
        }
        else if(Input.GetKeyDown("escape") && SceneManager.GetActiveScene().name == "Main")
        {
            pauseMenu.SetActive(true);
        }
    }
}
