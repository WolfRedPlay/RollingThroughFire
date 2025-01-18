using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour, IDataPersistence
{
    [SerializeField] bool _movementHelper = false;
    [SerializeField] Toggle _movementHelperToggle;


    public bool MovementHelper {  get { return _movementHelper; } set { _movementHelper = value; } }

    public void LoadData(GameData data)
    {
        _movementHelper = data.MovementHelper;
        _movementHelperToggle.isOn = _movementHelper;
    }

    public void SaveData(ref GameData data)
    {
        data.MovementHelper = _movementHelper;
    }
}
