using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    [Header("Recetas")]
    public List<RecipeSO> availableRecipes;
    public RecipeSO currentRecipe;

    [Header("Configuración de ciclo")]
    public float tiempoEntrePedidos = 10f;
    private float tiempoRestante;
    private bool esperandoNuevoPedido = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        GenerateNewOrder();
    }

    void Update()
    {
        if (esperandoNuevoPedido)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0)
            {
                GenerateNewOrder();
            }
        }
    }

    public void GenerateNewOrder()
    {
        if (availableRecipes.Count == 0)
        {
            Debug.LogWarning("❌ No hay recetas disponibles.");
            return;
        }

        currentRecipe = availableRecipes[Random.Range(0, availableRecipes.Count)];
        Debug.Log("📦 Nuevo pedido generado: " + currentRecipe.recipeName);

        esperandoNuevoPedido = false;
    }

    public void MarcarPedidoComoEntregado()
    {
        currentRecipe = null;
        esperandoNuevoPedido = true;
        tiempoRestante = tiempoEntrePedidos;

        Debug.Log("✅ Pedido entregado. Esperando próximo pedido en " + tiempoEntrePedidos + " segundos...");
    }

    public bool HayPedidoActivo()
    {
        return currentRecipe != null;
    }
}
