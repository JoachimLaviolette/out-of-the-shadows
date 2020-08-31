using UnityEngine;

public class AssetManager : MonoBehaviour
{
    private static AssetManager m_instance;
    [SerializeField]
    private Player m_player;
    [SerializeField]
    private CameraController m_camera;

    private AssetManager() { }

    private void Awake()
    {
        if (m_instance == null) m_instance = this;
    }

    public static Player GetPlayer()
    {
        return m_instance.m_player;
    }

    public static CameraController GetGameCamera()
    {
        return m_instance.m_camera;
    }
}
