using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private InputManager m_inputManager; 
    private PlayerController m_playerController;

    private float m_moveDir;
    private bool m_run;
    private bool m_jump;

    private void OnEnable()
    {
        m_inputManager.Enable();
    }

    private void OnDisable()
    {
        m_inputManager.Disable();
    }

    private void Awake()
    {
        Initialize();
        SetupControls();
        m_playerController = GetComponent<PlayerController>();
    }

    private void SetupControls()
    {
        m_inputManager = new InputManager();
        m_inputManager.Player.Run.performed += _ => m_run = true;
        m_inputManager.Player.Run.canceled += _ => m_run = false;
        m_inputManager.Player.Jump.performed += _ => m_jump = true; 
        m_inputManager.Player.Jump.canceled += _ => m_jump = false; 
        m_inputManager.Player.Movement.performed += context => m_moveDir = context.ReadValue<float>();
        m_inputManager.Player.Movement.canceled += _ => m_moveDir = 0f;
    }

    private void Initialize()
    {
        m_moveDir = 0f;
        m_run = false;
        m_jump = false;
    }

    private void FixedUpdate()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        m_playerController.Move(m_moveDir, m_run, m_jump);
        StartCoroutine(PlayMoveAnimation(m_moveDir));
        m_jump = false;
    }

    IEnumerator PlayMoveAnimation(float movement)
    {
        transform.eulerAngles = (movement < 0f ? Vector3.forward : movement > 0f ? Vector3.back : Vector3.zero) * 7f;
        yield return null;
    }
}
