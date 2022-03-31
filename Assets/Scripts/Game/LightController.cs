using SimpleEventBus.Disposables;
using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    [SerializeField] 
    private Light _light;
    
    private CompositeDisposable _subscriptions;
    private float[] _gameStateColor = {0f, 0.0025f, 0.0025f, 0f};

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CharacterStateEvent>(SetLightColor),
        };
    }

    private void SetLightColor(CharacterStateEvent eventData)
    {
        if (eventData.IsCharacterWin)
        {
            _gameStateColor[0] = 0.0025f;
            _gameStateColor[1] = 0f;
        }
        StartCoroutine(SetColor());
    }

    private IEnumerator SetColor(float countTime = 0.01f)
    {
        var lightColor = _light.color;
        while (lightColor.g >= 0.7)
        {
            yield return new WaitForSeconds(countTime);
            lightColor.r -= _gameStateColor[0];
            lightColor.g -= _gameStateColor[1];
            lightColor.b -= _gameStateColor[2];
            _light.color = lightColor;
        }
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}