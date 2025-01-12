using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContactManager : MonoBehaviour
{
    [Header("Contact name")]
    [SerializeField] string _name;
    [Tooltip("Text object for the name in contact list")]
    [SerializeField] TMP_Text _nameText;
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
    int _unreadMessagesAmount = 0;


    const string ReadText = "Read";
    const string NewMessagesText = " New Messages";


    private void Start()
    {
        _nameText.text = _name;

        _chatManager = FindAnyObjectByType<ChatManager>(FindObjectsInactive.Include);
        if (_chatManager == null) Debug.LogError("Chat manager not found for contact " + _name);

        UpdateMessagesAmount();
    }

    private void UpdateMessagesAmount()
    {
        if (_unreadMessagesAmount > 0)
        {
            _unreadMessagesAmountText.text = _unreadMessagesAmount.ToString() + NewMessagesText;
            _newMessagesIcon.SetActive(true);
            _noMessagesIcon.SetActive(false);
        }
        else
        {
            if (_unreadMessagesAmount < 0) _unreadMessagesAmount = 0;
            _unreadMessagesAmountText.text = ReadText;
            _newMessagesIcon.SetActive(false);
            _noMessagesIcon.SetActive(true);
        }
    }


    public void AddMessage(Message newMessage)
    {
        _unreadMessagesAmount++;
        UpdateMessagesAmount();
        _messages.Add(newMessage);
    }

    public void OpenChat()
    {
        if (_chatManager == null) return;
        _chatManager.OpenChat(_name);
        foreach (var message in _messages) 
        {
            _chatManager.AddMessage(message);
        }

    }
}

