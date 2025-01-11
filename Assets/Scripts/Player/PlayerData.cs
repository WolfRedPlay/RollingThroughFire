using UnityEngine;

public class PlayerData : MonoBehaviour, IDataPersistence
{
    public void LoadData(GameData data)
    {
        gameObject.transform.position = data.PlayerPosition;
        gameObject.transform.rotation = data.PlayerRotation; 
    }

    public void SaveData(ref GameData data)
    {
        return;
    }
}
