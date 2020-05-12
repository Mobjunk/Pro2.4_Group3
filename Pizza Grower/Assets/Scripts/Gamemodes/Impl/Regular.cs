using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular : GameHandler
{
    public override string GameModeName()
    {
        return "Default";
    }

    public override bool TimeBased()
    {
        return false;
    }

    public override int StartTime()
    {
        return 0;
    }

    public override void HandleNicePizza()
    {

    }

    public override void HandlePerfectPizza()
    {

    }
    public override void HandleWrongPizza()
    {
        throw new System.NotImplementedException();
    }
}
