using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{

    [Header("Checking username availablity")]
    private string _websiteResponse = "";
    private bool _finishedResponse = false;
    [SerializeField] Text coinText;
    [SerializeField] Text _feedbackText;
    private float _feedbackTimer;

    [Header("All game objects")]
    public GameObject instructionsCanvas;
    public GameObject highscoreCanvas;
    public GameObject mainMenuCanas;
    public GameObject custimizationCanas;
    public GameObject defaultGamemode;
    public GameObject flyCatchGamemode;
    public GameObject timeGamemode;
    public GameObject gameOverCanvas;
    public GameObject selectGamemodes;

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
        selectGamemodes.SetActive(false);
        flyCatchGamemode.SetActive(false);
        timeGamemode.SetActive(false);
    }

    private void Update()
    {
        coinText.text = $" x {PlayerPrefs.GetInt("Coins")}";
        if (_finishedResponse)
        {
            Debug.Log($"response: {_websiteResponse}");
            if (_websiteResponse.Equals("This username is currently free."))
            {
                SetFeedback("Your nickname has been changed succesfully!");
                PlayerPrefs.SetString("Nickname", nameField.text);
            }
            else 
            {
                SetFeedback("This nickname is already in use!");
                nameField.text = "";
            }
            _finishedResponse = false;
        }

        if(_feedbackTimer > 0)
        {
            _feedbackTimer -= Time.deltaTime;
            if(_feedbackTimer <= 0)
            {
                _feedbackText.text = "";
                _feedbackTimer = 0;
            }
        }
    }

    public void MainMenu()
    {
        mainMenuCanas.SetActive(true);
        instructionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(false);
        defaultGamemode.SetActive(false);
        flyCatchGamemode.SetActive(false);
        timeGamemode.SetActive(false);
        gameOverCanvas.SetActive(false);
        buttonClick.Play();
    }

    public void Play()
    {
        if (nameField.text.Equals(""))
        {
            //TODO: Make something show up telling the player he needs to fill in a name
            SetFeedback("Please set a nickname before you start playing!");
            return;
        }
        mainMenuCanas.SetActive(false);
        instructionsCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        custimizationCanas.SetActive(false);
        defaultGamemode.SetActive(false);
        gameOverCanvas.SetActive(false);
        selectGamemodes.SetActive(true);
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

    public void DefaultGamemode()
    {
        selectGamemodes.SetActive(false);
        defaultGamemode.SetActive(true);
        buttonClick.Play();
        GameHandler gameHandler = defaultGamemode.GetComponent<Regular>();
        if (gameHandler != null) gameHandler.Reset(gameHandler._hasBeenLoaded);
    }

    public void FlyCatchGamemode()
    {
        selectGamemodes.SetActive(false);
        flyCatchGamemode.SetActive(true);
        GameHandler gameHandler = flyCatchGamemode.GetComponent<Flies>();
        if (gameHandler != null) gameHandler.Reset(gameHandler._hasBeenLoaded);
    }

    public void TimeGamemode()
    {
        selectGamemodes.SetActive(false);
        timeGamemode.SetActive(true);
        GameHandler gameHandler = timeGamemode.GetComponent<TimerChanger>();
        if (gameHandler != null) gameHandler.Reset(gameHandler._hasBeenLoaded);
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
            StartCoroutine(CheckAvailablity(nameField.text));
            //Debug.Log("Name has been saved test");
            //PlayerPrefs.SetString("Nickname", nameField.text);
        }
    }

    private IEnumerator CheckAvailablity(string name)
    {
        //Creates a new form
        WWWForm form = new WWWForm();

        //Fills the form with the required data
        form.AddField("name", name);

        //Handles sending the web quest
        using (UnityWebRequest www = UnityWebRequest.Post("https://mobstar-sof.com/school/existing_user.php", form))
        {

            //Sends the web request
            yield return www.SendWebRequest();

            //Checks if there is no error
            if (!www.isNetworkError && !www.isHttpError)
            {
                _websiteResponse = www.downloadHandler.text;
                _finishedResponse = true;
            }
        }
    }

    private void SetFeedback(string message)
    {
        _feedbackText.text = message;
        _feedbackTimer = 4;
    }
}

