using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0f, 50f)]
    private float m_bodyDamages = 15f;

    private void Awake()
    {
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player is null) return;

        player.Damage(m_bodyDamages);
    }
}
