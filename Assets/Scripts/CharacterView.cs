using System;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterView : MonoBehaviour
{
    [SerializeField]
    private Transform _characterRoot;
    
    private CompositeDisposable _subscriptions;
    private GameObject _character;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable()
        {
            EventStreams.Game.Subscribe<CharacterSelectedEvent>(OnCharacterSelected),
            EventStreams.Game.Subscribe<CharacterRotatedEvent>(OnCharacterRotated)
        };
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }

    private void OnCharacterSelected(CharacterSelectedEvent eventData)
    {
        if (_character != null)
        {
            Destroy(_character);
        }

        _character = Instantiate(eventData.Character.Prefab, _characterRoot);
        _character.transform.rotation = Quaternion.identity;
        _character.SetActive(true);
    }
    
    private void OnCharacterRotated(CharacterRotatedEvent eventData)
    {
        if (_character != null)
        {
            _character.transform.Rotate(0, -eventData.Delta, 0);
        }
    }
}