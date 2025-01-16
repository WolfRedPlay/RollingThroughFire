using System;
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

    public List<ContactInfo> ContactsInfo = new List<ContactInfo>();


    public GameData()
    {
        PlayerPosition = new Vector3 (1.35f, 1.68f, 0.11f);
        PlayerRotation = Quaternion.Euler(0f, 90f, 0f);
        CurrentStage = -1;
        ContactsInfo.Add(new ContactInfo(Contact.MOM));
        ContactsInfo.Add(new ContactInfo(Contact.COWORKER));
        ContactsInfo.Add(new ContactInfo(Contact.SUPERVISOR));
        ContactsInfo.Add(new ContactInfo(Contact.CAMPUS));
    }
    


    public void AssignMessagesToContact(Contact contactToAssign, List<Message> newMessages)
    {
        int index = ContactsInfo.FindIndex(x => x.Contact == contactToAssign);
        if (index == -1)
        {
            ContactsInfo.Add(new ContactInfo(Contact.MOM, newMessages));
        }
        else
        {

            ContactInfo newInfo = ContactsInfo[index];
            newInfo.MessagesList = newMessages;
            ContactsInfo[index] = newInfo;
        }
    }

    public List<Message> GetMessagesForContact(Contact contactToFind)
    {
        int index = ContactsInfo.FindIndex(x => x.Contact == contactToFind);
        if (index == -1)
        {
            return new List<Message>();
        }
        else
        {
            return ContactsInfo[index].MessagesList;
        }
    }
}

[Serializable]
public struct ContactInfo
{
    public Contact Contact;
    public List<Message> MessagesList;

    public ContactInfo(Contact contact)
    {
        Contact = contact;
        MessagesList = new List<Message>();
    }
    public ContactInfo(Contact contact, List<Message> messages)
    {
        Contact = contact;
        MessagesList = messages;
    }
}
