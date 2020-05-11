using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class GameHandler : MonoBehaviour
{
    /// <summary>
    /// The sprite that will be shown if the pizza is too big
    /// </summary>
    [SerializeField] private GameObject perfectSprite;    
    /// <summary>
    /// The sprite that will be shown if the pizza is too big
    /// </summary>
    [SerializeField] private GameObject faultSprite;
    /// <summary>
    /// A check to see if the pizza box is moving left
    /// </summary>
    private bool _movingLeft;
    /// <summary>
    /// Checks if a game is currently in progress
    /// </summary>
    private bool _gameInProgress;
    /// <summary>
    /// The min and max size of the pizza
    /// </summary>
    [SerializeField] private Vector3 _minSize, _maxSize;
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
    private Resize _resize;
    /// <summary>
    /// Checks if there is a pizza being grown
    /// </summary>
    [SerializeField] private bool _pizzaInProgress = false;
    /// <summary>
    /// The name of the player
    /// </summary>
    [SerializeField] protected string _playerName;
    /// <summary>
    /// The score the player has gained
    /// </summary>
    [SerializeField] protected int _score;
    /// <summary>
    /// How much time passed in the game mode
    /// </summary>
    private float _timer;
    /// <summary>
    /// How much time in seconds has passed
    /// </summary>
    [SerializeField] protected int _timePassed;
    /// <summary>
    /// The amount of 'Perfect' pizzas the player has made
    /// </summary>
    [SerializeField] protected int _perfectPizzas;
    /// <summary>
    /// The amount of 'Nice' pizza's the player has made
    /// So between the min and max size
    /// </summary>
    [SerializeField] protected int _regularPizzas;
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
    [SerializeField] public float _timeLeft;
    
    public virtual void Start()
    {
        _timeLeft = StartTime();
        perfectSprite.SetActive(false);
        faultSprite.SetActive(false);
        _resize = _pizza.GetComponent<Resize>();
        _minSize = _boxHoleMin.transform.localScale;
        _maxSize = new Vector3(_boxHoleMax.transform.localScale.x - 0.125f, _boxHoleMax.transform.localScale.y - 0.125f, _boxHoleMax.transform.localScale.z);
        _playerName = PlayerPrefs.GetString("Nickname");
        _gameInProgress = true;
    }

    public virtual void Update()
    {
        if (!_gameInProgress) return;

        //Update the time passed in the gamemode
        _timer += Time.deltaTime;
        _timePassed = (int)_timer % 60;

        //Checks if its a time based game mode
        if(TimeBased())
        {
            _timeLeft -= Time.deltaTime;
            if(_timeLeft < 1)
            {
                Debug.Log("Ran out of time, show end screen!");
                return;
            }
        }
        
        //Checks if the player released the screen/mouse and if there is currently a pizza in progress
        if (!_resize.isClicking && _pizzaInProgress)
        {
            //Debug.Log($"pizza: {_pizza.transform.localScale.magnitude}, minSize: {_minSize.magnitude}, maxSize: {_maxSize.magnitude}");
            //Debug.Log("Pizza was being grown but the click has been released!");
            if (DoesNotMeetRequirments())
            {
                //Debug.Log("Pizza is too small so game over!");
                HandlePizzaCompletion(false);
            }
            else if (MeetsRequirments())
            {
                //Debug.Log("Pizza is not perfect but meets the requirements!");
                HandlePizzaCompletion(true);
            }
            else if (PerfectPizza())
            {
                //Debug.Log("Pizza is the perfect size!");
                HandlePizzaCompletion(true, true);
            }
            _pizzaInProgress = false;
        }
        else  if(_resize.isClicking) _pizzaInProgress = true;

        //Checks if the input of the player has been blocked and if there is no pizza in progress
        if (_resize.blockInput && !_pizzaInProgress && _movingLeft)
        {
            //Makes the pizza box move left
            _pizzaBox.transform.position = new Vector3(_pizzaBox.transform.position.x - 15, _pizzaBox.transform.position.y, _pizzaBox.transform.position.z);
            //Checks if the pizzaz boz has left the screen
            if (_pizzaBox.transform.position.x <= -300)
            {
                //Hide both sprites
                perfectSprite.SetActive(false);
                faultSprite.SetActive(false);
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
            perfectSprite.SetActive(false);
            faultSprite.SetActive(true);

            _gameInProgress = false;
            HandleGameOver();
            Debug.Log("Handle ending the game!");
            return;
        }
        else
        {
            perfectSprite.SetActive(true);
            faultSprite.SetActive(false);
        }
        //Blocks the clicking input
        _resize.blockInput = true;
        //Starts moving the pizza box to the left
        _movingLeft = true;
        //Increase the pizza counters based on perfection
        if (perfect)
        {
            HandlePerfectPizza();
            _perfectPizzas++;
        }
        else
        {
            HandleNicePizza();
            _regularPizzas++;
        }
    }

    public abstract void HandleNicePizza();
    public abstract void HandlePerfectPizza();
    public virtual void HandleGameOver()
    {
        _resize.blockInput = true;
        Highscore.instance.Insert(new HighscoreEntry(_playerName, _score, _timePassed, GameModeName()));
    }
}
