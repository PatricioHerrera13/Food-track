using System.Collections.Generic;
using UnityEngine;

public class RecipeDatabase : MonoBehaviour
{
    public static RecipeDatabase Instance;
    public List<RecipeSO> todasLasRecetas;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public RecipeSO ObtenerRecetaDesdeIngredientes(List<ItemSO> ingredientes)
    {
        foreach (var receta in todasLasRecetas)
        {
            if (receta.EsMatch(ingredientes)) return receta;
        }
        return null;
    }
}
