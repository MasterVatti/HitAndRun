using UnityEngine;
using SimpleEventBus.Disposables;
using System.Collections;

public class EndLevelPointController : MonoBehaviour
{
    private CompositeDisposable _subscriptions;
    private GameObject _currentCharacter;
    private bool _isCharacterWin;

    private void OnTriggerEnter(Collider other)
    {
        if (_isCharacterWin)
        {
            return;
        }
        if (other.gameObject.tag == GlobalConstants.CAHARACTER_TAG)
        {
            _isCharacterWin = true;
            StartCoroutine(GoGameWinState());
        }
    }

    private IEnumerator GoGameWinState()
    {
        EventStreams.Game.Publish(new CharacterStateEvent(true));
        yield return new WaitForSeconds(3f);
        EventStreams.Game.Publish(new GameWinEvent());
    }
}