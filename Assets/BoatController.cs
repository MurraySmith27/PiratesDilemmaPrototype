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
    public int[] LivingBoats = {0, 0, 0, 0};

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
    }

    // private void CalculateCapacity()
    // {
    //     if (boatCurrentCapacity > boatTotalCapacity){
    //         SinkBoat();
    //     }
    // }

    IEnumerator SinkBoat()
    {
        var c = StartCoroutine(SinkAnimation());

        yield return new WaitForSeconds(BoatRespawnTime);
        StopCoroutine(coroutine);
        boatSpawner.RespawnBoat(LivingBoats, boatSlot);
        Destroy(this);
        LivingBoats[boatSlot] = 0;
    }

    IEnumerator SinkAnimation()
    {
        Vector3 destination = transform.position - new Vector3(0, BoatRespawnHeight, 0);
        
        while (true)
        {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator SailBoat()
    {
        var c = StartCoroutine(SailBoatAnimation());

        yield return new WaitForSeconds(BoatRespawnTime);
        StopCoroutine(coroutine);
        boatSpawner.RespawnBoat(LivingBoats, boatSlot);
        Destroy(this);
        LivingBoats[boatSlot] = 0;

        for (int i = 0; i < 4; i++)
        {
            ScoreController.Instance.playerScores[i] += m_playerCapacity[i];
        }
    }

    IEnumerator SailBoatAnimation()
    {
        Vector3 destination = transform.position - new Vector3(BoatRespawnLength, 0, 0);
        
        while (true)
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
            yield return null;
        }
    }
}
