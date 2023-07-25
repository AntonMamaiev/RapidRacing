using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    public GameState gameState;
    public GameObject StartTrigger;
    public GameObject FinishTrigger;

    private void OnTriggerEnter(Collider Player)
    {
        StartTrigger.SetActive(false);
        FinishTrigger.SetActive(true);
        gameState.RaceInProgress = true;
        gameState.RaceFinished = false;
        gameState.RaceStarted = true;
    }
}
