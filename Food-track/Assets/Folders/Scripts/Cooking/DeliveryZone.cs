using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public float autoDestroyDelay = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ItemInstance>(out var item))
        {
            CheckIngredient(item);
        }
    }

    void CheckIngredient(ItemInstance item)
    {
        var currentRecipe = OrderManager.Instance.currentRecipe;

        if (currentRecipe == null)
        {
            Debug.LogWarning("No hay receta activa.");
            return;
        }

        bool isCorrect = currentRecipe.requiredIngredients.Contains(item.itemData);

        if (isCorrect)
        {
            Debug.Log("✅ Entregaste un ingrediente correcto: " + item.itemData.itemName);
            Destroy(item.gameObject, autoDestroyDelay);
        }
        else
        {
            Debug.Log("❌ Ingrediente incorrecto: " + item.itemData.itemName);
        }
    }
}
