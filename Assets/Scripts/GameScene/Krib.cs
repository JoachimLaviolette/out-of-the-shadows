using System.Collections;
using UnityEngine;

public class Krib : MonoBehaviour
{
    [SerializeField] 
    private LayerMask m_UILayerMask;
    private BoxCollider2D m_boxCollider;
    private Vector3[] m_waypoints;
    private int m_currentWaypointIndex;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float m_moveSpeed = .2f;
    [SerializeField] 
    [Range(1, 20)]
    private int m_reward = 1;
    private bool m_isPicked;
    private bool m_toDestroy;

    private void Awake()
    {
        m_boxCollider = GetComponent<BoxCollider2D>();
        m_waypoints = new Vector3[2];
        m_waypoints[0] = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        m_waypoints[1] = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        m_currentWaypointIndex = 0;
        m_isPicked = false;
        m_toDestroy = false; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_isPicked) return;

        Player player = collision.GetComponent<Player>();
        
        if (player is null) return;
        
        player.AddScore(m_reward);
        StartCoroutine(Pick());
    }

    private void Update()
    {
        if (!m_isPicked)
        {
            MoveToTargetWaypoint();
            UpdateTargetWaypoint();
        }
        else
        {
            CheckDestroy();
            StartCoroutine(PickAnimation());
        }
    }

    private void UpdateTargetWaypoint()
    {
        if (transform.position.Equals(m_waypoints[m_currentWaypointIndex]))
            m_currentWaypointIndex = m_currentWaypointIndex == m_waypoints.Length - 1 ? 0 : m_currentWaypointIndex + 1;
    }

    private void MoveToTargetWaypoint()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            m_waypoints[m_currentWaypointIndex],
            m_moveSpeed * Time.deltaTime
        );
    }

    IEnumerator Pick()
    {
        m_isPicked = true;

        yield return new WaitUntil(() => m_toDestroy);

        Destroy(gameObject);
    }

    private void CheckDestroy()
    {
        Collider2D collider = Physics2D.BoxCast(m_boxCollider.bounds.center, m_boxCollider.bounds.size, 0f, Vector2.up, .1f, m_UILayerMask).collider;
        m_toDestroy = collider is null ? false : collider.gameObject.Equals(UIManager.GetPlayerScore().gameObject);
    }

    IEnumerator PickAnimation()
    {
        transform.position = Vector3.MoveTowards(transform.position, UIManager.GetPlayerScore().transform.position, 0.3f);
        yield return new WaitForEndOfFrame();
    }
}
