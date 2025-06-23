using UnityEngine;

public class CookableItem : MonoBehaviour
{
    public float tiempoParaCocinar = 5f;
    public float tiempoParaQuemar = 8f;

    private float tiempoEnZona = 0f;
    private bool estaCocinando = false;
    private ItemInstance itemInstance;

    private void Start()
    {
        itemInstance = GetComponent<ItemInstance>();
    }

    private void Update()
    {
        if (!estaCocinando || itemInstance == null) return;

        tiempoEnZona += Time.deltaTime;

        if (tiempoEnZona >= tiempoParaQuemar)
        {
            CambiarEstadoCoccion(itemInstance.itemData.quemado);
        }
        else if (tiempoEnZona >= tiempoParaCocinar)
        {
            CambiarEstadoCoccion(itemInstance.itemData.cocido);
        }
    }

    public void EmpezarCoccion() => estaCocinando = true;
    public void DetenerCoccion() => estaCocinando = false;

    private void CambiarEstadoCoccion(ItemSO nuevoEstado)
    {
        if (nuevoEstado == null) return;

        GameObject nuevoObj = Instantiate(nuevoEstado.prefab, transform.position, transform.rotation);
        Rigidbody rb = nuevoObj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = GetComponent<Rigidbody>()?.linearVelocity ?? Vector3.zero;

        Destroy(gameObject);
    }
}
