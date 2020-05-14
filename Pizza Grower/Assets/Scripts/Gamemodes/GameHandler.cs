using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System;

public abstract class GameHandler : MonoBehaviour
{
    [HideInInspector] public bool _hasBeenLoaded = false;
    /// <summary>
    /// if you make the pizza to small or to big that the gameover canvas will show
    /// </summary>
    [SerializeField] private GameObject GameOverCanvas;
    /// <summary>
    /// the sound if a pizza is made correctly
    /// </summary>
    [SerializeField] private AudioSource goodPizza;
    /// <summary>
    /// the sound if a pizza is made incorrectly
    /// </summary>
    [SerializeField] private AudioSource badPizza;
    /// <summary>
    /// The sprite that will be shown if the pizza made correctly
    /// </summary>
    [SerializeField] private GameObject goodSprite;    
    /// <summary>
    /// The sprite that will be shown if the pizza is too big or too small
    /// </summary>
    [SerializeField] private GameObject faultSprite;
    /// <summary>
    /// The sprite that will be shown if the pizza is perfect size
    /// </summary>
    [SerializeField] private GameObject perfectSprite;
    /// <summary>
    /// A check to see if the pizza box is moving left
    /// </summary>
    protected bool _movingLeft;
    /// <summary>
    /// Checks if a game is currently in progress
    /// </summary>
    protected bool _gameInProgress;
    /// <summary>
    /// The min and max size of the pizza
    /// </summary>
    protected Vector3 _minSize, _maxSize;
    /// <summary>
    /// The pizza itself
    /// </summary>
    [SerializeField] private GameObject _pizza;
    /// <summary>
    /// The pizza box
    /// </summary>
    [SerializeField] private GameObject _pizzaBox;
    /// <summary>
    /// The hole in the pizza box
    /// </summary>
    [SerializeField] private GameObject _boxHoleMin, _boxHoleMax;
    /// <summary>
    /// The reference to the resize script
    /// </summary>
    protected Resize _resize;
    /// <summary>
    /// Checks if there is a pizza being grown
    /// </summary>
    protected bool _pizzaInProgress = false;
    /// <summary>
    /// The name of the player
    /// </summary>
    protected string _playerName;
    /// <summary>
    /// The score the player has gained
    /// </summary>
    protected int _score;
    /// <summary>
    /// The text element for score
    /// </summary>
    [SerializeField] Text _scoreText;
    /// <summary>
    /// How much time passed in the game mode
    /// </summary>
    protected float _timer;
    /// <summary>
    /// How much time in seconds has passed
    /// </summary>
    protected int _timePassed;
    /// <summary>
    /// The amount of 'Perfect' pizzas the player has made
    /// </summary>
    protected int _perfectPizzas;
    /// <summary>
    /// The amount of 'Nice' pizza's the player has made
    /// So between the min and max size
    /// </summary>
    protected int _regularPizzas;
    /// <summary>
    /// The amount of 'Bad' pizza's the player has made
    /// This is only used in the time based game mode
    /// </summary>
    protected int _badPizzas;
    /// <summary>
    /// The name of the gamemode
    /// </summary>
    /// <returns></returns>
    public abstract string GameModeName();
    /// <summary>
    /// If the game mode is time based or not
    /// </summary>
    /// <returns></returns>
    public abstract bool TimeBased();
    /// <summary>
    /// How many seconds the time baseed 
    /// </summary>
    /// <returns></returns>
    public abstract int StartTime();
    /// <summary>
    /// How much time is left in the time based game mode
    /// </summary>
    [HideInInspector] public float _timeLeft;

    public virtual void Start()
    {
        Reset();
        _hasBeenLoaded = true;
    }

