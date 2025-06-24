using UnityEngine;
using UnityEngine.UI;

public class UIFeedback : MonoBehaviour
{
    public static UIFeedback Instance;

    public Text feedbackText;
    public Text scoreText;
    private int score = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void MostrarFeedback(string mensaje, Color color, int puntos)
    {
        feedbackText.text = mensaje;
        feedbackText.color = color;
        feedbackText.gameObject.SetActive(true);
        score += puntos;
        ActualizarScore();
        CancelInvoke(nameof(OcultarFeedback));
        Invoke(nameof(OcultarFeedback), 2f);
    }

    void ActualizarScore()
    {
        if (scoreText != null)
            scoreText.text = "Puntos: " + score;
    }

    void OcultarFeedback()
    {
        feedbackText.gameObject.SetActive(false);
    }
}
