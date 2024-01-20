using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoldController : MonoBehaviour
{
    // Start is called before the first frame update
    
    //Game Objects
    public GameObject goldPile;
    public GameObject[] boats;
    
    //Player Actions
    public InputAction pickupGold;
    public InputAction dropGold;

    public int goldCarried = 0;
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
        pickupGold.performed += ctx => { OnPickupGold(ctx); };
        dropGold.performed += ctx => { OnDropGold(ctx); };
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPickupGold(InputAction.CallbackContext ctx)
    {
        //Check if close enough to gold pile
        if ((this.transform.position - goldPile.transform.position).magnitude < 10)
        {
            goldCarried += 5;
            
        }
    }
    public void OnDropGold(InputAction.CallbackContext ctx)
    {
        GameObject boat = MaybeFindNearestBoat();
        if (boat)
        {
            boat.GetComponent<BoatController>().AddGold(goldCarried, GetComponent<PlayerData>().playerNum);
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
}
