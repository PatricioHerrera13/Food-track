using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    public List<RecipeSO> availableRecipes;
    public RecipeSO currentRecipe;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        GenerateNewOrder();
    }

    public void GenerateNewOrder()
    {
        if (availableRecipes.Count == 0) return;

        currentRecipe = availableRecipes[Random.Range(0, availableRecipes.Count)];
        Debug.Log("Nuevo pedido generado: " + currentRecipe.recipeName);
    }
}
