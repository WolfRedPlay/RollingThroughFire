using Unity.AppUI.UI;
using UnityEngine;

public class SettingsHandler : MonoBehaviour, IDataPersistence
{
    [SerializeField] bool _movementHelper = false;

    Toggle _toggle;

    public bool MovementHelper {  get { return _movementHelper; } set { _movementHelper = value; } }

    public void LoadData(GameData data)
    {
        _movementHelper = data.MovementHelper;
    }

    public void SaveData(ref GameData data)
    {
        
        data.MovementHelper = _movementHelper;

    }
}
