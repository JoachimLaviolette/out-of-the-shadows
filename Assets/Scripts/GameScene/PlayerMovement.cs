using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    [Range(.1f, 2f)]
    private float m_RUN_STAMINA_COST = .1f;
    [SerializeField]
    [Range(.3f, 2f)]
    private float m_JUMP_STAMINA_COST = .3f;
    [SerializeField]
    [Range(5f, 12f)]
    private float m_STAMINA_REGENRATION_SPEED = 5f;

    private InputManager m_inputManager; 
    private PlayerController m_playerController; 
    private Player m_player;

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
        m_player = GetComponent<Player>();
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
        if (m_player.GetStamina() == 0f)
        {
            m_run = false;
            m_jump = false;
        }

        m_playerController.Move(m_moveDir, m_run, m_jump);
        StartCoroutine(PlayMoveAnimation(m_moveDir));
        
        if (m_run && m_moveDir != 0f)
            m_player.RemoveStamina(m_RUN_STAMINA_COST);
        if (m_jump)
            m_player.RemoveStamina(m_JUMP_STAMINA_COST);
        else if ((!m_run || m_moveDir == 0f) && !m_jump)
            m_player.AddStamina(m_STAMINA_REGENRATION_SPEED * Time.deltaTime);

        m_jump = false;
    }

    IEnumerator PlayMoveAnimation(float movement)
    {
        transform.eulerAngles = (movement < 0f ? Vector3.forward : movement > 0f ? Vector3.back : Vector3.zero) * 7f;
        yield return null;
    }
}
