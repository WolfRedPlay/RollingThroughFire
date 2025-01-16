using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour, IDataPersistence
{
    [Header("Level Stages")]
    [SerializeField] List<Stage> _stages;

    int _currentStageIndex = -1;

    public void LoadData(GameData data)
    {
        foreach (var stage in _stages)
        {
            stage.Checkpoint.SetCheckpointActive(false);
        }

        _currentStageIndex = data.CurrentStage;
        if (_currentStageIndex != -1)
            _stages[_currentStageIndex].LoadStage();
    }

    public void SaveData(ref GameData data)
    {
        _currentStageIndex++;
        data.CurrentStage = _currentStageIndex;
        if (_currentStageIndex != -1)
        {
            data.PlayerPosition = _stages[_currentStageIndex].Checkpoint.SaveTransform.position;
            data.PlayerRotation = _stages[_currentStageIndex].Checkpoint.SaveTransform.rotation;
        }

    }

    void Start()
    {
        foreach (var stage in _stages) 
        { 
            stage.Initialize();

            DataPersistenceManager dataManager = DataPersistenceManager.Instance;
            stage.OnStageStarted.AddListener(dataManager.SaveGame);

            if (_currentStageIndex == -1) stage.Checkpoint.SetCheckpointActive(false);
        }

        if (_currentStageIndex == -1) _stages[0].Checkpoint.SetCheckpointActive(true);
    }

    void Update()
    {
        
    }
}
