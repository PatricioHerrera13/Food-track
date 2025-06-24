using UnityEngine;
using UnityEngine.UI;

public class PantallaFinalUI : MonoBehaviour
{
    public static PantallaFinalUI Instance;

    public GameObject panelFinal;
    public Text textoResultado;
    public int puntajeRequerido = 50;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void MostrarResultado(int score)
    {
        panelFinal.SetActive(true);

        if (score >= puntajeRequerido)
        {
            textoResultado.text = $"🏆 ¡Victoria!\nGanancia: {score}";
        }
        else
        {
            textoResultado.text = $"❌ Derrota\nGanancia: {score}";
        }
    }
}
