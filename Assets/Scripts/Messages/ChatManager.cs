using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChatManager : MonoBehaviour
{
    [Tooltip("Text object for name of the contact")]
    [SerializeField] TMP_Text _contactName;

    [Tooltip("Object of contact list panel")]
    [SerializeField] GameObject _contactsWindow;

    [Tooltip("Transform of panel for messages")]
    [SerializeField] Transform _messagesPanel;
    
    [Tooltip("Transform of panel for messages")]
    [SerializeField] ScrollRect _scrollRect;

    [Tooltip("Prefab of message box object")]
    [SerializeField] GameObject _messagePrefab;




    List<GameObject> _messages = new List<GameObject>();
    Rect viewPortRect;
    bool _isActive = false;

    public void Activate() {  _isActive = true; }

    private void Start()
    {
        _messages.Clear();
        _isActive = false;
    }

    private void Update()
    {
        foreach (var message in _messages) 
        {
            message.GetComponent<MessageBox>().CheckIfMessageRead();
        }
        //if (!_messageData.IsRead && _isActive)
        //{
            

        //}

    }
    public void AddMessage(Message newMessage)
    {
        GameObject newMessageBox = Instantiate(_messagePrefab, _messagesPanel);
        newMessageBox.GetComponent<MessageBox>().Initialize(newMessage, _scrollRect.viewport);

        _messages.Add(newMessageBox);

    }

    public void CloseChat()
    {
        _isActive = false;
        foreach (GameObject message in _messages)
        {
            Destroy(message);
        }
        _messages.Clear();
        _contactsWindow.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenChat(string contactName)
    {
        _contactName.text = contactName;
        _contactsWindow.SetActive(false);
        gameObject.SetActive(true);
    }



    
}
