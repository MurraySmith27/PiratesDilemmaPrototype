using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyUILibrary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateOverlays : MonoBehaviour
{
    private VisualElement root;

    private VisualTreeAsset boatUIAsset;

    private VisualTreeAsset playerUIAsset;

    private List<GameObject> boats;

    private List<VisualElement> boatElements;

    private List<VisualElement> playerElements;

    private List<Coroutine> timerLabelCoroutines;

    private List<string> currentBoatLabels;

    private List<string> currentPlayerLabels;

    private Coroutine playerLabelCoroutine;

    void Start()
    {
        timerLabelCoroutines = new List<Coroutine>(new Coroutine[] {null, null, null, null});

        boatElements = new List<VisualElement>(new VisualElement[] {null, null, null, null});
        
        playerElements = new List<VisualElement>(new VisualElement[] {null, null, null, null});

        boats = new List<GameObject>(new GameObject[] {null, null, null, null});

        currentBoatLabels = new List<string>(new string[] {"", "", "", ""});

        currentPlayerLabels = new List<string>(new string[] { "", "", "", "" });
        
        BoatController.OnSpawnBoat += NewBoatSpawned;
        BoatController.OnAddGold += GoldAddedToBoat;
        
        root = GetComponent<UIDocument>().rootVisualElement;

        boatUIAsset = Resources.Load<VisualTreeAsset>("UI/GameView/OverlayUIs/BoatHoverUI");

        playerUIAsset = Resources.Load<VisualTreeAsset>("UI/GameView/OverlayUIs/PlayerHoverUI");

        for (int i = 0; i < 4; i++)
        {
            timerLabelCoroutines[i] = StartCoroutine(UpdateBoatUI(i));
        }

        playerLabelCoroutine = StartCoroutine(UpdatePlayerUIs());
    }

    void BoatDeleted(int boatNum)
    {
        boatElements[boatNum].Clear();
        boatElements[boatNum].RemoveFromHierarchy();
        boatElements[boatNum] = null;
        StopCoroutine(timerLabelCoroutines[boatNum]);
    }

    void NewBoatSpawned(int boatNum)
    {
        timerLabelCoroutines[boatNum] = StartCoroutine(UpdateBoatUI(boatNum));
    }

    void GoldAddedToBoat(int boatNum, int goldTotal, int capacity)
    {
        currentBoatLabels[boatNum] = $"{goldTotal} / {capacity}";
    }

    IEnumerator UpdateBoatUI(int boatNum)
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
                currentBoatLabels[boatNum] = $"0/{boatController.boatTotalCapacity}";
            }

            yield return null;
        }
        
        boatElements[boatNum] = boatUIAsset.Instantiate();
        root.Add(boatElements[boatNum]);

        RadialProgress timerElement = boatElements[boatNum].Q<RadialProgress>("radial-timer");
        timerElement.maxTotalProgress = initialTimeToLive;

        Label capacityLabel = boatElements[boatNum].Q<Label>("capacity-label");

        while (true)
        {
            if (boatElements[boatNum] != null)
            {
                Vector3 screen = Camera.main.WorldToScreenPoint(boats[boatNum].transform.position);
                boatElements[boatNum].style.left =
                    screen.x;// - (boatElements[boatNum].Q<RadialProgress>("radial-timer").layout.width / 2);
                boatElements[boatNum].style.top = (Screen.height - screen.y);

                BoatController boatController = boats[boatNum].GetComponent<BoatController>();
                timerElement.progress = boatController.timeToLive;

                capacityLabel.text = currentBoatLabels[boatNum];
                if (boatNum == 0)
                {
                    Debug.Log($"point on screen: {screen}, screen size: {Screen.width}, {Screen.height}");
                    Debug.Log($"root dimensions: {root.resolvedStyle.width}, {root.resolvedStyle.height}");
                    Debug.Log($"unresolved position of hovering label: {boatElements[boatNum].style.left}, {boatElements[boatNum].style.top}");
                    Debug.Log($"end position of hovering label: {boatElements[boatNum].resolvedStyle.left}, {boatElements[boatNum].resolvedStyle.top}");
                }
            }
            yield return null;
        }
    }

    IEnumerator UpdatePlayerUIs()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        GoldController[] goldControllers = players.Select(obj => { return obj.GetComponent<GoldController>(); }).ToArray();

        Label[] playerUILabels = new Label[4];

        for (int i = 0; i < GlobalState.Instance.numPlayers; i++)
        {
            playerElements[i] = playerUIAsset.Instantiate();
            root.Add(playerElements[i]);

            playerUILabels[i] = playerElements[i].Q<Label>("gold-count");
            currentPlayerLabels[i] = "0";
        }


        while (true)
        {
            for (int i = 0; i < GlobalState.Instance.numPlayers; i++)
            {
                Vector3 screen = Camera.main.WorldToScreenPoint(players[i].transform.position);

                playerElements[i].style.left =
                    screen.x - (playerUILabels[i].layout.width / 2);
                playerElements[i].style.top = (Screen.height - screen.y) - 100;

                currentPlayerLabels[i] = $"{goldControllers[i].goldCarried}";
                playerUILabels[i].text = currentPlayerLabels[i];
            }

            yield return null;
        }
        
    } 
}
