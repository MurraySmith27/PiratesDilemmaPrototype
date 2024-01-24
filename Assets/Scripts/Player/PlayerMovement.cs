using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(PlayerData), typeof(GoldController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_speed;
    
    private GoldController m_goldController;
    private PlayerInput m_playerInput;

    private InputAction m_moveAction;

    // Update is called once per frame
    void Update()
    {
        float speed = m_speed * ((100 - 2 * m_goldController.goldCarried) / 100f);
        Vector2 moveVector = m_moveAction.ReadValue<Vector2>().normalized * speed;
        transform.Translate(new Vector3(moveVector.x, 0f, moveVector.y));
    }

    void Awake()
    {
        m_goldController = GetComponent<GoldController>();
        m_playerInput = GetComponent<PlayerInput>();
        
        m_moveAction = m_playerInput.actions["Move"];
    }
    
    public void OnEnable()
    {
        m_moveAction.Enable();
    }

    public void OnDisable()
    {
        m_moveAction.Disable();
    }
}
