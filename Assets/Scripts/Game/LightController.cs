using System;
using SimpleEventBus.Disposables;
using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    [SerializeField] 
    private Light _light;
    
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameOverLightChangeEvent>(SetLightColor),
        };
    }

    private void SetLightColor(GameOverLightChangeEvent eventData)
    {
        StartCoroutine(SetColor());
    }

    private IEnumerator SetColor(float countTime = 0.02f)
    {
        var lightColor = _light.color;
        float count = 0.003f;
        while (lightColor.g >= 0.6)
        {
            yield return new WaitForSeconds(countTime);
            lightColor.r = 1f;
            lightColor.g -= count;
            lightColor.b -= count;
            _light.color = lightColor;
        }
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}