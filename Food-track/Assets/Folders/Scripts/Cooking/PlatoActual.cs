using System.Collections.Generic;
using UnityEngine;

public class PlatoActual : MonoBehaviour
{
    public static PlatoActual Instance;

    public List<ItemSO> ingredientesEnPlato = new List<ItemSO>();
    public Transform posicionVisualDeIngredientes;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AgregarIngrediente(ItemInstance item)
    {
        if (!ingredientesEnPlato.Contains(item.itemData))
        {
            ingredientesEnPlato.Add(item.itemData);
            item.transform.SetParent(posicionVisualDeIngredientes);
            item.transform.localPosition = new Vector3(0, ingredientesEnPlato.Count * 0.1f, 0);
            item.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void VaciarPlato()
    {
        ingredientesEnPlato.Clear();
        foreach (Transform child in posicionVisualDeIngredientes)
        {
            Destroy(child.gameObject);
        }
    }

    public bool EsPedidoCorrecto(RecipeSO receta)
    {
        if (receta == null) return false;
        if (ingredientesEnPlato.Count != receta.requiredIngredients.Count)
            return false;

        foreach (var required in receta.requiredIngredients)
        {
            if (!ingredientesEnPlato.Contains(required))
                return false;
        }

        return true;
    }
}
