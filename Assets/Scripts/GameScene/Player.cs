using System;
using UnityEngine;

public class OnStaminaChangedEventArgs : EventArgs
{
    public float newStaminaNormalized;
    public int newStamina; 
} 

public class Player : MonoBehaviour
{
    [SerializeField]
    [Range(50f, 300f)]
    private float m_MAX_HEALTH = 200f;
    [SerializeField]
    [Range(2000f, 5000f)]
    private float m_MAX_EXPERIENCE = 3000f;
    [SerializeField]
    [Range(50f, 500f)]
    private float m_MAX_STAMINA = 100;
    private float m_health;
    private float m_experience;
    private float m_stamina;
    private int m_kribs;

    public event EventHandler<float> m_onHealthChanged;
    public event EventHandler<float> m_onExperienceChanged;
    public event EventHandler<OnStaminaChangedEventArgs> m_onStaminaChanged;
    public event EventHandler<int> m_onKribsChanged;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_health = m_MAX_HEALTH;
        ResetExperience();
        m_stamina = m_MAX_STAMINA;
        m_kribs = 0;
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

    public void AddExperience(float amount)
    {
        m_experience = Mathf.Clamp(m_experience + amount, 0f, m_MAX_EXPERIENCE);
        m_onExperienceChanged?.Invoke(this, m_experience / m_MAX_EXPERIENCE);
    }

    public void ResetExperience()
    {
        m_experience = 0f;
        m_onExperienceChanged?.Invoke(this, m_experience / m_MAX_EXPERIENCE);
    }

    public void AddStamina(float amount)
    {
        m_stamina = Mathf.Clamp(m_stamina + amount, 0f, m_MAX_STAMINA);
        m_onStaminaChanged?.Invoke(this, new OnStaminaChangedEventArgs() { newStaminaNormalized = m_stamina / m_MAX_STAMINA, newStamina = (int) m_stamina });
    }

    public void RemoveStamina(float amount)
    {
        m_stamina = Mathf.Clamp(m_stamina - amount, 0f, m_MAX_STAMINA);
        m_onStaminaChanged?.Invoke(this, new OnStaminaChangedEventArgs() { newStaminaNormalized = m_stamina / m_MAX_STAMINA, newStamina = (int) m_stamina });
    }

    public void AddKribs(int amount)
    {
        m_kribs += amount;
        m_onKribsChanged?.Invoke(this, m_kribs);
    }

    public void RemoveKribs(int amount)
    {
        m_kribs -= amount;
        if (m_kribs < 0) m_kribs = 0;
        m_onKribsChanged?.Invoke(this, m_kribs);
    }

    public float GetStamina()
    {
        return m_stamina;
    }
}
