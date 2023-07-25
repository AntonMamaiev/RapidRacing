using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostCarPlayback : MonoBehaviour
{
    GhostCarData ghostCarData = new GhostCarData();
    List<GhostCarDataListItem> ghostCarDataList = new List<GhostCarDataListItem>();

    int currentPlaybackIndex = 0;

    float lastStoredTime = 0.1f;
    Vector3 lastStoredPosition = Vector3.zero;
    Quaternion lastStoredRotation;
    float timePassed;
    float lerpPercentage;

    float duration = 0.1f;

    void Start()
    {
        
    }

    void Update()
    {
        if (ghostCarDataList.Count == 0)
            return;

        if (Time.timeSinceLevelLoad >= ghostCarDataList[currentPlaybackIndex].timeSinceLevelLoaded)
        {
            lastStoredTime = ghostCarDataList[currentPlaybackIndex].timeSinceLevelLoaded;
            lastStoredPosition = ghostCarDataList[currentPlaybackIndex].position;
            lastStoredRotation = ghostCarDataList[currentPlaybackIndex].rotation;

            if (currentPlaybackIndex < ghostCarDataList.Count - 1)
                currentPlaybackIndex++;

            duration = ghostCarDataList[currentPlaybackIndex].timeSinceLevelLoaded - lastStoredTime;
        }

        timePassed = Time.timeSinceLevelLoad - lastStoredTime;
        lerpPercentage = timePassed / duration;

        transform.position = Vector3.Lerp(lastStoredPosition, ghostCarDataList[currentPlaybackIndex].position, lerpPercentage);
        transform.rotation = Quaternion.Lerp(lastStoredRotation, ghostCarDataList[currentPlaybackIndex].rotation, lerpPercentage);

    }
    public void LoadData()
    {
        if (!PlayerPrefs.HasKey($"{SceneManager.GetActiveScene().name}_ghost"))
        {
            Destroy(gameObject);
        }
        else
        {
            string jsonEncodedData = PlayerPrefs.GetString($"{SceneManager.GetActiveScene().name}_ghost");

            ghostCarData = JsonUtility.FromJson<GhostCarData>(jsonEncodedData);
            ghostCarDataList = ghostCarData.GetDataList();

        }
    }
}
