using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataSaveLoadManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private GameData gameData;
    private List<IDataSaveLoad> dataSaveLoadObjects;
    private FileDataHandler dataHandler;
    public static DataSaveLoadManager instance { get; private set; }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataSaveLoadObjects = FindAllSaveLoadObjects();
        LoadGame();
    }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Data Save Load");
        }
        instance = this;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        Debug.Log("LOADED");
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();
        }

        foreach (IDataSaveLoad dataSaveLoad in dataSaveLoadObjects)
        {
            dataSaveLoad.LoadData(gameData);
        }

    }
    public void SaveGame()
    {
        Debug.Log("SAVED");

        foreach (IDataSaveLoad dataSaveLoad in dataSaveLoadObjects)
        {
            dataSaveLoad.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataSaveLoad> FindAllSaveLoadObjects()
    {
        IEnumerable<IDataSaveLoad> dataSaveLoadObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataSaveLoad>();
        
        return new List<IDataSaveLoad>(dataSaveLoadObjects);
    }
}
