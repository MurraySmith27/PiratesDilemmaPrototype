using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyUILibrary;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateOverlays : MonoBehaviour
{
    private VisualElement root;

    private VisualTreeAsset timerAsset;

    public List<GameObject> boats;

    public List<VisualElement> boatTimers;

    private List<Coroutine> timerLabelCoroutines;

    void Start()
    {
        timerLabelCoroutines = new List<Coroutine>(new Coroutine[] {null, null, null, null});

        boatTimers = new List<VisualElement>(new VisualElement[] {null, null, null, null});

        boats = new List<GameObject>(new GameObject[] {null, null, null, null});
        
        BoatController.OnSpawnBoat += NewBoatSpawned;
        
        root = GetComponent<UIDocument>().rootVisualElement;

        timerAsset = Resources.Load<VisualTreeAsset>("UI/GameView/OverlayUIs/RadialProgressBar");

        for (int i = 0; i < 4; i++)
        {
            timerLabelCoroutines[i] = StartCoroutine(UpdateUIPositionOfBoatTimer(i));
        }
    }

    void BoatDeleted(int boatNum)
    {
        boatTimers[boatNum].Clear();
        boatTimers[boatNum].RemoveFromHierarchy();
        boatTimers[boatNum] = null;
        StopCoroutine(timerLabelCoroutines[boatNum]);
    }

    void NewBoatSpawned(int boatNum)
    {
        timerLabelCoroutines[boatNum] = StartCoroutine(UpdateUIPositionOfBoatTimer(boatNum));
    }

    IEnumerator UpdateUIPositionOfBoatTimer(int boatNum)
    {
        int initialTimeToLive = 0;
        yield return null;
        while (boats[boatNum] == null)
        {
            foreach (GameObject boat in GameObject.FindGameObjectsWithTag("Boat"))
            {
                if (boat.GetComponent<BoatController>().boatSlot == boatNum)
                {
                    boats[boatNum] = boat;
                }
            }
            
            if (boats[boatNum])
            {
                BoatController boatController = boats[boatNum].GetComponent<BoatController>();
                initialTimeToLive = boatController.timeToLive;
                boatController.OnDeleteBoat += BoatDeleted;
            }

            yield return null;
        }
        
        boatTimers[boatNum] = timerAsset.Instantiate();
        root.Add(boatTimers[boatNum]);

        RadialProgress timerElement = boatTimers[boatNum].Q<RadialProgress>("radial-timer");
        timerElement.maxTotalProgress = initialTimeToLive;

        while (true)
        {
            if (boatTimers[boatNum] != null)
            {
                Vector3 screen = Camera.main.WorldToScreenPoint(boats[boatNum].transform.position);
                boatTimers[boatNum].style.left =
                    screen.x - (boatTimers[boatNum].Q<RadialProgress>("radial-timer").layout.width / 2);
                boatTimers[boatNum].style.top = (Screen.height - screen.y);

                Debug.Log($"intitial time {initialTimeToLive}, current time: {boats[boatNum].GetComponent<BoatController>().timeToLive}");
                timerElement.progress = boats[boatNum].GetComponent<BoatController>().timeToLive;
            }
            yield return null;
        }
        
        
    }
}
