using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(PlayerData), typeof(GoldController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_speed;

    private GoldController m_goldController;
    private PlayerInput m_playerInput;

    private InputAction m_moveAction;

    private bool m_initialized = false;

    // For dashing
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashDuration;
    private InputAction m_dashAction;
    private bool isDashing;

    void Update()
    {
        if (m_initialized)
        {
            if (m_dashAction.triggered && !isDashing)
            {
                StartCoroutine(Dash());
            }
            else
            {
                float speed = m_speed * ((100 - 2 * m_goldController.goldCarried) / 100f);
                Vector2 moveVector = m_moveAction.ReadValue<Vector2>().normalized * speed;
                transform.Translate(new Vector3(moveVector.x, 0f, moveVector.y));
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collided");
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && isDashing)
        {
            Debug.Log("hit");
            PlayerMovement otherPlayerMovement = other.gameObject.GetComponent<PlayerMovement>();

            if (otherPlayerMovement != null)
            {
                Vector3 direction = other.transform.position - transform.position;
                direction.y = 0;
                Vector2 dashDirection = new Vector2(direction.x, direction.z).normalized;

                otherPlayerMovement.GetPushed(dashDirection);
            }
        }   
    }

    private void GetPushed(Vector2 dashDirection)
    {
        StartCoroutine(Slide(dashDirection, this));
        Debug.Log("pushed");
    }

    IEnumerator Slide(Vector2 dashDirection, PlayerMovement playerMovement)
    {
        // isDashing = true;

        Vector3 initial = playerMovement.transform.position;
        Vector3 final = initial + new Vector3(dashDirection.x, 0, dashDirection.y) * dashDistance;
        Vector3 pos;
        for (float t = 0; t < 1; t += Time.deltaTime / dashDuration)
        {
            pos = initial + (final - initial) * Mathf.Pow(t, 1f / 3f);
            playerMovement.transform.position = pos;
            yield return null;
        }

        // isDashing = false;
    }

    IEnumerator Dash()
    {
        isDashing = true;

        Vector3 initial = transform.position;
        Vector2 dashVector = m_moveAction.ReadValue<Vector2>().normalized * dashDistance;
        Vector3 final = new Vector3(initial.x + dashVector.x, initial.y, initial.z + dashVector.y);
        Vector3 pos;
        for (float t = 0; t < 1; t += Time.deltaTime / dashDuration)
        {
            pos = initial + (final - initial) * Mathf.Pow(t, 1f / 3f);
            transform.position = pos;
            yield return null;
        }

        isDashing = false;
    }

    void OnGameStart()
    {
        Debug.Log("on game start called in playermovement!");
        m_goldController = GetComponent<GoldController>();
        m_playerInput = GetComponent<PlayerInput>();

        m_moveAction = m_playerInput.actions["Move"];
        m_dashAction = m_playerInput.actions["Dash"];
        m_initialized = true;
    }

    void Awake()
    {
        GameStartHandler.Instance.onGameStart += OnGameStart;

        m_initialized = false;
    }
}
