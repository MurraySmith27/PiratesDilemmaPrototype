using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoldController : MonoBehaviour
{
    // Start is called before the first frame update
    
    //Game Objects
    private GameObject goldPile;
    private GameObject[] boats;

    public GameObject goldToSpawn;
    
    //Player Actions
    public List<InputAction> pickupGold;

    private int goldCarried = 0;
    void Start()
    {   //Finding GameObjects
        if (goldPile == null)
        {
            goldPile = GameObject.FindGameObjectWithTag("GoldPile");
        }

        if (boats == null)
        {
            boats = GameObject.FindGameObjectsWithTag("Boat");
        }
        
        //Assigning Callbacks

        for (int i = 0; i < pickupGold.Count; i++)
        {
            pickupGold[i].performed += ctx => { OnPickupGold(ctx); };
            pickupGold[i].performed += ctx => { OnDropGold(ctx); };
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
            if ((this.transform.position - goldPile.transform.position).magnitude < 10)
            {
                goldCarried += 5;

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
                //boat.GetComponent<BoatController>().AddGold(goldCarried, GetComponent<PlayerData>().playerNum);
                goldCarried -= 5;
            }
        }
    }

    public GameObject MaybeFindNearestBoat()
    {
        GameObject nearestBoat = null;

        foreach (GameObject boat in boats)
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
        GameObject spawnedObject = Instantiate(goldToSpawn, this.transform.position, this.transform.rotation, this.transform);

        // Optionally, set the local position and rotation of the spawned object relative to the parent
        spawnedObject.transform.localPosition = Vector3.forward * 2; // Set to zero if you want it to be at the parent's position
        spawnedObject.transform.localRotation = Quaternion.identity; // Set to identity if you want it to have the parent's rotation
    }
}
