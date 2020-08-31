using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_playerController;
    [SerializeField]
    [Range(0f, 30f)]
    private float m_moveSpeed = 2f;
    [SerializeField]
    private Func<Vector3> GetTargetPosition;

    public void Setup(Func<Vector3> GetTargetPosition)
    {
        this.GetTargetPosition = GetTargetPosition;
    }

    public void SetGetTargetPosition(Func<Vector3> GetTargetPosition)
    {
        this.GetTargetPosition = GetTargetPosition;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = GetTargetPosition();
        targetPosition.x += 5f;
        targetPosition.z = transform.position.z;

        Vector3 moveDir = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(targetPosition, transform.position);

        if (distance > 0f)
        {
            Vector3 newPosition = transform.position + moveDir * distance * m_moveSpeed * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newPosition, targetPosition);

            // Overshoot
            if (distanceAfterMoving > distance) newPosition = targetPosition;

            transform.position = newPosition;
        }
    }
}
