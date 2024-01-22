using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoldController : MonoBehaviour
{
    // Start is called before the first frame update
    
    //Game Objects
    private GameObject goldPile;
    private GameObject spawnedGold;

    public GameObject goldToSpawn;
    
    //Player Actions
    public List<InputAction> pickupGold;

    public int goldCarried = 0;
    void Start()
    {   //Finding GameObjects
        if (goldPile == null)
        {
            goldPile = GameObject.FindGameObjectWithTag("GoldPile");
        }
        
        //Assigning Callbacks

        for (int i = 0; i < pickupGold.Count; i++)
        {
            if (i == GetComponent<PlayerData>().playerNum)
            {
                pickupGold[i].performed += ctx =>
                { OnPickupGold(ctx); };
                pickupGold[i].performed += ctx => { OnDropGold(ctx); };
            }
        }
        
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPickupGold(InputAction.CallbackContext ctx)
    {
        if (goldCarried == 0)
        {

            //Check if close enough to gold pile
            if ((this.transform.position - goldPile.transform.position).magnitude < 50)
            {
                GetComponent<AudioSource>().Play();
                goldCarried += 15;

                SpawnGoldAsChild();

            }
        }
    }
    public void OnDropGold(InputAction.CallbackContext ctx)
    {
        if (goldCarried != 0)
        {
            GameObject boat = MaybeFindNearestBoat();
            if (boat)
            {
                Destroy(spawnedGold);
                boat.GetComponent<BoatController>().AddGold(goldCarried, GetComponent<PlayerData>().playerNum);
                goldCarried -= 15;
                
            }
        }
    }

    public GameObject MaybeFindNearestBoat()
    {
        GameObject nearestBoat = null;

        foreach (GameObject boat in GameObject.FindGameObjectsWithTag("Boat"))
        {
            if ((!nearestBoat && (this.transform.position - boat.transform.position).magnitude < 10) ||
                nearestBoat && 
                ((this.transform.position - boat.transform.position).magnitude < 
                 (this.transform.position - nearestBoat.transform.position).magnitude))
            {
                nearestBoat = boat;
            }
        }
        
        return nearestBoat;
    }
    
    void SpawnGoldAsChild()
    {
        // Instantiate objectToSpawn as a child of this.transform
        spawnedGold = Instantiate(goldToSpawn, this.transform.position, this.transform.rotation, this.transform);

        // Optionally, set the local position and rotation of the spawned object relative to the parent
        spawnedGold.transform.localPosition = Vector3.forward * 2; // Set to zero if you want it to be at the parent's position
        spawnedGold.transform.localRotation = Quaternion.identity; // Set to identity if you want it to have the parent's rotation
    }

    public void OnEnable()
    {
        foreach (InputAction x in pickupGold)
        {
            x.Enable();
        }
    }

    public void OnDisable()
    {
        foreach (InputAction x in pickupGold)
        {
            x.Disable();
        }
    }
}
