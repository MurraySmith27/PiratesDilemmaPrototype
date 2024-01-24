using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float default_speed = 0.5f;
    private float speed;
    [SerializeField] private List<InputAction> moveActionsPerPlayer;
    GoldController goldController;

    private PlayerData playerData;
    // Update is called once per frame
    void Update()
    {
        speed = default_speed * ((100 - 2 * goldController.goldCarried) / 100f);
        Vector2 moveVector = moveActionsPerPlayer[playerData.playerNum].ReadValue<Vector2>().normalized * speed;

        transform.Translate(new Vector3(moveVector.x, 0f, moveVector.y));

    }

    void Start()
    {
        goldController = GetComponent<GoldController>();
        playerData = GetComponent<PlayerData>();
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
