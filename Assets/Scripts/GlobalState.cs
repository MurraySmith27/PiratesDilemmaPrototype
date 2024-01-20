using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private List<GameObject> playerSpawnPositions;
    
    public static GlobalState Instance = null;

    public List<GameObject> players;

    public int numPlayers = 0;

    void Awake()
    {
        if (GlobalState.Instance == null)
        {
            Instance = this;
        }
        
        numPlayers = 0;

        for (int i = 0; i < 4; i++)
        {
            AddPlayer();
        }
    }


    public void AddPlayer()
    {
        int playerNum = numPlayers;
        GameObject newPlayer = Instantiate(playerPrefab, playerSpawnPositions[playerNum].transform.position, Quaternion.identity);

        newPlayer.GetComponent<PlayerData>().playerNum = playerNum;
        
        numPlayers = numPlayers + 1;
    }
}
