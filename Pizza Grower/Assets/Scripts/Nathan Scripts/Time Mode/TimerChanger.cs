using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerChanger : GameHandler
{
    public Text gameTimer;

    public override void Start()
    {      
        base.Start();
        StartTime();
    }
    public override bool TimeBased()
    {       
        return true;
    }
    public override string GameModeName()
    {
        return "Time Limit Mode";
    }
    public override int StartTime()
    {
        return 60;
    }
    public override void HandleNicePizza()
    {
        _timeLeft += 1f;
    }
    public override void HandlePerfectPizza()
    {
        _timeLeft += 5f;
    }
    public override void HandleWrongPizza()
    {
        _timeLeft -= 10f;
    }
    public override void HandleGameOver()
    {
        base.HandleGameOver();
    }



    // Start is called before the first frame update



    // Update is called once per frame
    public override void Update()
    {        
        gameTimer.text = _timeLeft.ToString("F0");
        base.Update();
    }
}
