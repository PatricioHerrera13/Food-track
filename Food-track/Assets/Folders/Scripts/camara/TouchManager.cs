using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public LayerMask itemLayer;
    public float maxTouchMovement = 10f;

    private Vector2 touchStart;
    private bool arrastrando = false;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
            ProcesarInicio(Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && PlayerInventory.Instance.IsHoldingItem())
            MoverMano(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
        {
            if (arrastrando)
            {
                PlayerInventory.Instance.DropItem();
                arrastrando = false;
            }
        }
#else
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchStart = touch.position;
            ProcesarInicio(touch.position);
        }

        if (touch.phase == TouchPhase.Moved && PlayerInventory.Instance.IsHoldingItem())
            MoverMano(touch.position);

        if (touch.phase == TouchPhase.Ended && arrastrando)
        {
            PlayerInventory.Instance.DropItem();
            arrastrando = false;
        }
#endif
    }

    void ProcesarInicio(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, itemLayer))
        {
            if (!PlayerInventory.Instance.IsHoldingItem())
            {
                if (hit.collider.TryGetComponent<ItemInstance>(out var item))
                {
                    PlayerInventory.Instance.PickUpItem(item);
                    arrastrando = true;
                }
                else if (hit.collider.TryGetComponent<DispenserTactil>(out var dispenser))
                {
                    bool entregado = dispenser.DispensarEnMano();
                    arrastrando = entregado;
                }
            }
        }
    }

    void MoverMano(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            PlayerInventory.Instance.UpdateHandPosition(hit.point);
        }
    }
}
