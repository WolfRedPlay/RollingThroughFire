using UnityEngine;

public class GameData
{
    #region PLAYER
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    #endregion

    #region STAGES
    public int CurrentStage;
    #endregion

    public GameData()
    {
        PlayerPosition = new Vector3 (1.35f, 1.68f, 0.11f);
        PlayerRotation = Quaternion.Euler(0f, 90f, 0f);
        CurrentStage = -1;
    }
}
