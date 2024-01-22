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
        timerLabelCoroutines = new List<Coroutine>();

        BoatController.OnSpawnBoat += NewBoatSpawned;
        
        root = GetComponent<UIDocument>().rootVisualElement;

        timerAsset = Resources.Load<VisualTreeAsset>("UI/GameView/OverlayUIs/RadialProgressBar");

        boatTimers = new List<VisualElement>();
            
        for (int i = 0; i < boats.Count; i++)
        {
            timerLabelCoroutines.Add(StartCoroutine(UpdateUIPositionOfBoatTimer(i)));
        }
    }

    void BoatDeleted(int boatNum)
    {
        boatTimers[boatNum].Clear();
        boatTimers[boatNum].RemoveFromHierarchy();
        boatTimers[boatNum] = null;
        StopCoroutine(timerLabelCoroutines[boatNum]);
    }

    void NewBoatSpawned(int boatNum, int boatInstanceId)
    {
        timerLabelCoroutines[boatNum] = StartCoroutine(UpdateUIPositionOfBoatTimer(boatNum));
    }

    IEnumerator UpdateUIPositionOfBoatTimer(int boatNum)
    {
        while (boats[boatNum] == null)
        {
            boats[boatNum] = GameObject.FindGameObjectsWithTag("Boat")
                .FirstOrDefault(obj => obj.GetComponent<BoatController>().boatSlot == boatNum);
            if (boats[boatNum])
            {
                BoatController boatController = boats[boatNum].GetComponent<BoatController>();
                boatController.OnDeleteBoat += BoatDeleted;
            }
            yield return null;
        }
        
        boatTimers[boatNum] = timerAsset.Instantiate();
        root.Add(boatTimers[boatNum]);

        while (true)
        {
            if (boatTimers[boatNum] != null)
            {
                Vector3 screen = Camera.main.WorldToScreenPoint(boats[boatNum].transform.position);
                boatTimers[boatNum].style.left =
                    screen.x - (boatTimers[boatNum].Q<RadialProgress>("radial-timer").layout.width / 2);
                boatTimers[boatNum].style.top = (Screen.height - screen.y);
            }
            yield return null;
        }
        
        
    }
}
