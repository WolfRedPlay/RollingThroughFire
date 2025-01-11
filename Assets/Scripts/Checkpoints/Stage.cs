using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Stage
{
    public UnityEvent OnStageStarted;
    
    public UnityEvent OnStageLoaded;

    [SerializeField] Checkpoint _checkpoint;

    public Checkpoint Checkpoint => _checkpoint;
    public void Initialize()
    {
        _checkpoint.SetStage(this);
    }

    public void StartStage()
    {
        OnStageStarted?.Invoke();
    }

    public void LoadStage()
    {
        OnStageLoaded?.Invoke();
    }
}
