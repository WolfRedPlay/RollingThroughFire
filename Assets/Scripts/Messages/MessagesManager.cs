using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MessagesManager : MonoBehaviour
{

    List<ContactManager> _contacts; 

    void Start()
    {
        _contacts = FindObjectsByType<ContactManager>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
    }

    
    public void SendNewMessage(Message_SO newMessage)
    {
        ContactManager contactToAddMessage = _contacts.Find(x => x.Contact == newMessage.Contact);
        if (contactToAddMessage == null) return;
        contactToAddMessage.AddMessage(newMessage);
    }
}
