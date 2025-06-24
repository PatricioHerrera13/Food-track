using UnityEngine;
using UnityEngine.UI;

public class PedidoVisual : MonoBehaviour
{
    public RecipeSO receta;
    public Image imagenPedido;

    public void Configurar(RecipeSO recetaAsignada)
    {
        receta = recetaAsignada;
        if (imagenPedido != null && receta.image != null)
            imagenPedido.sprite = receta.image;
    }

    public bool CoincideCon(ItemSO item)
    {
        return receta != null && receta.outputItem == item;
    }
}
