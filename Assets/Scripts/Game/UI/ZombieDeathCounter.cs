using UnityEngine;
using SimpleEventBus.Disposables;
using TMPro;
using UnityEngine.UI;

public class ZombieDeathCounter : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private TextMeshProUGUI _counter;
    [SerializeField] 
    private Image _image;
    
    private CompositeDisposable _subscriptions;
    private float _zombiesCountInScene;
    private float _zombiesCounter;
    private bool _isZombieCounterStopped;
    
    private void Awake()
    {
        _slider.value = 0;
        _slider.minValue = 0;

        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<ZombieDeathEvent>(ZombieDeathCountHandler),
            EventStreams.Game.Subscribe<CharacterStateEvent>(StopZombieDeathCounter)
        };
    }

    private void ZombieDeathCountHandler(ZombieDeathEvent eventData)
    {
        if (_isZombieCounterStopped)
        {
            return;
        }
        _zombiesCounter += 1;
        if (_zombiesCountInScene == _zombiesCounter)
        {
            _slider.value = _zombiesCounter;
            _image.color = Color.green;
            _counter.text = (GlobalConstants.ZOMBIE_DEATH_COUNTER_COMPLETE_TEXT);
            return;
        }
        _counter.text = ($"{_zombiesCounter}/{_zombiesCountInScene}");
        _slider.value = _zombiesCounter;
    }
    
    private void StopZombieDeathCounter(CharacterStateEvent eventData)
    {
        _isZombieCounterStopped = true;
    }
    
    public void Initialize(float zombiesCount)
    {
        _zombiesCountInScene = zombiesCount;
        _slider.maxValue = zombiesCount;
        _counter.text = ($"0/{zombiesCount}");
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}