using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public Transform handPoint; // El objeto "mano"
    public ItemInstance itemInHand;

    [Header("Zona permitida de movimiento (cubo interactivo)")]
    public BoxCollider interactionVolume;

    [Header("Layer donde el ítem puede caer (zonas de trabajo)")]
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

        Debug.Log("Agarraste: " + item.itemData.itemName);
    }

    public void DropItem()
    {
        if (!IsHoldingItem()) return;

        RaycastHit hit;
        Vector3 dropPosition = handPoint.position;

        // 1. Ver si hay una zona de trabajo debajo
        if (Physics.Raycast(dropPosition, Vector3.down, out hit, 2f, groundLayer))
        {
            dropPosition = hit.point + Vector3.up * 0.05f;

            // 2. Ver si la zona tiene WorkZone.cs
            if (hit.collider.TryGetComponent<WorkZone>(out var zone))
            {
                Debug.Log("📍 Ítem soltado en zona: " + zone.zoneType);

                // Podés ejecutar lógica según zona:
                switch (zone.zoneType)
                {
                    case WorkZoneType.Cocina:
                        Debug.Log("🔥 Este ítem podría ir a la parrilla.");
                        break;
                    case WorkZoneType.Emplatado:
                        Debug.Log("🍽 El ítem se agregó al plato.");
                        PlatoActual.Instance.AgregarIngrediente(itemInHand);
                        break;
                    case WorkZoneType.Entrega:
                        Debug.Log("📦 Entregado para evaluación.");
                        break;
                    default:
                        break;
                }
            }
        }

        // Soltar el ítem en la posición calculada
        itemInHand.transform.SetParent(null);
        itemInHand.transform.position = dropPosition;
        itemInHand.GetComponent<Rigidbody>().isKinematic = false;
        itemInHand.isHeld = false;
        itemInHand = null;
    }

    public void UpdateHandPosition(Vector3 targetWorldPos)
    {
        // Convertir el punto a coordenadas locales del volumen
        Vector3 localPoint = interactionVolume.transform.InverseTransformPoint(targetWorldPos);

        // Obtener el tamaño y centro del BoxCollider
        Vector3 halfSize = interactionVolume.size * 0.5f;
        Vector3 center = interactionVolume.center;

        // Clampeamos dentro del volumen considerando el centro
        localPoint.x = Mathf.Clamp(localPoint.x, center.x - halfSize.x, center.x + halfSize.x);
        localPoint.y = Mathf.Clamp(localPoint.y, center.y - halfSize.y, center.y + halfSize.y);
        localPoint.z = Mathf.Clamp(localPoint.z, center.z - halfSize.z, center.z + halfSize.z);

        // Convertimos a posición mundial y aplicamos
        Vector3 clampedWorldPos = interactionVolume.transform.TransformPoint(localPoint);
        handPoint.position = clampedWorldPos;
    }
}
