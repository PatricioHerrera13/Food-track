using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    public ItemSO itemData;
    public bool isHeld = false;
    public bool isCooked = false;
    public bool isInfected = false;

    public void Cook()
    {
        isCooked = true;
        Debug.Log($"{itemData.itemName} cocinado.");
    }

    public void Infect()
    {
        isInfected = true;
        Debug.Log($"{itemData.itemName} infectado.");
    }
}
