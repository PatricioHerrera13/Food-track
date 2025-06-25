using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public LayerMask itemLayer;
    public float maxTouchMovement = 10f; // Máxima distancia en pantalla para considerar "toque"

    private Vector2 touchStartPos;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            touchStartPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            float moved = Vector2.Distance(touchStartPos, Input.mousePosition);
            if (moved < maxTouchMovement)
            {
                ProcesarToque(Input.mousePosition);
            }
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                touchStartPos = touch.position;

            if (touch.phase == TouchPhase.Ended)
            {
                float moved = Vector2.Distance(touchStartPos, touch.position);
                if (moved < maxTouchMovement)
                {
                    ProcesarToque(touch.position);
                }
            }
        }
#endif
    }

    void ProcesarToque(Vector2 pantallaPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pantallaPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, itemLayer))
        {
            if (hit.collider.TryGetComponent<ItemInstance>(out var item))
            {
                if (PlayerInventory.Instance.IsHoldingItem())
                    PlayerInventory.Instance.DropItem();
                else
                    PlayerInventory.Instance.PickUpItem(item);
            }
            else if (hit.collider.TryGetComponent<DispenserTactil>(out var dispenser))
            {
                dispenser.Dispensar();
            }
        }
    }
}
