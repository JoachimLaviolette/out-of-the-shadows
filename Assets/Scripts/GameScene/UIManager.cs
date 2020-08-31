using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;
    [SerializeField]
    private Transform m_playerHealth;
    [SerializeField]
    private Transform m_playerExperience;
    [SerializeField]
    private Image m_playerStamina;
    [SerializeField]
    private Text m_playerStaminaText;
    [SerializeField]
    private Text m_playerKribs;

    private UIManager() { }

    private void Awake()
    {
        if (m_instance == null) m_instance = this;
    }

    private void Start()
    {
        AssetManager.GetPlayer().m_onHealthChanged += UpdatePlayerHealth;
        AssetManager.GetPlayer().m_onExperienceChanged += UpdatePlayerExperience;
        AssetManager.GetPlayer().m_onStaminaChanged += UpdatePlayerStamina;
        AssetManager.GetPlayer().m_onKribsChanged += UpdatePlayerKribs;
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

    private static void UpdatePlayerExperience(object sender, float newExperience)
    {
        Vector3 scale = m_instance.m_playerExperience.localScale;
        scale.x = newExperience;
        m_instance.m_playerExperience.localScale = new Vector3(
                newExperience,
                m_instance.m_playerExperience.localScale.y,
                m_instance.m_playerExperience.localScale.z
            );
    }

    private static void UpdatePlayerStamina(object sender, OnStaminaChangedEventArgs args)
    {
        m_instance.m_playerStamina.fillAmount = args.newStaminaNormalized;
        m_instance.m_playerStaminaText.text = args.newStamina.ToString();
    }

    private static void UpdatePlayerKribs(object sender, int newKribs)
    {
        m_instance.m_playerKribs.text = newKribs.ToString();
    }

    public static Text GetPlayerKribs()
    {
        return m_instance?.m_playerKribs;
    }
}
