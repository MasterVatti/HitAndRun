using SimpleEventBus.Disposables;
using UnityEngine;
using System.Collections;
using TMPro;

public class BombTimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _countdownCounter;
    [SerializeField]
    private Animation _bombCountdownAnimation;
    
    private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        _bombCountdownAnimation.Stop();
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<BombCountdownStartEvent>(StartBombCountdown)
        };
    }

    private void StartBombCountdown(BombCountdownStartEvent eventData)
    {
        _bombCountdownAnimation.Play();
        StartCoroutine(StartCountdown(eventData));
    }

    private IEnumerator StartCountdown(BombCountdownStartEvent eventData, float countTime = 1f)
    {
        _countdownCounter.text = ($"{eventData.BombTimer}");
        int count = eventData.BombTimer;
        while (count > 0)
        {
            yield return new WaitForSeconds(countTime);
            count--;
            _countdownCounter.text = ($"{count}");
        }
        gameObject.SetActive(false);
        eventData.OnBombDetonateEffect?.Invoke();
        EventStreams.Game.Publish(new BombCountdownEndEvent());
        StopAllCoroutines();
    }
    
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}