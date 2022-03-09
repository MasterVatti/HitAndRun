using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public void StartGame()
    {
        EventStreams.Game.Publish(new StartGameEvent());
    }
}