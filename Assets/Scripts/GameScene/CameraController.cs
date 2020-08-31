using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController m_playerController;

    private void LateUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, m_playerController.transform.position + new Vector3(3f, 3f, 0f), 1f * Time.deltaTime);
        newPosition.z = -10f;
        transform.position = newPosition;
    }
}
