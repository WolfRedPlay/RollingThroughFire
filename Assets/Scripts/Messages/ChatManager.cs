using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.AppUI.UI;
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

    public void Activate() 
    { 
        StartCoroutine(ActivationDelay()); 
    }

    IEnumerator ActivationDelay()
    {
        yield return new WaitForSeconds(.1f);
        _isActive = true;
    }


    private void LateUpdate()
    {
        if (_isActive)
            foreach (var message in _messages) 
            {
                message.GetComponent<MessageBox>().CheckIfMessageRead();
            }

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
        _contactsWindow.SetActive(false);
        gameObject.SetActive(true);
        _contactName.text = contactName;
        StartCoroutine(PositionDelay());
    }

    IEnumerator PositionDelay()
    {
        yield return new WaitForSeconds(.1f);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        _scrollRect.verticalNormalizedPosition = CalculateContentPosition();
    }

    int FindFirstNotReadMessage()
    {
        int index = -1;
        for(int i = 0; i < _messages.Count; i++)
        {
            if (!_messages[i].GetComponent<MessageBox>().Message.IsRead)
            {
                index = i;
                break;
            }
        }

        if (index == -1) index = _messages.Count - 1;

        return index;
    }

    private float CalculateContentHeight()
    {
        float totalHeight = 0f;

        totalHeight += _messagesPanel.GetComponent<VerticalLayoutGroup>().padding.top;

        for (int i = 0; i < _messages.Count; i++)
        {
            RectTransform rec = _messages[i].GetComponent<RectTransform>();
            totalHeight += rec.rect.height;
            if (i < _messages.Count - 1) totalHeight += _messagesPanel.GetComponent<VerticalLayoutGroup>().spacing;
        }

        totalHeight += _messagesPanel.GetComponent<VerticalLayoutGroup>().padding.bottom;

        return totalHeight;
    }

    private float CalculatePositionUpToIndex(int index)
    {
        float position = 0f;

        for (int i = 0; i < index; i++)
        {
            RectTransform rec = _messages[i].GetComponent<RectTransform>();
            if (rec != null)
            {
                position += rec.rect.height + _messagesPanel.GetComponent<VerticalLayoutGroup>().spacing;
            }
        }

        return position;
    }

    private float CalculateContentPosition()
    {
        int index = FindFirstNotReadMessage();

        float viewportHeight = _scrollRect.viewport.rect.height;
        float contentHeight = CalculateContentHeight();

        if (contentHeight <= viewportHeight)
        {
            return 1f;
        }
        float positionInContent = CalculatePositionUpToIndex(index);


        float normalizedPosition = 1f - (positionInContent / (contentHeight - viewportHeight));
        return Mathf.Clamp01(normalizedPosition);
    }
}
