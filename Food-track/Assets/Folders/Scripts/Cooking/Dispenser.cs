using System.Collections;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [Header("Ítem a instanciar")]
    [SerializeField] private GameObject itemToDispensePrefab;

    [Header("Cooldown entre ítems")]
    [SerializeField] private float cooldownTime = 0.25f;
    private bool isCoolingDown = false;

    [Header("Sonido")]
    public AudioSource audioSource;
    public AudioClip sfxDispenseItem;

    // Método para activar el dispenser
    public void Dispense()
    {
        if (isCoolingDown) return;

        CreateItem();

        if (sfxDispenseItem != null && audioSource != null)
            audioSource.PlayOneShot(sfxDispenseItem);

        StartCoroutine(StartCooldown());
    }

    private GameObject CreateItem()
    {
        if (itemToDispensePrefab == null)
        {
            Debug.LogError("❌ Dispenser sin prefab asignado.");
            return null;
        }

        Vector3 spawnPos = transform.position + Vector3.up * 0.5f;
        return Instantiate(itemToDispensePrefab, spawnPos, Quaternion.identity);
    }

    private IEnumerator StartCooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCoolingDown = false;
    }
}
