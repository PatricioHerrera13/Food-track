using UnityEngine;

public class DispenserTactil : MonoBehaviour
{
    public GameObject prefabItem;
    public LayerMask detectionLayer;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            RevisarInput(Input.mousePosition);
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            RevisarInput(Input.GetTouch(0).position);
#endif
    }

    void RevisarInput(Vector2 pantallaPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pantallaPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, detectionLayer))
        {
            if (hit.collider.gameObject == gameObject)
            {
                Vector3 pos = transform.position + Vector3.up * 0.5f;
                Instantiate(prefabItem, pos, Quaternion.identity);
            }
        }
    }
}
