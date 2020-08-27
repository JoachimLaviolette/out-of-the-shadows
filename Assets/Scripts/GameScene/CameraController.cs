using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;

    private void LateUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, player.transform.position + new Vector3(6f, 3f, 0f), 1f * Time.deltaTime);
        newPosition.z = -10f;
        transform.position = newPosition;
    }
}
