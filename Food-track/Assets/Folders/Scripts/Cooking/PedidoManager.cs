using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedidoManager : MonoBehaviour
{
    public static PedidoManager Instance;

    [Header("Recetas posibles")]
    public List<RecipeSO> recetasDisponibles;

    [Header("Slots para pedidos")]
    public Transform[] posicionesPedidos; // Máximo 6
    public GameObject prefabPedidoVisual;

    [Header("Control de tiempo")]
    public float tiempoEntrePedidos = 5f;
    private float tiempoActual = 0f;

    [Header("Tiempo total del juego")]
    public float duracionDelJuego = 90f; // o el tiempo que quieras


    private List<PedidoVisual> pedidosActivos = new List<PedidoVisual>();
    private bool juegoActivo = true;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        tiempoActual = 5f; // Primer pedido a los 5s
    }

    void Update()
    {
        if (!juegoActivo) return;

        // 🔴 Conteo regresivo global
        duracionDelJuego -= Time.deltaTime;

        if (duracionDelJuego <= 0f)
        {
            FinalizarJuego();
            return;
        }

        // 🟢 Spawneo de pedidos
        tiempoActual -= Time.deltaTime;

        if (tiempoActual <= 0f && pedidosActivos.Count < posicionesPedidos.Length)
        {
            GenerarNuevoPedido();
            tiempoActual = tiempoEntrePedidos;
        }
    }

    public void GenerarNuevoPedido()
    {
        RecipeSO receta = recetasDisponibles[Random.Range(0, recetasDisponibles.Count)];

        for (int i = 0; i < posicionesPedidos.Length; i++)
        {
            if (posicionesPedidos[i].childCount == 0)
            {
                GameObject nuevo = Instantiate(prefabPedidoVisual, posicionesPedidos[i].position, Quaternion.identity, posicionesPedidos[i]);
                PedidoVisual visual = nuevo.GetComponent<PedidoVisual>();
                visual.Configurar(receta);
                pedidosActivos.Add(visual);
                return;
            }
        }
    }

    public PedidoVisual ObtenerPedidoQueCoincida(ItemSO entregado)
    {
        foreach (var pedido in pedidosActivos)
        {
            if (pedido != null && pedido.CoincideCon(entregado))
                return pedido;
        }

        return null;
    }

    public void CompletarPedido(PedidoVisual pedido)
    {
        pedidosActivos.Remove(pedido);
        Destroy(pedido.gameObject);
    }

    public void FinalizarJuego()
    {
        juegoActivo = false;
        PantallaFinalUI.Instance.MostrarResultado(UIFeedback.Instance.Score);
    }
}
