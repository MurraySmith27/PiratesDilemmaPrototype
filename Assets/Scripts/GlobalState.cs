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
            AddPlayer(i);
        }
    }


    public void AddPlayer(int i)
    {
        int playerNum = numPlayers;
        GameObject newPlayer = Instantiate(playerPrefab, playerSpawnPositions[playerNum].transform.position, Quaternion.identity);

        newPlayer.GetComponent<PlayerData>().playerNum = playerNum;
        newPlayer.GetComponent<MeshRenderer>().material.color = GetPlayerColor(i);
        numPlayers = numPlayers + 1;
    }
    Color GetPlayerColor(int playerNumber)
    {
        Color[] colors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow}; // Add more colors as needed.
        // Use the modulo operator to cycle through colors if there are more players than colors.
        return colors[playerNumber % colors.Length];
    }
}
