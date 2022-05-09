using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager _instance;
    public enum GameState { MENU, STORY, GAME, PAUSE, ENDGAME };

    public GameState gameState { get; private set; }
    public int minute;
    public float seconds;
    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
        }

        return _instance;
    }
    private GameManager()
    {
     
        minute = 5;
        seconds = 0;
        gameState = GameState.MENU;
    }

    private void Reset()
    {
       
        minute = 2;
        seconds = 0;
    }

    public void ChangeState(GameState nextState)
    {
        if ((gameState == GameState.ENDGAME || gameState == GameState.MENU) && nextState == GameState.GAME)
            Reset();
        gameState = nextState;
        changeStateDelegate();
    }
}
