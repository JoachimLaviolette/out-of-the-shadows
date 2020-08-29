using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private LayerMask m_walkableSurfaceMask;
    Rigidbody2D m_rb;
    BoxCollider2D m_bc;
    [SerializeField]
    BoxCollider2D m_groundCollider;
    private Animator m_animator;
    [SerializeField]
    [Range(0.01f, 0.2f)]
    private float m_groundDetectionDistance = 0.01f;
    [SerializeField]
    [Range(2f, 5f)]
    private float m_moveSpeed = 2f;
    [SerializeField]
    [Range(5f, 10f)]
    private float m_runSpeed = 5f;
    [Range(200f, 1000f)]
    private float m_jumpForce = 500f;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_bc = GetComponent<BoxCollider2D>();
        m_animator = GetComponent<Animator>();
    }

    public void Move(float dir, bool run, bool jump)
    {
        transform.Translate(Vector3.right * dir * (run ? m_runSpeed : m_moveSpeed) * Time.deltaTime);
        HandleJump(jump);
    }

    private void HandleJump(bool jump)
    {
        if (!jump || !IsGrounded()) return;

        m_rb.AddForce(new Vector2(0f, m_jumpForce));
        StartCoroutine(PlayJumpAnimation());
    }

    IEnumerator PlayJumpAnimation()
    {
        m_animator.SetBool(Animator.StringToHash("isJumping"), true);
        yield return new WaitForSeconds(.30f);
        m_animator.SetBool(Animator.StringToHash("isJumping"), false);
        yield return null;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(m_groundCollider.bounds.center, m_groundCollider.bounds.size, 0f, Vector2.down, m_groundDetectionDistance, m_walkableSurfaceMask).collider != null;
    }
}
