using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int[] m_playerCapacity = {0, 0, 0, 0 };

    public float timeToLive;

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
        Debug.Log($"adding {goldCapacity} gold to player {playerNumber} on boat {boatSlot}");
        boatCurrentCapacity += goldCapacity;
        if (boatCurrentCapacity > boatTotalCapacity){
            StopCoroutine(coroutine);
            StartCoroutine(SinkBoat());
        }
        m_playerCapacity[playerNumber] += goldCapacity;
    }


    IEnumerator SinkBoat()
    {
        var c = StartCoroutine(SinkAnimation());

        yield return new WaitForSeconds(BoatRespawnTime);
        StopCoroutine(coroutine);
        boatSpawner.RespawnBoat(boatSlot);
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
        var c = StartCoroutine(SailBoatAnimation());

        yield return new WaitForSeconds(BoatRespawnTime);
        StopCoroutine(coroutine);
        boatSpawner.RespawnBoat(boatSlot);
        Destroy(this.gameObject);

        for (int i = 0; i < 4; i++)
        {
            ScoreController.Instance.playerScores[i] += m_playerCapacity[i];
        }
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
