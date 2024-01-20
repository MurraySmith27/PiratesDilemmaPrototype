using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;

    [SerializeField] private List<InputAction> moveActionsPerPlayer;

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = moveActionsPerPlayer[GetComponent<PlayerData>().playerNum].ReadValue<Vector2>().normalized * speed;

        transform.Translate(new Vector3(moveVector.x, 0f, moveVector.y));

    }
    
    
    public void OnEnable()
    {
        foreach (InputAction x in moveActionsPerPlayer)
        {
            x.Enable();
        }
    }

    public void OnDisable()
    {
        foreach (InputAction x in moveActionsPerPlayer)
        {
            x.Disable();
        }
    }
}
