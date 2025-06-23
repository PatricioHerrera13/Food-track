using UnityEngine;

public class PlatoEntregaUI : MonoBehaviour
{
    public void EntregarPlato()
    {
        var receta = OrderManager.Instance.currentRecipe;
        bool esCorrecto = PlatoActual.Instance.EsPedidoCorrecto(receta);

        if (esCorrecto)
        {
            Debug.Log("✅ Pedido correcto entregado");
            OrderManager.Instance.GenerateNewOrder();
            PlatoActual.Instance.VaciarPlato();
        }
        else
        {
            Debug.Log("❌ Pedido incorrecto entregado");
        }
    }
}
