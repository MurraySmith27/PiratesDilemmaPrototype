using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreController : MonoBehaviour
{

    public GlobalState globalState;

    public static ScoreController Instance = null;

    public List<int> playerScores;

    void Awake()
    {

        if (ScoreController.Instance == null)
        {
            Instance = this;
        }
        
        playerScores = new List<int>();
        Debug.Log($"global state instance: {GlobalState.Instance}");
        for (int i = 0; i < globalState.numPlayers; i++)
        {
            playerScores.Add(0);
        }
    }

    public void UpdateScore()
    {
        
    }
}
