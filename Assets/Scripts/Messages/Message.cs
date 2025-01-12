using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "Message")]
public class Message : ScriptableObject
{
    [Tooltip("The text of message that will be shown")]
    [SerializeField] [TextArea] string _text;

    public string Text { get { return _text; } }
}
