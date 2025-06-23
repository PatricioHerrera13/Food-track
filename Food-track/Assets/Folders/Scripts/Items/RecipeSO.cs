using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "ChoriExpress/Receta")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;                 // Ej: Choripán clásico
    public List<ItemSO> requiredIngredients;  // Pan + Chorizo
    public float maxValue;                    // Valor máximo si está perfecto
    public float allowedTolerance = 0f;       // (Opcional) margen de error
    public Sprite image;                      // Para mostrar en UI del pedido
}
