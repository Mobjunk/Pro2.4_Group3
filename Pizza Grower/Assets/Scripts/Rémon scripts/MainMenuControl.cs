using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    public GameObject instructionsCanvas;
    public GameObject highscoreCanvas;
    public GameObject mainMenuCanas;
    public GameObject custimizationCanas;
    public GameObject defaultGamemode;
    public GameObject gameOverCanvas;

    [Header("Sounds")]
    public AudioSource buttonClick;
    public AudioSource quit;

    [Header("Name Components")]
    public InputField nameField;    // Start is called before the first frame update
    void Start()
    {
        nameField.text = PlayerPrefs.GetString("Nickname", nameField.text);

        mainMenuCanas.SetActive(true);
        instructionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(false);
        defaultGamemode.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    private void Update()
    {
        if (nameField.text == "")
        {
            //....
        }
        if (nameField.text != "")
        {
            //....
        }
    }

    public void MainMenu()
    {
        mainMenuCanas.SetActive(true);
        instructionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(false);
        defaultGamemode.SetActive(false);
        gameOverCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        buttonClick.Play();
    }

    public void Play()
    {
        mainMenuCanas.SetActive(false);
        instructionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(false);
        defaultGamemode.SetActive(true);
        gameOverCanvas.SetActive(false);
        buttonClick.Play();
    }

    public void Instructions()
    {
        mainMenuCanas.SetActive(false);
        instructionsCanvas.SetActive(true);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(false);
        defaultGamemode.SetActive(false);
        gameOverCanvas.SetActive(false);
        buttonClick.Play();
    }

    public void Highscore()
    {
        buttonClick.Play();
        mainMenuCanas.SetActive(false);
        instructionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(true);
        custimizationCanas.SetActive(false);
        defaultGamemode.SetActive(false);
        gameOverCanvas.SetActive(false);

    }

    public void Custimization()
    {
        mainMenuCanas.SetActive(false);
        instructionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(true);
        defaultGamemode.SetActive(false);
        gameOverCanvas.SetActive(false);
        buttonClick.Play();
    }

    public void Back()
    {
        mainMenuCanas.SetActive(true);
        instructionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(false);
        defaultGamemode.SetActive(false);
        gameOverCanvas.SetActive(false);
        buttonClick.Play();
    }


    public void Quit() 
    {
        Application.Quit();
        quit.Play();
    }

    public void NameSave()
    {
        if (nameField.text != "")
        {
            PlayerPrefs.SetString("Nickname", nameField.text);
        }
    }

}

