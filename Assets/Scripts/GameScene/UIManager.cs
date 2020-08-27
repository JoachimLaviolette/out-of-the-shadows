using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;
    [SerializeField]
    private Transform m_playerHealth;
    [SerializeField]
    private Text playerScore;

    private UIManager() { }

    private void Awake()
    {
        if (m_instance == null) m_instance = this;
    }

    private void Start()
    {
        AssetManager.GetPlayer().m_onHealthChanged += UpdatePlayerHealth;
        AssetManager.GetPlayer().m_onScoreChanged += UpdatePlayerScore;
    }
    private static void UpdatePlayerHealth(object sender, float newHealth)
    {
        Vector3 scale = m_instance.m_playerHealth.localScale;
        scale.x = newHealth;
        m_instance.m_playerHealth.localScale = new Vector3(
                newHealth,
                m_instance.m_playerHealth.localScale.y,
                m_instance.m_playerHealth.localScale.z
            );
    }

    private static void UpdatePlayerScore(object sender, int newScore)
    {
        m_instance.playerScore.text = String.Format("KRIBS: {0}", newScore);
    }

    public static Text GetPlayerScore()
    {
        return m_instance?.playerScore;
    }
}
