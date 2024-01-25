
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class CharacterSelectUIController : MonoBehaviour
{

    [SerializeField] private UIDocument screenSpaceUIDoc;
    private VisualElement root;

    private VisualTreeAsset readyUpHover;

    private List<VisualElement> pressToJoinElements;
    private List<VisualElement> readyUpHoverElements;

    [SerializeField] private List<GameObject> renderTextureQuads;
    private List<UIDocument> docs;

    [SerializeField] private Sprite controllerReadyUpIcon;
    [SerializeField] private Sprite keyboardReadyUpIcon;

    private List<bool> readyPlayers = new List<bool>();
    
    void Awake()
    {
        root = screenSpaceUIDoc.rootVisualElement;
        docs = new List<UIDocument>();

        pressToJoinElements = new List<VisualElement>();
        readyUpHoverElements = new List<VisualElement>();
        readyUpHover = Resources.Load<VisualTreeAsset>("UI/CharacterSelect/ReadyUpHover");
    }

    void Start()
    {
        for (int i = 0; i < PlayerConfigData.Instance.m_maxNumPlayers; i++)
        {
            //resize the render texture quads to fit the screen properly
            Vector3 cameraPos = Camera.main.transform.position;
            float quadToCameraDistance =
                Vector3.Distance(renderTextureQuads[i].transform.position, cameraPos);
            float quadHeightScale = 2f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad) *
                                      quadToCameraDistance;
            float quadWidthScale = quadHeightScale * Camera.main.aspect / PlayerConfigData.Instance.m_maxNumPlayers;

            renderTextureQuads[i].transform.localScale = new Vector3(quadWidthScale, quadHeightScale, 1);
            Vector3 currentPos = renderTextureQuads[i].transform.position;
            renderTextureQuads[i].transform.position = new Vector3(currentPos.x, currentPos.y ,quadWidthScale * (i - 1.5f));
            
            docs.Add(renderTextureQuads[i].GetComponent<UIDocument>());
            pressToJoinElements.Add(docs[i].rootVisualElement);

            pressToJoinElements[i].Q<Label>("player-label").text = $"Player {i + 1}";
        }
        
        PlayerConfigData.Instance.m_onPlayerJoin += OnPlayerJoin;
    }

    void OnPlayerJoin(int newPlayerNum)
    {
        Debug.Log("on player join called");
        pressToJoinElements[newPlayerNum - 1].Q<Label>("press-to-join-label").text = "";

        VisualElement newReadyUpHover = readyUpHover.Instantiate();
        readyUpHoverElements.Add(newReadyUpHover);
        
        root.Add(newReadyUpHover);
        
        Vector3 screen = Camera.main.WorldToScreenPoint(renderTextureQuads[newPlayerNum - 1].transform.position);
        // newReadyUpHover.style.left =
        //     screen.x - (newReadyUpHover.layout.width / 2) + 50;
        // newReadyUpHover.style.top = (Screen.height - screen.y) + 50;

        PlayerInput input = PlayerConfigData.Instance.m_playerInputObjects[newPlayerNum - 1];

        Image image = new Image();
        if (PlayerConfigData.Instance.m_playerInputObjects[newPlayerNum - 1].devices[0] is Gamepad)
        {
            image.sprite = controllerReadyUpIcon;
        }
        else
        {
            image.sprite = controllerReadyUpIcon;
        }
        
        newReadyUpHover.Q<VisualElement>("button-icon").Add(image);

        input.actions["Interact"].performed += ctx => { PlayerReadyUpToggle(newPlayerNum); };
    }

    private void PlayerReadyUpToggle(int playerNum)
    {
        
    }
    
    
}
