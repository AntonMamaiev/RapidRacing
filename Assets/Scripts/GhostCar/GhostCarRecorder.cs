using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostCarRecorder : MonoBehaviour
{
    public Transform carObject;
    public GameObject ghostcarObject;

    GhostCarData ghostCarData = new GhostCarData();

    bool isRecording = true;

    Rigidbody carRigidbody;
    public GameState gameState;
    public LapsManager lapsManager;


    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        StartCoroutine(RecordCarPositionCO());
        GameObject ghostCar = Instantiate(ghostcarObject);
        ghostCar.GetComponent<GhostCarPlayback>().LoadData();
    }
    private void Update()
    {
        if (lapsManager.BestTimeGained)
        {
            if (gameState.RaceFinished)
            {
                SaveData();
                gameState.RaceFinished = false;
                lapsManager.BestTimeGained = false;
            }
        }
    }
    IEnumerator RecordCarPositionCO()
    {
        while (isRecording)
        {
            if (carObject != null)
                ghostCarData.AddDataItem(new GhostCarDataListItem(carRigidbody.position, carRigidbody.rotation, Time.timeSinceLevelLoad));

            yield return new WaitForSeconds(0.1f);
        }
    }
    void SaveData()
    {
        string jsonEncodedData = JsonUtility.ToJson(ghostCarData);

        Debug.Log($"Saved ghost data {jsonEncodedData}");

        PlayerPrefs.SetString($"{SceneManager.GetActiveScene().name}_ghost", jsonEncodedData);
        PlayerPrefs.Save();

        isRecording = false;
    }
}
