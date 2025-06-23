using UnityEngine;

public class DragManager : MonoBehaviour
{
    public LayerMask ingredientLayer;
    public LayerMask interactionVolumeLayer;

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickup(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && PlayerInventory.Instance.IsHoldingItem())
        {
            MoveHandTo(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PlayerInventory.Instance.DropItem();
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            TryPickup(touch.position);
        }
        else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            if (PlayerInventory.Instance.IsHoldingItem())
                MoveHandTo(touch.position);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            PlayerInventory.Instance.DropItem();
        }
    }

    void TryPickup(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, ingredientLayer))
        {
            if (hit.collider.TryGetComponent<ItemInstance>(out var item))
            {
                PlayerInventory.Instance.PickUpItem(item);
            }
        }
    }

    void MoveHandTo(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactionVolumeLayer))
        {
            PlayerInventory.Instance.UpdateHandPosition(hit.point);
        }
    }
}
