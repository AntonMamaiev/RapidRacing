using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float BestTimeRace;
    public float PreviousTime;

    public GameData()
    {
        this.BestTimeRace = 999999;
        this.PreviousTime = 0;
    }
}
