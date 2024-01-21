using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    private int m_numBoat = 4;
    private List<GameObject> m_boats = new List<GameObject>();
    public GameObject[] m_boatSpawnLocations;

    // Spawn the boats with their randomized timeToLive and boatTotalCapacity
    void Awake()
    {

        for (int i = 0; i < m_numBoat; i++){
            m_boats.Add(Instantiate(Resources.Load("Shiptest_prefab"), 
                 m_boatSpawnLocations[i].transform.position, Quaternion.identity) as GameObject);
            m_boats[i].GetComponent<BoatController>().timeToLive = Random.Range(4, 15);
            m_boats[i].GetComponent<BoatController>().boatTotalCapacity = Random.Range(30, 80);
            m_boats[i].GetComponent<BoatController>().boatSlot = i;
            m_boats[i].GetComponent<BoatController>().boatSpawner = this;
        }
    }

    public void RespawnBoat(int boatSlot)
    {
            m_boats[boatSlot] = GameObject.Instantiate(Resources.Load("Shiptest_prefab"), 
                m_boatSpawnLocations[boatSlot].transform.position, Quaternion.identity) as GameObject;
            m_boats[boatSlot].GetComponent<BoatController>().timeToLive = Random.Range(20, 60);
            m_boats[boatSlot].GetComponent<BoatController>().boatTotalCapacity = Random.Range(5, 10);
            m_boats[boatSlot].GetComponent<BoatController>().boatSlot = boatSlot;
            m_boats[boatSlot].GetComponent<BoatController>().boatSpawner = this;
    }
    
}
