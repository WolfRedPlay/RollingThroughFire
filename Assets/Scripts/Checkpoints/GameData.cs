using System.Collections.Generic;
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

    public Dictionary<Contact, List<Message>> MessagesList = new Dictionary<Contact, List<Message>>();


    public GameData()
    {
        PlayerPosition = new Vector3 (1.35f, 1.68f, 0.11f);
        PlayerRotation = Quaternion.Euler(0f, 90f, 0f);
        CurrentStage = -1;
        MessagesList.Add(Contact.COWORKER, new List<Message>());
        MessagesList.Add(Contact.CAMPUS, new List<Message>());
        MessagesList.Add(Contact.MOM, new List<Message>());
        MessagesList.Add(Contact.SUPERVISOR, new List<Message>());
    }
}
