using UnityEngine;

public class RestartLevelButton : MonoBehaviour
{
    public void RestartLevel()
    {
        EventStreams.Game.Publish(new StartGameEvent());
    }
}