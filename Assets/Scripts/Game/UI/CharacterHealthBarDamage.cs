using System;
using UnityEngine;
using SimpleEventBus.Disposables;
using UnityEngine.UI;

public class CharacterHealthBarDamage : MonoBehaviour
{
    [SerializeField] 
    private Image _image;
    
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CharacterTakeDamageEvent>(CharacterTakeDamage)
        };
    }

    private void CharacterTakeDamage(CharacterTakeDamageEvent eventData)
    {
        _image.fillAmount -= 0.2f;
        if (_image.fillAmount <= 0.01f)
        {
            EventStreams.Game.Publish(new CharacterDeathEvent());
        }
        if (_image.fillAmount >= 0.4f & _image.fillAmount <= 0.6f)
        {
            _image.color = Color.yellow;
        }
        if (_image.fillAmount <= 0.3f)
        {
            _image.color = Color.red;
        }
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}