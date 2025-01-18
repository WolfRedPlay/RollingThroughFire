using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContactManager : MonoBehaviour, IDataPersistence
{
    [Header("Contact name")]
    [SerializeField] string _name;
    [Tooltip("Text object for the name in contact list")]
    [SerializeField] TMP_Text _nameText;
    [Tooltip("Contact from enum for messages manager to find it")]
    [SerializeField] Contact _contact;
    [Space]
    [Header("Icons")]
    [Tooltip("Text object for amount of unread messages")]
    [SerializeField] TMP_Text _unreadMessagesAmountText;
    [Tooltip("Object for icon of unread messages")]
    [SerializeField] GameObject _newMessagesIcon;
    [Tooltip("Object for icon all messages are read")]
    [SerializeField] GameObject _noMessagesIcon;


    List<Message> _messages = new List<Message>();
    ChatManager _chatManager;


    public Contact Contact => _contact;

    const string ReadText = "Read";
    const string NewMessagesText = " New Messages";
    const string UrgentMessagesText = "URGENT:";


    private void Start()
    {
        _nameText.text = _name;

        _chatManager = FindAnyObjectByType<ChatManager>(FindObjectsInactive.Include);
        if (_chatManager == null) Debug.LogError("Chat manager not found for contact " + _name);
    }

    private void OnEnable()
    {
        UpdateMessagesAmount();
    }

    private void UpdateMessagesAmount()
    {
        int unreadMessagesAmount = 0;
        bool isUrgent = false;

        foreach (Message message in _messages)
        {
            if (!message.IsRead) unreadMessagesAmount++;
            if (message.IsUrgent) isUrgent = true;
        }


        if (unreadMessagesAmount > 0)
        {
            _newMessagesIcon.SetActive(true);
            _noMessagesIcon.SetActive(false);
            if (isUrgent)
            {
                _unreadMessagesAmountText.text = UrgentMessagesText + unreadMessagesAmount.ToString() + NewMessagesText;
                _newMessagesIcon.GetComponent<Image>().color = Color.yellow;
            }


        }
        else
        {
            _unreadMessagesAmountText.text = ReadText;
            _newMessagesIcon.SetActive(false);
            _noMessagesIcon.SetActive(true);
        }
    }


    public void AddMessage(Message_SO newMessageSO)
    {
        UpdateMessagesAmount();
        Message newMessage = new Message(newMessageSO.Text, newMessageSO.IsUrgent);
        _messages.Add(newMessage);
    }

    public void OpenChat()
    {
        if (_chatManager == null) return;
        foreach (Message message in _messages)
        {
            _chatManager.AddMessage(message);
        }
        _chatManager.OpenChat(_name);
        _chatManager.Activate();
    }

    public void LoadData(GameData data)
    {
        _messages = data.GetMessagesForContact(_contact);
    }

    public void SaveData(ref GameData data)
    {
        data.AssignMessagesToContact(_contact, _messages);
    }
}

[Serializable]
public class Message
{
    public Message(string text, bool isUrgent)
    {
        Text = text;
        IsRead = false;
        IsUrgent = isUrgent;
    }
    public bool IsUrgent;
    public string Text;
    public bool IsRead;
}

public enum Contact
{
    COWORKER,
    CAMPUS,
    SUPERVISOR,
    MOM
}

