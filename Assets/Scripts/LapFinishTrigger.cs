using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapFinishTrigger : MonoBehaviour
{
    public GameState gameState;
    public GameObject LapFinishTrig;
    public GameObject LapStartTrig;
    public GameObject MenuEnd;

    private void OnTriggerEnter(Collider Player)
    {
        LapFinishTrig.SetActive(false);
        LapStartTrig.SetActive(true);
        gameState.RaceFinished = true;
        gameState.RaceInProgress = false;
        MenuEnd.SetActive(true);
    }
}
