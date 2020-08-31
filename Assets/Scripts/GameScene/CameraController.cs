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
    [SerializeField]
    private LayerMask m_areaDelimiterLayerMask;
    [SerializeField]
    [Range(5f, 30f)]
    private float m_areaDelimiterCheckingDistance = 10f;

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
        targetPosition.z = transform.position.z;

        Vector3 moveDir = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(targetPosition, transform.position);
            
        if (distance > 0f)
        {
            Vector3 newPosition = transform.position + moveDir * distance * m_moveSpeed * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newPosition, targetPosition);

            // Overshoot
            if (distanceAfterMoving > distance) newPosition = targetPosition;

            if (IsNearAreaDelimiter(newPosition)) newPosition.x = transform.position.x;

            transform.position = newPosition;
        }
    }

    private bool IsNearAreaDelimiter(Vector3 position)
    {
        return Physics2D.Raycast(position, Vector3.left, m_areaDelimiterCheckingDistance, m_areaDelimiterLayerMask).collider != null ||
            Physics2D.Raycast(position, Vector3.right, m_areaDelimiterCheckingDistance, m_areaDelimiterLayerMask).collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * m_areaDelimiterCheckingDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * m_areaDelimiterCheckingDistance);
    }
}
