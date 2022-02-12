using System;
using System.Linq;
using IngameStateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyState : MonoBehaviour, IState
{
    public void Initialize(StateMachine stateMachine)
    {
        
    }

    public void OnEnter()
    {
        SceneManager.LoadScene(GlobalConstants.LOBBY_SCENE_NAME);
    }

    public void OnExit()
    {
        
    }
    
    public void Dispose()
    {
        
    }
}