using UnityEngine;

public class CookingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CookableColorItem>(out var cookable))
        {
            cookable.EmpezarCoccion();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CookableColorItem>(out var cookable))
        {
            cookable.DetenerCoccion();
        }
    }
}
