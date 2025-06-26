using UnityEngine;

public class DispenserTactil : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject prefabItem;

    [Header("Opciones")]
    public Vector3 offset = new Vector3(0, 0.5f, 0);

    public bool DispensarEnMano()
    {
        if (prefabItem == null)
        {
            Debug.LogError("❌ No hay prefab asignado al Dispenser.");
            return false;
        }

        if (PlayerInventory.Instance == null || PlayerInventory.Instance.IsHoldingItem())
        {
            Debug.Log("🚫 Mano ocupada, no se puede dispensar");
            return false;
        }

        Vector3 spawnPos = transform.position + offset;
        GameObject nuevo = Instantiate(prefabItem, spawnPos, Quaternion.identity);

        ItemInstance item = nuevo.GetComponent<ItemInstance>();
        if (item != null)
        {
            PlayerInventory.Instance.PickUpItem(item);
            return true;
        }

        Debug.LogError("❌ El prefab del dispenser no tiene ItemInstance.");
        return false;
    }
}
