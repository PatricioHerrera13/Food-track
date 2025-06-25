using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public Transform handPoint; // Objeto "mano"
    public ItemInstance itemInHand;

    [Header("Zona permitida de movimiento (cubo interactivo)")]
    public BoxCollider interactionVolume;

    [Header("Layer donde el ítem puede caer (zonas físicas)")]
    public LayerMask groundLayer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool IsHoldingItem() => itemInHand != null;

    public void PickUpItem(ItemInstance item)
    {
        if (IsHoldingItem()) return;

        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.SetParent(handPoint);
        item.transform.localPosition = Vector3.zero;
        itemInHand = item;
        item.isHeld = true;

        Debug.Log("👋 Agarraste: " + item.itemData.itemName);
    }

    public void DropItem()
    {
        if (!IsHoldingItem()) return;

        RaycastHit hit;
        Vector3 dropPosition = handPoint.position;

        // Si hay una superficie válida debajo, soltar el ítem ahí
        if (Physics.Raycast(dropPosition, Vector3.down, out hit, 2f, groundLayer))
        {
            dropPosition = hit.point + Vector3.up * 0.05f;
        }

        itemInHand.transform.SetParent(null);
        itemInHand.transform.position = dropPosition;
        itemInHand.GetComponent<Rigidbody>().isKinematic = false;
        itemInHand.isHeld = false;
        itemInHand = null;
    }

    public void UpdateHandPosition(Vector3 targetWorldPos)
    {
        Vector3 localPoint = interactionVolume.transform.InverseTransformPoint(targetWorldPos);
        Vector3 halfSize = interactionVolume.size * 0.5f;
        Vector3 center = interactionVolume.center;

        localPoint.x = Mathf.Clamp(localPoint.x, center.x - halfSize.x, center.x + halfSize.x);
        localPoint.y = Mathf.Clamp(localPoint.y, center.y - halfSize.y, center.y + halfSize.y);
        localPoint.z = Mathf.Clamp(localPoint.z, center.z - halfSize.z, center.z + halfSize.z);

        Vector3 clampedWorldPos = interactionVolume.transform.TransformPoint(localPoint);
        handPoint.position = clampedWorldPos;
    }
}
