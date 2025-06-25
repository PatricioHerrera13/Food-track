using UnityEngine;

public class DispenserTactil : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject prefabItem;

    [Header("Opciones")]
    public Vector3 offset = new Vector3(0, 0.5f, 0);

    // Este método será llamado desde TouchManager
    public void Dispensar()
    {
        if (prefabItem == null)
        {
            Debug.LogError("❌ No hay prefab asignado al Dispenser.");
            return;
        }

        Vector3 spawnPosition = transform.position + offset;
        Instantiate(prefabItem, spawnPosition, Quaternion.identity);
        Debug.Log("✅ Item generado desde Dispenser");
    }
}
