using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "ChoriExpress/Receta")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;                 // Ej: Choripán clásico
    public List<ItemSO> requiredIngredients;  // Ingredientes necesarios (ej: pan + chorizo cocido)
    public float maxValue = 10f;              // Valor máximo si se entrega perfecto
    public float allowedTolerance = 0f;       // (Opcional) margen de error
    public Sprite image;                      // Imagen del plato para mostrar en el pedido físico
    public ItemSO outputItem;                 // Ítem resultante si se completa la receta

    /// <summary>
    /// Compara una lista de ingredientes actuales y determina si cumplen con esta receta.
    /// </summary>
    public bool EsMatch(List<ItemSO> actuales)
    {
        if (requiredIngredients.Count != actuales.Count) return false;

        foreach (var ing in requiredIngredients)
        {
            if (!actuales.Contains(ing))
                return false;
        }

        return true;
    }
}
