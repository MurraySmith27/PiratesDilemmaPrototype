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

        for (int i = 0; i < m_numBoat; i++) {
            m_boats.Add(Instantiate(Resources.Load("Shiptest_prefab"), 
                 m_boatSpawnLocations[i].transform.position, Quaternion.identity) as GameObject);
            BoatController boatController = m_boats[i].GetComponent<BoatController>();
            boatController.timeToLive = new System.Random().Next(20, 30);
            boatController.boatTotalCapacity = Random.Range(60, 80);
            boatController.boatSlot = i;
            boatController.boatSpawner = this;
            
        }
    }

    public int RespawnBoat(int boatSlot)
    {
            m_boats[boatSlot] = GameObject.Instantiate(Resources.Load("Shiptest_prefab"), 
                m_boatSpawnLocations[boatSlot].transform.position, Quaternion.identity) as GameObject;
            BoatController boatController = m_boats[boatSlot].GetComponent<BoatController>();
            boatController.timeToLive = new System.Random().Next(20, 30);
            boatController.boatTotalCapacity = Random.Range(60, 80);
            boatController.boatSlot = boatSlot;
            boatController.boatSpawner = this;
            return m_boats[boatSlot].GetInstanceID();
    }
    
}
