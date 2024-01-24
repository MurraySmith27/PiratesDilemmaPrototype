using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//The purpose of this class to to store data that persists between scenes about players.
public class PlayerConfigData : MonoBehaviour
{
    private static PlayerConfigData _instance;
    public static PlayerConfigData Instance
    {
        get { return _instance; }
    }

    [SerializeField] private List<Transform> m_playerSpawnPositions = new List<Transform>(4);

    [SerializeField] private List<Color> m_playerColors = new List<Color>(4);
    
    [SerializeField]
    private int m_maxNumPlayers = 4;

    [HideInInspector]
    public int m_numPlayers = 0;

    private List<PlayerControlSchemes> m_playerControlSchemesList;
    
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

        m_playerControlSchemesList = new List<PlayerControlSchemes>();
        
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
        
        DontDestroyOnLoad(playerInput.gameObject);
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
