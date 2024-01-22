using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public delegate void DeleteBoatCallback(int boatNum);

public delegate void SpawnBoatCallback(int boatNum);

public delegate void GoldAddedCallback(int boatNum, int goldTotal, int capacity);

public class BoatController : MonoBehaviour
{

    public BoatSpawner boatSpawner;

    private Coroutine coroutine;
    
    // private Transform m_boatTransform;
    private const float BoatRespawnHeight = 3f;
    private const float BoatRespawnLength = -10f;
    private const float BoatLeaveTimeSeconds = 30;
    private const float BoatRespawnTime = 5f;
    public int boatTotalCapacity;
    public int boatCurrentCapacity;
    public int[] m_playerCapacity = {0, 0, 0, 0 };

    public DeleteBoatCallback OnDeleteBoat;
    public static SpawnBoatCallback OnSpawnBoat;
    public static GoldAddedCallback OnAddGold;
    
    public int timeToLive;

    public int boatSlot;


    void Start() 
    {
        coroutine = StartCoroutine(CountDownTimer());
    }

    IEnumerator CountDownTimer() 
    {

        while (timeToLive > 0) {
            timeToLive--;
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(SailBoat());
    }

    public void AddGold(int goldCapacity, int playerNumber)
    {
        boatCurrentCapacity += goldCapacity;
        if (boatCurrentCapacity > boatTotalCapacity){
            StopCoroutine(coroutine);
            StartCoroutine(SinkBoat());
        }
        m_playerCapacity[playerNumber] += goldCapacity;
        
        OnAddGold(boatSlot, m_playerCapacity.Sum(), boatTotalCapacity);
    }


    IEnumerator SinkBoat()
    {
        OnDeleteBoat(boatSlot);
        var c = StartCoroutine(SinkAnimation());

        yield return new WaitForSeconds(BoatRespawnTime);
        StopCoroutine(coroutine);
        
        
        int boatInstanceId = boatSpawner.RespawnBoat(boatSlot);
        OnSpawnBoat(boatSlot);
        
        Destroy(this.gameObject);
    }

    IEnumerator SinkAnimation()
    {
        GetComponents<AudioSource>()[0].Play();
        Vector3 destination = transform.position - new Vector3(0, BoatRespawnHeight, 0);
        
        while (true)
        {
            transform.Translate(new Vector3(0, -50, 0) * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator SailBoat()
    {
        OnDeleteBoat(boatSlot);
        var c = StartCoroutine(SailBoatAnimation());

        yield return new WaitForSeconds(BoatRespawnTime);
        StopCoroutine(coroutine);
        
        
        int boatInstanceId = boatSpawner.RespawnBoat(boatSlot);
        OnSpawnBoat(boatSlot);
        
        for (int i = 0; i < GlobalState.Instance.numPlayers; i++)
        {
            ScoreController.Instance.playerScores[i] += m_playerCapacity[i];
        }
        
        Destroy(this.gameObject);
    }

    IEnumerator SailBoatAnimation()
    {
        GetComponents<AudioSource>()[1].Play();
        Vector3 destination = transform.position - new Vector3(BoatRespawnLength, 0, 0);
        
        while (true)
        {
            transform.Translate(new Vector3(-10, 0, 0) * Time.deltaTime);
            yield return null;
        }
    }
}
