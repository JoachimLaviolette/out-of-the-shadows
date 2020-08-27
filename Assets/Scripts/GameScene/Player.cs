using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Range(50f, 300f)]
    private float m_MAX_HEALTH = 200f;
    private float m_health;
    private int m_score;

    public event EventHandler<float> m_onHealthChanged;
    public event EventHandler<int> m_onScoreChanged;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_health = m_MAX_HEALTH;
        m_score = 0;
    }

    public void Damage(float amount)
    {
        m_health = Mathf.Clamp(m_health - amount, 0f, m_MAX_HEALTH);
        m_onHealthChanged?.Invoke(this, m_health / m_MAX_HEALTH);
    }

    public void Heal(float amount)
    {
        m_health = Mathf.Clamp(m_health + amount, 0f, m_MAX_HEALTH);
        m_onHealthChanged?.Invoke(this, m_health / m_MAX_HEALTH);
    }

    public void AddScore(int amount)
    {
        this.m_score += amount;
        m_onScoreChanged?.Invoke(this, m_score);
    }
}
