using UnityEngine;

public class TouchCameraHorizontal : MonoBehaviour
{
    public float sensibilidad = 0.1f;
    public float limiteIzquierdo = -5f;
    public float limiteDerecho = 5f;

    private Vector2 touchStart;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            float delta = Input.GetAxis("Mouse X") * sensibilidad;
            MoverCamara(delta);
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float delta = touch.deltaPosition.x * sensibilidad * Time.deltaTime;
                MoverCamara(delta);
            }
        }
#endif
    }

    void MoverCamara(float deltaX)
    {
        Vector3 nuevaPos = transform.position + new Vector3(-deltaX, 0, 0);
        nuevaPos.x = Mathf.Clamp(nuevaPos.x, limiteIzquierdo, limiteDerecho);
        transform.position = nuevaPos;
    }
}
