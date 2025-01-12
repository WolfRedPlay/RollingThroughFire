using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    [Tooltip("Text object for message content")]
    [SerializeField] TMP_Text _textField;

    public void SetText(string newText)
    {
        _textField.text = newText;
    }
}
