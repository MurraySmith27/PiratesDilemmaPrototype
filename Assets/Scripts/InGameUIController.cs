using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUIController : MonoBehaviour
{
    // Start is called before the first frame update
    private Label globalTimerLabel;
    private Label individualTimerLabel;
    private Label leaderBoardLabel;


    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        globalTimerLabel = root.Q<Label>("GlobalTimer");
        individualTimerLabel = root.Q<Label>("BoatTimer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
