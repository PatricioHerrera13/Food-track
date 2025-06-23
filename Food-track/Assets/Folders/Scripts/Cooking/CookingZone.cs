using UnityEngine;

public class CookingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CookableItem>(out var cookable))
        {
            cookable.EmpezarCoccion();
            Debug.Log("🔥 Cocinando: " + cookable.GetComponent<ItemInstance>()?.itemData.itemName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CookableItem>(out var cookable))
        {
            cookable.DetenerCoccion();
            Debug.Log("🥩 Retirado de la parrilla: " + cookable.GetComponent<ItemInstance>()?.itemData.itemName);
        }
    }
}
