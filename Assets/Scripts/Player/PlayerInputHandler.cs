using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : PlayerInput
{
    //called when a player pressed a button on a controller.
    public void OnPlayerJoined()
    {
        Debug.Log("player joined!");
    }
}
