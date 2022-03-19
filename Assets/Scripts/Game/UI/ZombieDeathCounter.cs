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
    
    
    private CompositeDisposable _subscriptions;
    private float _zombiesCountInScene;
    private float _zombiesCounter;
    
    private void Awake()
    {
        _zombiesCountInScene = FindObjectOfType<ZombieManager>().GetZombiesCount();
        _counter.text = ($"0/{_zombiesCountInScene}");
        _slider.maxValue = _zombiesCountInScene;
        _slider.minValue = 0;
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<ZombieDeathEvent>(ZombieDeathCountHandler)
        };
    }

    private void ZombieDeathCountHandler(ZombieDeathEvent eventData)
    {
        _counter.text = ($"{_zombiesCounter += 1}/{_zombiesCountInScene}");
        _slider.value = _zombiesCounter;
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}