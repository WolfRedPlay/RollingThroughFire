using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    [Tooltip("Text object for name of the contact")]
    [SerializeField] TMP_Text _contactName;

    [Tooltip("Object of contact list panel")]
    [SerializeField] GameObject _contactsWindow;

    [Tooltip("Transform of panel for messages")]
    [SerializeField] Transform _messagesPanel;

    [Tooltip("Prefab of message box object")]
    [SerializeField] GameObject _messagePrefab;


    List<GameObject> _messages = new List<GameObject>();

    private void Start()
    {
        _messages.Clear();
    }
    public void AddMessage(Message newMessage)
    {
        GameObject newMessageBox = Instantiate(_messagePrefab, _messagesPanel);
        newMessageBox.GetComponent<MessageBox>().SetText(newMessage.Text);
        _messages.Add(newMessageBox);
    }

    public void CloseChat()
    {
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
