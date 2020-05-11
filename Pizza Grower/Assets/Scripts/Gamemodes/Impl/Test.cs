using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : GameHandler
{
    public override string GameModeName()
    {
        return "Test";
    }

    public override bool TimeBased()
    {
        return true;
    }

    public override int StartTime()
    {
        return 60;
    }

    public override void HandleNicePizza()
    {
        _timeLeft += 5;
    }

    public override void HandlePerfectPizza()
    {
        _timeLeft += 10;
    }

    public override void HandleGameOver()
    {

        base.HandleGameOver();
    }
}
