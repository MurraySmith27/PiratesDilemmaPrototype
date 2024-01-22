using System.Collections;
using System.Collections.Generic;
using MyUILibrary;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateOverlays : MonoBehaviour
{
    private VisualElement root;

    public List<GameObject> boats;

    public List<VisualElement> boatTimers;

    void Start()
    {
        boats = new List<GameObject>(GameObject.FindGameObjectsWithTag("Boat"));
        
        root = GetComponent<UIDocument>().rootVisualElement;

        VisualTreeAsset timerAsset = Resources.Load<VisualTreeAsset>("UI/GameView/OverlayUIs/RadialProgressBar");

        boatTimers = new List<VisualElement>();
        for (int i = 0; i < boats.Count; i++)
        {
            boatTimers.Add(timerAsset.Instantiate());
            root.Add(boatTimers[i]);
        }
    }

    private void Update()
    {
        for (int i = 0; i < boats.Count; i++)
        {
            Vector3 screen = Camera.main.WorldToScreenPoint(boats[i].transform.position);
            Debug.Log(screen.x - (boatTimers[i].Q<RadialProgress>("radial-timer").layout.width / 2));
            boatTimers[i].style.left = screen.x - (boatTimers[i].Q<RadialProgress>("radial-timer").layout.width / 2);
            boatTimers[i].style.top = (Screen.height - screen.y);
        }
    }
}
