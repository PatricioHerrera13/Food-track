using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ChoriExpress/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public GameObject prefab;
    public float valor;
    public bool isConsumable;

    [Header("Estados de cocción (si aplica)")]
    public ItemSO cocido;
    public ItemSO quemado;
}
