using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GhostCarDataListItem : ISerializationCallbackReceiver
{
    [System.NonSerialized]
    public Vector3 position = Vector3.zero;

    [System.NonSerialized]
    public Quaternion rotation;

    [System.NonSerialized]
    public float timeSinceLevelLoaded = 0;

    [SerializeField]
    int x = 0;

    [SerializeField]
    int y = 0;

    [SerializeField]
    int z = 0;

    [SerializeField]
    int rx = 0;

    [SerializeField]
    int ry = 0;

    [SerializeField]
    int rz = 0;

    [SerializeField]
    int rw = 0;

    [SerializeField]
    int t = 0;

    public GhostCarDataListItem(Vector3 position_, Quaternion rotation_, float timeSinceLevelLoaded_)
    {
        position = position_;
        rotation = rotation_;
        timeSinceLevelLoaded = timeSinceLevelLoaded_;
    }

    public void OnBeforeSerialize()
    {
        t = (int)(timeSinceLevelLoaded * 1000.0f);
        x = (int)(position.x * 1000.0f);
        y = (int)(position.y * 1000.0f);
        z = (int)(position.z * 1000.0f);
        rx = (int)(rotation.x * 1000.0f);
        ry = (int)(rotation.y * 1000.0f);
        rz = (int)(rotation.z * 1000.0f);
        rw = (int)(rotation.w * 1000.0f);
        //r = 
    }
    public void OnAfterDeserialize()
    {
        timeSinceLevelLoaded = t / 1000.0f;
        position.x = x / 1000.0f;
        position.y = y / 1000.0f;
        position.z = z / 1000.0f;
        rotation.x = rx / 1000.0f;
        rotation.y = ry / 1000.0f;
        rotation.z = rz / 1000.0f;
        rotation.w = rw / 1000.0f;
    }
}
