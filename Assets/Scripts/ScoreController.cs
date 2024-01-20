using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreController : MonoBehaviour
{

    public UIDocument doc;
    
    public static ScoreController Instance;

    public List<int> playerScores;

    void Awake()
    {

        var label =doc.rootVisualElement.Q<Label>("main-text");

        label.text = "blah";
        if (!ScoreController.Instance)
        {
            ScoreController.Instance = this;
        }
        
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
