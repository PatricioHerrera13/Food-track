using UnityEngine;

public class CookableColorItem : MonoBehaviour
{
    [Header("Tiempos de cocción")]
    public float tiempoParaCocinar = 3f;
    public float tiempoParaQuemar = 6f;

    private float tiempoActual = 0f;
    private bool cocinando = false;

    [Header("Colores")]
    public Color colorCrudo = new Color(1f, 1f, 1f);          // Blanco o carne cruda
    public Color colorCocido = new Color(0.6f, 0.3f, 0.1f);    // Marrón cocido
    public Color colorQuemado = Color.black;

    [Header("Visual")]
    public Renderer targetRenderer;

    private void Awake()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponentInChildren<Renderer>();
        }

        // Color inicial
        if (targetRenderer != null)
            targetRenderer.material.color = colorCrudo;
    }

    void Update()
    {
        if (!cocinando) return;

        tiempoActual += Time.deltaTime;

        // Calculamos el color actual según el progreso
        if (tiempoActual < tiempoParaCocinar)
        {
            // Crudo → Cocido
            float t = tiempoActual / tiempoParaCocinar;
            targetRenderer.material.color = Color.Lerp(colorCrudo, colorCocido, t);
        }
        else if (tiempoActual < tiempoParaQuemar)
        {
            // Cocido → Quemado
            float t = (tiempoActual - tiempoParaCocinar) / (tiempoParaQuemar - tiempoParaCocinar);
            targetRenderer.material.color = Color.Lerp(colorCocido, colorQuemado, t);
        }
        else
        {
            // Quemado total
            targetRenderer.material.color = colorQuemado;
        }
    }

    public void EmpezarCoccion()
    {
        cocinando = true;
        Debug.Log("🔥 Comenzó la cocción");
    }

    public void DetenerCoccion()
    {
        cocinando = false;
        Debug.Log("❄️ Detenida la cocción");
    }
}
