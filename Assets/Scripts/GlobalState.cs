using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    private static GlobalState _instance;
    public static GlobalState Instance
    {
        get { return _instance; }
    }

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private List<GameObject> playerSpawnPositions;
    

    public List<GameObject> players;

    public int numPlayers = 0;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
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
