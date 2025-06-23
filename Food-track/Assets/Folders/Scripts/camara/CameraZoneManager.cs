using UnityEngine;

public class CameraZoneManager : MonoBehaviour
{
    public Transform[] zoneTargets; // Los puntos de enfoque (1 por zona)
    public Transform cameraTransform; // La cámara principal
    public Vector3 offset = new Vector3(0f, 4.5f, -2f);
    public float transitionSpeed = 3f;

    private int currentZone = 0;
    private bool isTransitioning = false;

    void Update()
    {
        if (isTransitioning) return;

        // Cambio de zona con teclas (provisorio hasta tener UI o swipe)
        if (Input.GetKeyDown(KeyCode.RightArrow)) ChangeZone(1);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangeZone(-1);
    }

    void LateUpdate()
    {
        if (!isTransitioning) return;

        Vector3 targetPosition = zoneTargets[currentZone].position + offset;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, Time.deltaTime * transitionSpeed);
        cameraTransform.LookAt(zoneTargets[currentZone]);

        // Cortamos la transición cuando esté cerca
        if (Vector3.Distance(cameraTransform.position, targetPosition) < 0.05f)
            isTransitioning = false;
    }

    public void ChangeZone(int direction)
    {
        int newZone = Mathf.Clamp(currentZone + direction, 0, zoneTargets.Length - 1);
        if (newZone != currentZone)
        {
            currentZone = newZone;
            isTransitioning = true;
        }
    }

    // Llamada directa (desde UI táctil)
    public void GoToZone(int index)
    {
        if (index >= 0 && index < zoneTargets.Length)
        {
            currentZone = index;
            isTransitioning = true;
        }
    }
}
