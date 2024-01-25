using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public delegate void OnPlayerJoin(int newPlayerNum);

//The purpose of this class to to store data that persists between scenes about players.
public class PlayerConfigData : MonoBehaviour
{
    private static PlayerConfigData _instance;
    public static PlayerConfigData Instance
    {
        get { return _instance; }
    }

    [SerializeField] private List<Transform> m_playerSpawnPositions = new List<Transform>(4);
    
    public List<Color> m_playerColors = new List<Color>(4);
    
    public int m_maxNumPlayers = 4;

    [HideInInspector]
    public int m_numPlayers = 0;

    private List<PlayerControlSchemes> m_playerControlSchemesList;
    
    [HideInInspector]
    public List<PlayerInput> m_playerInputObjects;

    public OnPlayerJoin m_onPlayerJoin;
    
    private void Awake()
    {
        if (PlayerConfigData._instance != null && PlayerConfigData._instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            PlayerConfigData._instance = this;
        }

        m_playerControlSchemesList = new List<PlayerControlSchemes>(m_maxNumPlayers);
        m_playerInputObjects = new List<PlayerInput>(m_maxNumPlayers);
        
        DontDestroyOnLoad(this.gameObject);    
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        int playerNum = PlayerConfigData.Instance.AddPlayer();
        if (playerNum == -1)
        {
            //at player limit. destroy.
            Destroy(this.gameObject);
        }

        playerInput.gameObject.transform.position = m_playerSpawnPositions[playerNum - 1].position;
        playerInput.gameObject.GetComponent<MeshRenderer>().material.color = m_playerColors[playerNum - 1];

        RegisterDeviceWithPlayer(playerNum, playerInput.devices[0]);
        m_playerInputObjects.Add(playerInput);
        
        DontDestroyOnLoad(playerInput.gameObject);
        m_onPlayerJoin(playerNum);
    }

    public int AddPlayer()
    {
        if (m_numPlayers < m_maxNumPlayers)
        {
            m_numPlayers++;
            
            m_playerControlSchemesList.Add(new PlayerControlSchemes());
                        
            return m_numPlayers;
        }
        else
        {
            return -1;
        }
    }

    public void RegisterDeviceWithPlayer(int playerNum, InputDevice device)
    {
        if (playerNum > m_numPlayers)
        {
            throw new ArgumentException($"Cannot change device for player number {playerNum}. " +
                                        $"There are only {m_numPlayers} players registered.");
        }

        m_playerControlSchemesList[playerNum - 1].devices = new[] { device };
    }

    public void DeregisterDeviceFromPlayer(int playerNum, InputDevice device)
    {
        if (playerNum > m_numPlayers)
        {
            throw new ArgumentException($"Cannot change device for player number {playerNum}. " +
                                        $"There are only {m_numPlayers} players registered.");
        }

        m_playerControlSchemesList[playerNum - 1].devices = null;
    }
}