    public virtual void Update()
    {
        if (!_gameInProgress) return;

        //Update the time passed in the gamemode
        _timer += Time.deltaTime;
        _timePassed = (int)_timer % 60;
        _scoreText.text = "Score: " + _score;

        //Checks if its a time based game mode
        if(TimeBased())
        {
            _timeLeft -= Time.deltaTime;
            if(_timeLeft <= 0)
            {
                _gameInProgress = false;
                HandleGameOver();
                badPizza.Play();
                _pizza.SetActive(false);
                _pizzaBox.SetActive(false);
                Debug.Log("Handle ending the game!");
                return;
            }
        }

        //Checks if the player released the screen/mouse and if there is currently a pizza in progress
        if (!_resize.isClicking && _pizzaInProgress)
        {
            //Checks if the pizza doesnt meet any requirments
            if (DoesNotMeetRequirments()) HandlePizzaCompletion(false);
            //Checks if the pizza meets the 'Nice' requirments
            else if (MeetsRequirments()) HandlePizzaCompletion(true);
            //Checks if hte pizza meets the 'Perfect' requirments
            else if (PerfectPizza()) HandlePizzaCompletion(true, true);
            _pizzaInProgress = false;
        }
        else if (_resize.isClicking)
        {
            //Make sure the game ends when the pizza reaches max
            if(_pizza.transform.localScale.magnitude > _maxSize.magnitude + 0.05) HandlePizzaCompletion(false);
            _pizzaInProgress = true;
        }

        //Checks if the input of the player has been blocked and if there is no pizza in progress
        if (_resize.blockInput && !_pizzaInProgress && _movingLeft)
        {
            //Makes the pizza box move left
            _pizzaBox.transform.position = new Vector3(_pizzaBox.transform.position.x - 15, _pizzaBox.transform.position.y, _pizzaBox.transform.position.z);
            //Checks if the pizzaz boz has left the screen
            if (_pizzaBox.transform.position.x <= -300)
            {
                //Hide both sprites
                goodSprite.SetActive(false);
                faultSprite.SetActive(false);
                perfectSprite.SetActive(false);
                //Hide the max size image
                _boxHoleMax.SetActive(false);
                //Sets the pizza box back in the middle
                _pizzaBox.transform.localPosition = Vector3.zero;
                //Reset the pizza's scale
                _pizza.transform.localScale = Vector3.zero;
                //Makes the pizza no longer move to the left
                _movingLeft = false;
                //Reset the resizing (Input & size)
                _resize.Reset();
            }
        }
    }

    /// <summary>
    /// Checks if the pizza does not meet the required size
    /// </summary>
    /// <returns></returns>
    protected bool DoesNotMeetRequirments()
    {
        return _pizza.transform.localScale.magnitude < _minSize.magnitude || _pizza.transform.localScale.magnitude > _maxSize.magnitude + 0.05;
    }
    /// <summary>
    /// Checks if the pizza meets the required size
    /// </summary>
    /// <returns></returns>
    protected bool MeetsRequirments()
    {
        return _pizza.transform.localScale.magnitude >= _minSize.magnitude && _pizza.transform.localScale.magnitude <= _maxSize.magnitude - 0.05;
    }
    /// <summary>
    /// Checks if the pizza has the perfect size
    /// </summary>
    /// <returns></returns>
    protected bool PerfectPizza()
    {
        return _pizza.transform.localScale.magnitude >= _maxSize.magnitude - 0.05 && _pizza.transform.localScale.magnitude <= _maxSize.magnitude + 0.05;
    }

