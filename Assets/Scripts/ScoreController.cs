using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreController : MonoBehaviour
{

    private static ScoreController _instance;
    public static ScoreController Instance
    {
        get { return _instance;  }
    }

    public List<int> playerScores;

    void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    void Start()
    {
        playerScores = new List<int>();
        for (int i = 0; i < GlobalState.Instance.numPlayers; i++)
        {
            playerScores.Add(0);
        }
    }

    public void UpdateScore()
    {
        
    }
}
