using System;
using UnityEngine;
using SimpleEventBus.Disposables;
using UnityEngine.UI;

public class CharacterHealthBar : MonoBehaviour
{
    [SerializeField] 
    private Image _image;
    
    private CompositeDisposable _subscriptions;
    private float _maxCharacterHP = 1f;
    private float _damage = 0.2f;
    private float _armor = 0.2f;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CharacterTakeDamageEvent>(CharacterTakeDamage),
            EventStreams.Game.Subscribe<CharacterStateEvent>(HideCharacterHealthBar),
            EventStreams.Game.Subscribe<FirstAidKitActivatedEvent>(HealCharacter)
        };
    }

    private void CharacterTakeDamage(CharacterTakeDamageEvent eventData)
    {
        var damageAmount = eventData.ZombieDamage * _armor;
        _image.fillAmount -= eventData.ZombieDamage - damageAmount;
        SetColorHealth();
    }

    private void SetColorHealth()
    {
        if (_image.fillAmount <= 0.00001f)
        {
            EventStreams.Game.Publish(new CharacterStateEvent(false));
        }
        if (_image.fillAmount >= GetHPBorderColor(0.4f) && _image.fillAmount <= GetHPBorderColor(0.7f))
        {
            _image.color = Color.yellow;
        }
        if (_image.fillAmount < GetHPBorderColor(0.4f))
        {
            _image.color = Color.red;
        }
    }

    private float GetHPBorderColor(float border)
    {
        return Mathf.Clamp01(border * _maxCharacterHP);
    }
    
    private void HideCharacterHealthBar(CharacterStateEvent eventData)
    {
        _image.fillAmount = 0f;
        _subscriptions?.Dispose();
    }
    
    private void HealCharacter(FirstAidKitActivatedEvent eventData)
    {
        _image.fillAmount = _maxCharacterHP;
        _image.color = Color.green;
    }
    
    public void Initialize(float health, float armor)
    {
        _image.fillAmount = health;
        _maxCharacterHP = health;
        _armor = armor;
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}