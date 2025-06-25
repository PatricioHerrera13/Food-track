using UnityEngine;

public class CameraMoverBotones : MonoBehaviour
{
    public float velocidad = 3f;
    public float limiteIzquierdo = -5f;
    public float limiteDerecho = 5f;

    [HideInInspector] public bool moverIzquierda = false;
    [HideInInspector] public bool moverDerecha = false;

    void Update()
    {
        Vector3 pos = transform.position;

        if (moverIzquierda)
            pos.x -= velocidad * Time.deltaTime;
        else if (moverDerecha)
            pos.x += velocidad * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, limiteIzquierdo, limiteDerecho);
        transform.position = pos;
    }
}
