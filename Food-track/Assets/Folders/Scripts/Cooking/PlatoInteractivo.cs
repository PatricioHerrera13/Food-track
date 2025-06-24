using System.Collections.Generic;
using UnityEngine;

public class PlatoInteractivo : MonoBehaviour
{
    public List<ItemSO> ingredientes = new List<ItemSO>();
    public float alturaOffset = 0.1f;

    public void AgregarIngrediente(ItemInstance item)
    {
        if (!ingredientes.Contains(item.itemData))
        {
            ingredientes.Add(item.itemData);
            item.transform.SetParent(transform);
            item.transform.localPosition = new Vector3(0, ingredientes.Count * alturaOffset, 0);
            item.GetComponent<Rigidbody>().isKinematic = true;

            RevisarSiSePuedeCrearReceta();
        }
    }

    void RevisarSiSePuedeCrearReceta()
    {
        RecipeSO receta = RecipeDatabase.Instance.ObtenerRecetaDesdeIngredientes(ingredientes);
        if (receta != null)
        {
            GameObject nuevo = Instantiate(receta.outputItem.prefab, transform.position, Quaternion.identity);
            Destroy(gameObject); // destruir el plato con ingredientes
        }
    }
}
