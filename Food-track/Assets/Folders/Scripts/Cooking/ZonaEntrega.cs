using UnityEngine;

public class ZonaEntrega : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ItemInstance>(out var item))
        {
            PedidoVisual pedido = PedidoManager.Instance.ObtenerPedidoQueCoincida(item.itemData);
            if (pedido != null)
            {
                PedidoManager.Instance.CompletarPedido(pedido);
                UIFeedback.Instance.MostrarFeedback("✅ ¡Pedido entregado!", Color.green, Mathf.RoundToInt(item.itemData.valor));
                Destroy(other.gameObject);
            }
            else
            {
                UIFeedback.Instance.MostrarFeedback("❌ Pedido incorrecto", Color.red, 0);
            }
        }
    }
}
