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

    private Label boatTimer1;
        private Label boatTimer2;
            private Label boatTimer3;
                private Label boatTimer4;

    private Label toBeUpdatedBoatLabel;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        globalTimerLabel = root.Q<Label>("GlobalTimer");
        boatTimerTitleLabel = root.Q<Label>("BoatTimerTitle");
        leaderBoardLabel = root.Q<Label>("PlayerLeaderBoard");

        boatTimer1 = root.Q<Label>("BoatTimer1");
        boatTimer2 = root.Q<Label>("BoatTimer2");
        boatTimer3 = root.Q<Label>("BoatTimer3");
        boatTimer4 = root.Q<Label>("BoatTimer4");

        boatTimerTitleLabel.text = "BOAT TIMERS:";
        leaderBoardLabel.text = "LEADERBOARD";

        StartCoroutine(GlobalCountdown(120));

        StartCoroutine(Boat1CountDown());

        StartCoroutine(Boat2CountDown());

        StartCoroutine(Boat3CountDown());

        StartCoroutine(Boat4CountDown());
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
    
    IEnumerator Boat1CountDown()
    {
        int count = 30;

        while (count > 0)
        {
            // Update the UI
            boatTimer1.text = "Boat 1" + ": " +  count.ToString();

            // Wait for one second
            yield return new WaitForSeconds(1);

            // Decrease the count
            count--;
        }

        // Countdown finished
        boatTimer1.text = "Time's up!";
    }

    IEnumerator Boat2CountDown()
    {
        int count = 30;

        while (count > 0)
        {
            // Update the UI
            boatTimer2.text = "Boat 2"+ ": " +  count.ToString();

            // Wait for one second
            yield return new WaitForSeconds(1);

            // Decrease the count
            count--;
        }

        // Countdown finished
        boatTimer2.text = "Time's up!";
    }

    IEnumerator Boat3CountDown()
    {
        int count = 30;

        while (count > 0)
        {
            // Update the UI
            boatTimer3.text = "Boat 3"+ ": " +  count.ToString();

            // Wait for one second
            yield return new WaitForSeconds(1);

            // Decrease the count
            count--;
        }

        // Countdown finished
        boatTimer3.text = "Time's up!";
    }

    IEnumerator Boat4CountDown()
    {
        int count = 30;

        while (count > 0)
        {
            // Update the UI
            boatTimer4.text = "Boat 4"+ ": " +  count.ToString();

            // Wait for one second
            yield return new WaitForSeconds(1);

            // Decrease the count
            count--;
        }

        // Countdown finished
        boatTimer4.text = "Time's up!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
