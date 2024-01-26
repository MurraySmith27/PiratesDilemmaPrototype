using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;


public delegate void OnGameStart();
public class GameStartHandler : MonoBehaviour
{

    private static GameStartHandler m_instance;

    public static GameStartHandler Instance
    {
        get
        {
            return m_instance;
        }
    }
    
    public OnGameStart onGameStart;
    
    void Start()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(m_instance.gameObject);
        }
        else
        {
            m_instance = this;
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < PlayerConfigData.Instance.m_numPlayers; i++)
        {
            GameObject player = PlayerConfigData.Instance.m_playerInputObjects[i].gameObject;

            player.GetComponent<PlayerData>().m_playerNum = i;
        }
        
        onGameStart();
    }
}
