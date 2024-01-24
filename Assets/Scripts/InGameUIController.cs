using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUIController : MonoBehaviour
{
    // Start is called before the first frame update
    // public GameObject globalTimerObj;
    // public GameObject boatTimerObj;
    // public GameObject leaderBoardObj;

    private Label globalTimerLabel;
    private Label leaderBoardLabel;
    
    private Label toBeUpdatedBoatLabel;

    private Label[] playerScoreElements;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        globalTimerLabel = root.Q<Label>("GlobalTimer");
        leaderBoardLabel = root.Q<Label>("PlayerLeaderBoard");

        playerScoreElements = new Label[4];

        for (int i = 0; i < PlayerConfigData.Instance.m_numPlayers; i++)
        {
            playerScoreElements[i] = root.Q<Label>($"player-{i + 1}-score");
            playerScoreElements[i].text = $"P{i}: 0";
        }

        playerScoreElements[0].style.backgroundColor = Color.red;
        playerScoreElements[1].style.backgroundColor = Color.blue;
        playerScoreElements[2].style.backgroundColor = Color.green;
        playerScoreElements[3].style.backgroundColor = Color.yellow;
        
        
        leaderBoardLabel.text = "Scores:";

        StartCoroutine(GlobalCountdown(120));

        GameObject[] boats = GameObject.FindGameObjectsWithTag("Boat");

        ScoreController.Instance.OnScoreUpdate += UpdateScoreUI;
    }

    void UpdateScoreUI(List<int> newScores)
    {

        for (int i = 0; i < PlayerConfigData.Instance.m_numPlayers; i++)
        {
            playerScoreElements[i].text = $"P{i}: {newScores[i]}";
        }
    }

    IEnumerator GlobalCountdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            // Update the UI
            globalTimerLabel.text = "TIME REMAINING: " +  count.ToString();

            // Wait for one second
            yield return new WaitForSeconds(1);

            // Decrease the count
            count--;
        }

        // Countdown finished
        globalTimerLabel.text = "Time's up!";
    }
    
}
