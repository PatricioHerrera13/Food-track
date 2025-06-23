using UnityEngine;

public enum WorkZoneType
{
    Pedidos,
    Cocina,
    Emplatado,
    Entrega
}

public class WorkZone : MonoBehaviour
{
    public WorkZoneType zoneType;
}
