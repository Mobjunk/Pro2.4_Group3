using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject highscoreCanvas;
    public GameObject mainMenuCanas;
    public GameObject custimizationCanas;

    [Header("Name Set Components")]
    public GameObject nameSetCanvas;
    public InputField nameField;


    private void Update()
    {
        Debug.Log(PlayerPrefs.GetString("Nickname"));
    }

    // Start is called before the first frame update
    void Start()
    {
        mainMenuCanas.SetActive(true);
        optionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(false);
    }

    public void Play()
    {
        mainMenuCanas.SetActive(false);
        optionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(false);
        nameSetCanvas.SetActive(true);
        
    }

    public void Options()
    {
        mainMenuCanas.SetActive(false);
        optionsCanvas.SetActive(true);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(false);
    }

    public void Highscore()
    {
        mainMenuCanas.SetActive(false);
        optionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(true);
        custimizationCanas.SetActive(false);
    }

    public void Custimization()
    {
        mainMenuCanas.SetActive(false);
        optionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(true);
    }

    public void Back()
    {
        mainMenuCanas.SetActive(true);
        optionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(false);
    }


    public void Quit() 
    {
        Application.Quit();
    }

    public void NameSave()
    {
        if (nameField.text != "")
        {
            PlayerPrefs.SetString("Nickname", nameField.text);
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

}

