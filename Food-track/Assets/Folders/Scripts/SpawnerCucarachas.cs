using UnityEngine;
using UnityEngine.AI;

public class SpawnerCucarachas : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject prefabCucaracha;
    public int cantidadInicial = 10;
    public Vector3 areaSpawn = new Vector3(5, 0, 5);

    [Header("NavMesh")]
    public float maxOffsetAltura = 2f;

    private void Start()
    {
        for (int i = 0; i < cantidadInicial; i++)
        {
            SpawnAleatorio();
        }
    }

    void SpawnAleatorio()
    {
        for (int intentos = 0; intentos < 20; intentos++)
        {
            Vector3 puntoAleatorio = transform.position + new Vector3(
                Random.Range(-areaSpawn.x, areaSpawn.x),
                0,
                Random.Range(-areaSpawn.z, areaSpawn.z)
            );

            if (NavMesh.SamplePosition(puntoAleatorio, out NavMeshHit hit, maxOffsetAltura, NavMesh.AllAreas))
            {
                GameObject nuevaCuca = Instantiate(prefabCucaracha, hit.position, Quaternion.identity);
                nuevaCuca.name = $"Cucaracha_{Random.Range(1000, 9999)}";
                return;
            }
        }

        Debug.LogWarning("No se encontró una posición válida sobre el NavMesh para una cucaracha.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, areaSpawn * 2);
    }
}
