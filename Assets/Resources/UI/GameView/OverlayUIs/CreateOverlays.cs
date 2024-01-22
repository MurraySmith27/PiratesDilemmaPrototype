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

    void Start()
    {
        boats = new List<GameObject>(GameObject.FindGameObjectsWithTag("Boat"));

        BoatController.OnSpawnBoat += NewBoatSpawned;
        foreach (GameObject boat in boats)
        {
            BoatController boatController = boat.GetComponent<BoatController>();
            boatController.OnDeleteBoat += BoatDeleted;
        }
        
        root = GetComponent<UIDocument>().rootVisualElement;

        timerAsset = Resources.Load<VisualTreeAsset>("UI/GameView/OverlayUIs/RadialProgressBar");

        boatTimers = new List<VisualElement>();
        for (int i = 0; i < boats.Count; i++)
        {
            boatTimers.Add(timerAsset.Instantiate());
            root.Add(boatTimers[i]);
        }
    }

    void BoatDeleted(int boatNum)
    {
        boatTimers[boatNum].Clear();
        boatTimers[boatNum].RemoveFromHierarchy();
        boatTimers[boatNum] = null;
    }

    void NewBoatSpawned(int boatNum)
    {
        
        boats[boatNum] = GameObject.FindGameObjectsWithTag("Boat")
            .FirstOrDefault(
                obj => obj.GetComponent<BoatController>().boatSlot == boatNum
            );
        
        Debug.Log($"boat: {boats[boatNum]}");

        boatTimers[boatNum] = timerAsset.Instantiate();
        root.Add(boatTimers[boatNum]);

        BoatController boatController = boats[boatNum].GetComponent<BoatController>();
        boatController.OnDeleteBoat += BoatDeleted;
    }

    private void Update()
    {
        for (int i = 0; i < boats.Count; i++)
        {
            if (boatTimers[i] != null)
            {
                Vector3 screen = Camera.main.WorldToScreenPoint(boats[i].transform.position);
                boatTimers[i].style.left =
                    screen.x - (boatTimers[i].Q<RadialProgress>("radial-timer").layout.width / 2);
                boatTimers[i].style.top = (Screen.height - screen.y);
            }
        }
    }
}
