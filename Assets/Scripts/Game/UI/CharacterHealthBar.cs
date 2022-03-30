using UnityEngine;
using SimpleEventBus.Disposables;
using UnityEngine.UI;

public class CharacterHealthBar : MonoBehaviour
{
    [SerializeField] 
    private Image _image;
    
    private CompositeDisposable _subscriptions;
    private float _maxCharacterHP;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CharacterTakeDamageEvent>(CharacterTakeDamage),
            EventStreams.Game.Subscribe<CharacterDeathEvent>(HideCharacterHealthBar),
            EventStreams.Game.Subscribe<FirstAidKitActivatedEvent>(HealCharacter)
            
        };
    }

    private void CharacterTakeDamage(CharacterTakeDamageEvent eventData)
    {
        _image.fillAmount -= 0.2f;
        SetColorHealth();
    }

    private void SetColorHealth()
    {
        if (_image.fillAmount <= 0.00001f)
        {
            EventStreams.Game.Publish(new CharacterDeathEvent());
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

    private float GetHPBorderColor(float borderColor)
    {
        return Mathf.Clamp01(borderColor * _maxCharacterHP);
    }
    
    private void HideCharacterHealthBar(CharacterDeathEvent eventData)
    {
        _image.fillAmount = 0f;
        _subscriptions?.Dispose();
    }
    
    private void HealCharacter(FirstAidKitActivatedEvent eventData)
    {
        _image.fillAmount = _maxCharacterHP;
        _image.color = Color.green;
    }
    
    public void Initialize(float count)
    {
        _image.fillAmount = count;
        _maxCharacterHP = count;
    }
}