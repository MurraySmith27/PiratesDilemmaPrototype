
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSelectUIController : MonoBehaviour
{
    
    private VisualElement root;

    private VisualTreeAsset pressToJoinScreen;

    private List<VisualElement> pressToJoinElements;
    
    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        // pressToJoinScreen = Resources.Load("UI/CharacterSelect/InputToJoinHover");
    }

    void Start()
    {
        // root.Add(presstoJoinScree);
    }
}
