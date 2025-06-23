using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform focusPoint; // El objeto a seguir (ingrediente o jugador)
    public float smoothSpeed = 2f;
    public Vector3 offset = new Vector3(0f, 4.5f, -2f);
    public float maxFollowDistance = 1f;

    private Vector3 targetPosition;

    void LateUpdate()
    {
        if (focusPoint == null) return;

        Vector3 desiredPosition = focusPoint.position + offset;
        float distance = Vector3.Distance(transform.position, desiredPosition);

        if (distance > maxFollowDistance)
        {
            // Solo interpolar si el foco está lejos del centro visual
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }

        // Siempre mirar hacia el foco
        transform.LookAt(focusPoint);
    }
}
