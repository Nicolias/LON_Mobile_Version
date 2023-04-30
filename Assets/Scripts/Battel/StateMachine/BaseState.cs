using System;
using UnityEngine;

public abstract class BaseState
{
    protected BattelStationSwitcher BattelStationSwitcher { get; private set; }

    public BaseState(BattelStationSwitcher battelStationSwitcher)
    {
        BattelStationSwitcher = battelStationSwitcher;
    }

    public abstract void Enter();
    public abstract void Exit();
}
