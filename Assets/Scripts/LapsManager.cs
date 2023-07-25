using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class LapsManager : MonoBehaviour, IDataSaveLoad
{
    public static int MinCount;
    public static int SecCount;
    public static float MilliCount;
    public static int MilliDisplay;
    private float BestTime;
    public bool BestTimeGained;
    private float LapTime;
    public GameState gameState;
    public LapTrigger LapTrigger;
    public TMP_Text MinuteBox;
    public TMP_Text SecondBox;
    public TMP_Text MilliBox;
    public TMP_Text RecordMinuteBox;
    public TMP_Text RecordSecondBox;
    public TMP_Text RecordMilliBox;
    public TMP_Text PrevMinuteBox;
    public TMP_Text PrevSecondBox;
    public TMP_Text PrevMilliBox;
    public TMP_Text StartUI;
    Scene scene;
    int onetime = 0;

    private void Start()
    {
        Invoke("CanPlay", 3);
        scene = SceneManager.GetActiveScene();
        StartCoroutine(StartTimer());
        BestTimeGained = false;
        DataSaveLoadManager.instance.LoadGame();
    }
    
    void Update()
    {
        if (gameState.RaceInProgress)
        {
            MilliCount += Time.deltaTime * 1000;
            MilliDisplay = (int)(MilliCount % 1000);

            if (MilliCount <= 9)
                MilliBox.text = "00" + MilliDisplay;
            if (MilliCount <= 99)
                MilliBox.text = "0" + MilliDisplay;
            else
                MilliBox.text = "" + MilliDisplay;

            if (MilliCount >= 1000)
            {
                MilliCount = 0;
                SecCount += 1;
            }

            if (SecCount <= 9)
                SecondBox.text = "0" + SecCount + ".";
            else
                SecondBox.text = "" + SecCount + ".";

            if (SecCount >= 60)
            {
                SecCount = 0;
                MinCount += 1;
            }

            if (MinCount <= 9)
                MinuteBox.text = "0" + MinCount + ":";
            else
                MinuteBox.text = "" + MinCount + ":";
        }
        
        if (gameState.RaceRestarted)
        {
            MinCount = SecCount = 0; MilliCount = 0; MilliDisplay = 0;
            MinuteBox.text = "00:"; SecondBox.text = "00."; MilliBox.text = "000";


            LapTrigger.StartTrigger.SetActive(true);
            LapTrigger.FinishTrigger.SetActive(false);
            gameState.RaceStarted = false;
            gameState.RaceFinished = false;
            gameState.RaceRestarted = false;

            SceneManager.LoadScene(scene.name);
        }
        if (gameState.RaceFinished)
        {
            PrevMinuteBox.text = MinuteBox.text; PrevSecondBox.text = SecondBox.text; PrevMilliBox.text = MilliBox.text;
            LapTime = (MinCount * 1000 * 60) + (SecCount * 1000) + MilliCount;
            if (LapTime < BestTime)
            {
                BestTimeGained = true;
                BestTime = LapTime;
                RecordMinuteBox.text = MinuteBox.text; RecordSecondBox.text = SecondBox.text; RecordMilliBox.text = MilliBox.text;
                DataSaveLoadManager.instance.SaveGame();
            }
            if (onetime == 0)
                DataSaveLoadManager.instance.SaveGame();
            onetime++;
        }

    }
    
    void CanPlay()
    {
        gameState.RaceInProgress = true;
    }

    IEnumerator StartTimer()
    {
        MinCount = SecCount = 0; MilliCount = 0; MilliDisplay = 0;
        MinuteBox.text = "00:"; SecondBox.text = "00."; MilliBox.text = "000";

        StartUI.text = "3";

        yield return new WaitForSeconds(1f);
        StartUI.text = "2";
        yield return new WaitForSeconds(1f);
        StartUI.text = "1";
        yield return new WaitForSeconds(1f);
        StartUI.text = "START";
        yield return new WaitForSeconds(1f);
        float duration = 2f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            StartUI.color = new Color(StartUI.color.r, StartUI.color.g, StartUI.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
    public void LoadPrevTime(float milliseconds)
    {
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliseconds);
        string minutes = timeSpan.Minutes.ToString();
        string seconds = timeSpan.Seconds.ToString();
        string millisecondsStr = timeSpan.Milliseconds.ToString();


        if (minutes == "0") minutes = "00";
        if (seconds == "0") seconds = "00";
        if (millisecondsStr == "0") millisecondsStr = "000";

        PrevMinuteBox.text = minutes + ":";
        PrevSecondBox.text = seconds + ".";
        PrevMilliBox.text = millisecondsStr;

    }
    public void LoadBestTime(float milliseconds)
    {
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliseconds);
        string minutes = timeSpan.Minutes.ToString();
        string seconds = timeSpan.Seconds.ToString();
        string millisecondsStr = timeSpan.Milliseconds.ToString();


        if (minutes == "0") minutes = "00";
        if (seconds == "0") seconds = "00";
        if (millisecondsStr == "0") millisecondsStr = "000";

        RecordMinuteBox.text = minutes + ":";
        RecordSecondBox.text = seconds + ".";
        RecordMilliBox.text = millisecondsStr;

    }

    public void LoadData(GameData data)
    {
        this.BestTime = data.BestTimeRace;
        this.LapTime = data.PreviousTime;
        if(BestTime != 999999)
            LoadBestTime(BestTime);
        LoadPrevTime(LapTime);
    }
    public void SaveData(ref GameData data)
    {
        data.BestTimeRace = this.BestTime;
        data.PreviousTime = this.LapTime;
    }
}
