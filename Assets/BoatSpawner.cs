using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    private int m_numBoat = 4;
    private List<GameObject> m_boats = new List<GameObject>();
    public GameObject[] m_boatSpawnLocations;

    // Spawn the boats with their randomized timeToLive and boatTotalCapacity
    void Start()
    {
        // if (!m_boatInLocation1){
        //     m_boat =  GameObject.Instantiate(Resources.Load("Shiptest_prefab"), 
        //         _boatSpawnLocation1, Quaternion.identity) as GameObject;
        //     m_boatTransform = m_boat.transform;
        //     m_boatLocation = 1;
        //     m_boatInLocation1 = true;
        // }
        // else if (!m_boatInLocation2){
        //     m_boat=  GameObject.Instantiate(Resources.Load("Shiptest_prefab"), 
        //         _boatSpawnLocation2, Quaternion.identity) as GameObject;
        //     m_boatTransform = m_boat.transform;
        //     m_boatLocation = 2;
        //     m_boatInLocation2 = true;
        // } 
        // else if (!m_boatInLocation3){
        //     m_boat=  GameObject.Instantiate(Resources.Load("Shiptest_prefab"), 
        //         _boatSpawnLocation3, Quaternion.identity) as GameObject;
        //     m_boatTransform = m_boat.transform;
        //     m_boatLocation = 3;
        //     m_boatInLocation3 = true;
        // } 
        // else if (!m_boatInLocation4){
        //     m_boat=  GameObject.Instantiate(Resources.Load("Shiptest_prefab"), 
        //         _boatSpawnLocation4, Quaternion.identity) as GameObject;
        //     m_boatTransform = m_boat.transform;
        //     m_boatLocation = 4;
        //     m_boatInLocation4 = true;
        // } 

        for (int i = 0; i < m_numBoat; i++){
            m_boats.Add(Instantiate(Resources.Load("Shiptest_prefab"), 
                 m_boatSpawnLocations[i].transform.position, Quaternion.identity) as GameObject);
            m_boats[i].GetComponent<BoatController>().timeToLive = Random.Range(20, 60);
            m_boats[i].GetComponent<BoatController>().boatTotalCapacity = Random.Range(30, 80);
            m_boats[i].GetComponent<BoatController>().LivingBoats[i] = 1;
            m_boats[i].GetComponent<BoatController>().boatSlot = i;
            m_boats[i].GetComponent<BoatController>().boatSpawner = this;
        }

        // m_boat1 =  GameObject.Instantiate(Resources.Load("Shiptest_prefab"), 
        //          m_boatSpawnLocation1, Quaternion.identity);
        // m_boat2 =  GameObject.Instantiate(Resources.Load("Shiptest_prefab"), 
        //          m_boatSpawnLocation2, Quaternion.identity);
        // m_boat3 =  GameObject.Instantiate(Resources.Load("Shiptest_prefab"), 
        //          m_boatSpawnLocation3, Quaternion.identity);
        // m_boat4 =  GameObject.Instantiate(Resources.Load("Shiptest_prefab"), 
        //          m_boatSpawnLocation4, Quaternion.identity);

        // m_boat1.GetComponent<BoatController>().timeToLive = Random.Range(20, 60);
        // m_boat1.GetComponent<BoatController>().boatTotalCapacity = Random.Range(40, 100);
        // m_boat2.GetComponent<BoatController>().timeToLive = Random.Range(20, 60);
        // m_boat2.GetComponent<BoatController>().boatTotalCapacity = Random.Range(40, 100);
        // m_boat3.GetComponent<BoatController>().timeToLive = Random.Range(20, 60);
        // m_boat3.GetComponent<BoatController>().boatTotalCapacity = Random.Range(40, 100);
        // m_boat4.GetComponent<BoatController>().timeToLive = Random.Range(20, 60);
        // m_boat4.GetComponent<BoatController>().boatTotalCapacity = Random.Range(40, 100);
    }

    public void RespawnBoat(int[] LivingBoats, int boatSlot)
    {
        for (int i = 0; i < m_numBoat; i++){
            if (LivingBoats[i] == 0){
                m_boats[i] = GameObject.Instantiate(Resources.Load("Shiptest_prefab"), 
                    m_boatSpawnLocations[i].transform.position, Quaternion.identity) as GameObject;
                m_boats[i].GetComponent<BoatController>().timeToLive = Random.Range(20, 60);
                m_boats[i].GetComponent<BoatController>().boatTotalCapacity = Random.Range(30, 80);
                m_boats[i].GetComponent<BoatController>().LivingBoats[i] = 1;
                m_boats[i].GetComponent<BoatController>().boatSlot = i;
                m_boats[i].GetComponent<BoatController>().boatSpawner = this;
            }
        }
    }
    
}
