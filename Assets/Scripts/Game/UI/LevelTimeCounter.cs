using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SimpleEventBus.Disposables;

public class LevelTimeCounter : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private TextMeshProUGUI _counter;
    [SerializeField] 
    private Image _image;
    [SerializeField]
    private float _levelTimeLimit;

    private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CharacterDeathEvent>(StopLevelTimeCounter)
        };
    }

    private void Start()
    {
        _slider.value = 0;
        _slider.minValue = 0;
        _slider.maxValue = _levelTimeLimit;
        _counter.text = ($"0/{_levelTimeLimit}");
        StartCoroutine(DoTimer());
    }

    private IEnumerator DoTimer(float countTime = 1f)
    {
        int count = 0;
        while (_levelTimeLimit > count)
        {
            yield return new WaitForSeconds(countTime);
            count++;
            _counter.text = ($"{count}/{_levelTimeLimit}");
            _slider.value = count;
        }
        _image.color = Color.red;
        _counter.text = (GlobalConstants.LEVEL_TIME_COUNTER_IS_OVER);
        EventStreams.Game.Publish(new CharacterDeathEvent());
    }

    private void StopLevelTimeCounter(CharacterDeathEvent eventData)
    {
        StopAllCoroutines();
    }
    
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}