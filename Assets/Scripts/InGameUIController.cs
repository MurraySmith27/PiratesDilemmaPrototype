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
    private Label boatTimerTitleLabel;
    private Label leaderBoardLabel;

    private List<Label> boatTimers;
    
    private Label toBeUpdatedBoatLabel;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        globalTimerLabel = root.Q<Label>("GlobalTimer");
        boatTimerTitleLabel = root.Q<Label>("BoatTimerTitle");
        leaderBoardLabel = root.Q<Label>("PlayerLeaderBoard");
        
        boatTimers = new List<Label>();
        boatTimers.Add(root.Q<Label>("BoatTimer1"));
        boatTimers.Add(root.Q<Label>("BoatTimer2"));
        boatTimers.Add(root.Q<Label>("BoatTimer3"));
        boatTimers.Add(root.Q<Label>("BoatTimer4"));
        

        boatTimerTitleLabel.text = "BOAT TIMERS:";
        leaderBoardLabel.text = "LEADERBOARD";

        StartCoroutine(GlobalCountdown(120));

        GameObject[] boats = GameObject.FindGameObjectsWithTag("Boat");
        
        Debug.Log($"num players: {GlobalState.Instance.numPlayers}");
        for (int i = 0; i < GlobalState.Instance.numPlayers; i++)
        {
            int countDown = 0;
            for (int j = 0; j < GlobalState.Instance.numPlayers; j++)
            {
                BoatController boatController = boats[j].GetComponent<BoatController>();
                if (boatController.boatSlot == i)
                {
                    countDown = (int)boatController.timeToLive;
                }
            }
            StartCoroutine(BoatCountDown(i, countDown));
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
    
    IEnumerator BoatCountDown(int boatNum, int initialCount)
    {
        int count = initialCount;

        while (count > 0)
        {
            // Update the UI
            boatTimers[boatNum].text = $"Boat {boatNum}" + ": " +  count.ToString();

            // Wait for one second
            yield return new WaitForSeconds(1);

            // Decrease the count
            count--;
        }

        // Countdown finished
        boatTimers[boatNum].text = "Time's up!";
    }
}
