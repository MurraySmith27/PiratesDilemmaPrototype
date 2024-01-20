using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private List<GameObject> playerSpawnPositions;
    
    public static GlobalState Instance;

    public List<GameObject> players;

    public int numPlayers;

    void Awake()
    {
        if (!GlobalState.Instance)
        {
            GlobalState.Instance = this;
        }    
    }


    public void AddPlayer()
    {
        int playerNum = numPlayers + 1;
        GameObject newPlayer = Instantiate(playerPrefab, playerSpawnPositions[playerNum].transform.position, Quaternion.identity);

        newPlayer.GetComponent<PlayerData>().playerNum = playerNum;
        
        numPlayers++;
    }
}
