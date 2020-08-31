using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    [SerializeField]
    private Transform m_player;

    private GameManager() { }

    private void Awake()
    {
        if (m_instance == null) m_instance = this;
    }

    private void Start()
    {
        SetCameraTarget(m_player);
    }

    private void SetCameraTarget(Transform target)
    {
        AssetManager.GetGameCamera().SetGetTargetPosition(() => { return target.position; });
    }

}
