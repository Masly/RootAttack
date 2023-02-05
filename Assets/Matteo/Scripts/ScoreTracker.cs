using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [System.Serializable]
    public class PlayerScoreVar
    {
        public Player.PlayerID targetPlayer;
        public GameEvent increaseScoreEvent;
        public int score = 0;
        public void IncreaseScoreCallback()
        {
            score++;
        }
    }

    public PlayerScoreVar player1ScoreVar;
    public PlayerScoreVar player2ScoreVar;

    public GameEvent resetScoreEvent;

    public void ResetScoreCallback()
    {
        player1ScoreVar.score = 0;
        player2ScoreVar.score = 0;
    }

    void OnEnable()
    {
        resetScoreEvent.RegisterListener(ResetScoreCallback);
        player1ScoreVar.increaseScoreEvent.RegisterListener(player1ScoreVar.IncreaseScoreCallback);
        player2ScoreVar.increaseScoreEvent.RegisterListener(player2ScoreVar.IncreaseScoreCallback);
    }

    void OnDisable()
    {
        resetScoreEvent.UnregisterListener(ResetScoreCallback);
        player1ScoreVar.increaseScoreEvent.UnregisterListener(player1ScoreVar.IncreaseScoreCallback);
        player2ScoreVar.increaseScoreEvent.UnregisterListener(player2ScoreVar.IncreaseScoreCallback);
    }





}
