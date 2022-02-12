using Pools;
using SimpleEventBus.Disposables;
using UnityEngine;

public class CharacterCharacteristicsView : MonoBehaviour
{
    [SerializeField] 
    private CharacterCharacteristicView _characteristicView;
    [SerializeField]
    private RectTransform _characteristicsRoot;
    
    private CompositeDisposable _subscriptions;
    private MonoBehaviourPool<CharacterCharacteristicView> _characteristicsPool;

    private void Awake()
    {
        _characteristicsPool = new MonoBehaviourPool<CharacterCharacteristicView>(_characteristicView, _characteristicsRoot);
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CharacterSelectedEvent>(OnCharacterSelected)
        };
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }

    private void OnCharacterSelected(CharacterSelectedEvent eventData)
    {
        _characteristicsPool.ReleaseAll();
        var selectedCharacter = eventData.Character;
        foreach (var characteristic in selectedCharacter.Characteristics)
        {
            _characteristicsPool.Take().Initialize(characteristic);
        }
    }
}