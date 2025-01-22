using System.Collections.Generic;
using System.Linq;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;


public class MessagesManager : MonoBehaviour
{
    [SerializeField] HapticImpulsePlayer _hapticImpulse;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] float minTimeBetweenSounds = 0.1f;

    [SerializeField] AudioClip _notification;
    [SerializeField] AudioClip _alarm;
    
    
    List<ContactManager> _contacts;
    Queue<AudioClip> _notifications = new Queue<AudioClip>();
    bool _isNotifying = false;

    float lastPlayTime = 0f;


    void Start()
    {
        _contacts = FindObjectsByType<ContactManager>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
    }

    
    public void SendNewMessage(Message_SO newMessage)
    {
        ContactManager contactToAddMessage = _contacts.Find(x => x.Contact == newMessage.Contact);
        if (contactToAddMessage == null) return;
        contactToAddMessage.AddMessage(newMessage);

        if (newMessage.IsUrgent) _notifications.Enqueue(_alarm);
        else _notifications.Enqueue(_notification);
        

        PlayNextNotification();
    }


    private void PlayNextNotification()
    {
        if (_isNotifying) return;

        if (_notifications.Count == 0) return;

        if (Time.time - lastPlayTime < minTimeBetweenSounds)
        {
            _isNotifying = true;
            return;
        }

        AudioClip clip = _notifications.Dequeue();
        _audioSource.clip = clip;
        if (clip == _alarm) _audioSource.volume = .3f;
        else _audioSource.volume = .5f;
        _audioSource.Play();
        lastPlayTime = Time.time;
        _isNotifying = true;
        _hapticImpulse.SendHapticImpulse(1f, _audioSource.clip.length);

    }

    private void Update()
    {
        if (_isNotifying)
        {
            if (!_audioSource.isPlaying)
            {
                _isNotifying = false;
                PlayNextNotification();
            }
        }
    }
}
