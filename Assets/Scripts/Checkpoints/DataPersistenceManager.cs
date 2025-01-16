using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName = "";

    [Header("Scenes of levels")]
    [SerializeField]List<string> _levelScenesNames;
    GameData _gameData;
    List<IDataPersistence> _dataPersistenceObjects;
    FileDataHandler _fileDataHandler;
    

    public static DataPersistenceManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There are more then one Data manager!");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

    }
    


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _fileDataHandler = new FileDataHandler(Application.dataPath, fileName);
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (_levelScenesNames.Find(x => x == SceneManager.GetActiveScene().name) != null)
            NewGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_levelScenesNames.Find(x => x == scene.name) != null)
            LoadGame();
    }

    public void NewGame()
    {
        _gameData = new GameData();
        _fileDataHandler.Save(_gameData);
        Debug.Log("New save created");
    }

    public void LoadGame()
    {
        _dataPersistenceObjects = FindAllDataPersistenceObjects();
        _gameData = _fileDataHandler.Load();
        if (_gameData == null)
        {
            Debug.LogWarning("No data found! Initializing data to default.");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(_gameData);
        }

        Debug.Log("GameLoaded");
    }

    public void SaveGame()
    {
        _dataPersistenceObjects = FindAllDataPersistenceObjects();
        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref _gameData);
        }

        _fileDataHandler.Save(_gameData);

        Debug.Log("GameSaved");
    }
}