    protected virtual void HandlePizzaCompletion(bool correct, bool perfect = false)
    {
        if (!correct)
        {
            //Disable the good sprite
            goodSprite.SetActive(false);
            //Enable the fault sprite
            faultSprite.SetActive(true);
            //Disable perfect sprite
            perfectSprite.SetActive(false);
            //Checks if the game isnt time based
            if (!TimeBased())
            {
                //Makes sure the game isnt in progress so no game logic can be ran
                _gameInProgress = false;
                //Handles the game over stuff
                HandleGameOver();
                //Play the bad pizza sound
                badPizza.Play();
                //Disable all needed sprites so it doesnt show in the game over screen
                faultSprite.SetActive(false);
                _pizza.SetActive(false);
                _pizzaBox.SetActive(false);
                //Reset text
                _scoreText.text = "";
                //holds the arary of all flies
                GameObject[] flies = GameObject.FindGameObjectsWithTag("Fly");
                //Checks if the flies isnt null
                if (flies != null)
                {
                    //Loops though all the flies and removes them
                    foreach (GameObject fly in flies)
                    {
                        Destroy(fly.gameObject);
                    }
                }
                return;
            }
            else
            {
                HandleWrongPizza();
                _resize.GrowSpeed();
                //Play the bad pizza sound
                badPizza.Play();
                _badPizzas++;
            }
           
        }
        else
        {
            //Play the good pizza sound
            goodPizza.Play();
            //show the good pizza icon
            goodSprite.SetActive(true);
            //Disable the fault and perfect sprite
            faultSprite.SetActive(false);
            perfectSprite.SetActive(false);
            _resize.GrowSpeed();
            //Increase the pizza counters based on perfection
            if (perfect)
            {
                HandlePerfectPizza();
                _perfectPizzas++;
                perfectSprite.SetActive(true);
                goodSprite.SetActive(false);
                faultSprite.SetActive(false);
            }
            else
            {
                HandleNicePizza();
                _regularPizzas++;
            }
        }
        //Blocks the clicking input
        _resize.blockInput = true;
        //Starts moving the pizza box to the left
        _movingLeft = true;  
    }

    public abstract void HandleNicePizza();
    public abstract void HandlePerfectPizza();
    public abstract void HandleWrongPizza();
    public virtual void HandleGameOver()
    {
        int coinsGained = _score / 2;
        int currentCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", currentCoins + coinsGained);

        //Display game over canvas
        GameOverCanvas.SetActive(true);

        Text gameOverText = GameOverCanvas.GetComponentInChildren<Text>();
        gameOverText.text = $"Game Over\n\n{(TimeBased() ? "Bad Pizza's: "+_badPizzas+"\n" : "")}Nice Pizza's: {_regularPizzas}\nPerfect Pizza's: {_perfectPizzas}\n\nScore: {_score}\nCoins Gained: {coinsGained}\nCurrent Coins: {PlayerPrefs.GetInt("Coins")}";

        _resize.blockInput = true;
        //if(_score > 0 && !_playerName.Equals(""))
        //Highscore.instance.Insert(new HighscoreEntry(_playerName, _score, _timePassed, GameModeName()));
    }

    public void Reset(bool realReset = false)
    {
        //Debug.Log($"GameHandler {(realReset ? "reset" : "startup")}...");
        //Debug.Log($"Player has {PlayerPrefs.GetInt("Coins")} coins.");
        _timeLeft = StartTime();
        //Show the pizza
        _pizza.SetActive(true);
        //Make sure its a real reset else it will break the game
        if (realReset) _pizza.transform.localScale = Vector3.zero;
        //Show pizza box and the hole
        _pizzaBox.SetActive(true);
        _boxHoleMax.SetActive(true);
        //Reset the variables to 0
        _score = 0;
        _perfectPizzas = 0;
        _regularPizzas = 0;
        _badPizzas = 0;
        _pizzaInProgress = false;
        //Handles deactivating the required game objects
        perfectSprite.SetActive(false);
        goodSprite.SetActive(false);
        faultSprite.SetActive(false);
        GameOverCanvas.SetActive(false);
        //Handles resize code
        _resize = _pizza.GetComponent<Resize>();
        _resize.Reset();
        if (realReset) _resize.currentGrow = _resize.minGrow;
        //Sets the min and max size the pizza can be
        _minSize = _boxHoleMin.transform.localScale;
        _maxSize = new Vector3(_boxHoleMax.transform.localScale.x - 0.125f, _boxHoleMax.transform.localScale.y - 0.125f, _boxHoleMax.transform.localScale.z);
        //Sets the player name
        _playerName = PlayerPrefs.GetString("Nickname");
        //Makes it so the logic of the game is executed
        _gameInProgress = true;
    }
}
