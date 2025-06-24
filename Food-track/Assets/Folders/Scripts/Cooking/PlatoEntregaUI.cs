using UnityEngine;

public class PlatoEntregaUI : MonoBehaviour
{
    public void EntregarPlato()
    {
        var receta = OrderManager.Instance.currentRecipe;
        bool esCorrecto = PlatoActual.Instance.EsPedidoCorrecto(receta);

       if (esCorrecto)
        {
            UIFeedback.Instance.MostrarFeedback("✅ Pedido correcto", Color.green, 10);
            OrderManager.Instance.GenerateNewOrder();
            PlatoActual.Instance.VaciarPlato();
        }
        else
        {
            UIFeedback.Instance.MostrarFeedback("❌ Pedido incorrecto", Color.red, 0);
        }
    }
}
