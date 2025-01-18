using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "Message")]
public class Message_SO : ScriptableObject
{
    [Tooltip("The text of message that will be shown")]
    [SerializeField] [TextArea] string _text;

    [SerializeField] Contact _contactToSend;
    [SerializeField] bool _isUrgent = false;

    public Contact Contact => _contactToSend;
    public string Text => _text;
    public bool IsUrgent => _isUrgent;
}
