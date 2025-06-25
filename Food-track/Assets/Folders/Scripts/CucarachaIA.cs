using UnityEngine;
using UnityEngine.AI;

public class CucarachaAI : MonoBehaviour
{
    public enum Estado { Buscando, Comiendo, Muerta }
    public Estado estadoActual = Estado.Buscando;

    [Header("Detección")]
    public float rangoDeteccion = 5f;
    public LayerMask capaComida;
    public string tagComida = "Comida";

    [Header("NavMesh")]
    private NavMeshAgent agent;
    private Transform objetivoComida;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Debug.Log($"[{name}] Estado inicial: {estadoActual}");
    }

    private void Update()
    {
        if (estadoActual == Estado.Muerta) return;

        switch (estadoActual)
        {
            case Estado.Buscando:
                BuscarComida();
                break;

            case Estado.Comiendo:
                agent.isStopped = true;
                break;
        }
    }

    private void BuscarComida()
    {
        Collider[] objetosDetectados = Physics.OverlapSphere(transform.position, rangoDeteccion, capaComida);

        foreach (var objeto in objetosDetectados)
        {
            // Verifica que tenga el tag correcto y esté en la capa "Ingredient"
            if (objeto.CompareTag(tagComida) && objeto.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
            {
                objetivoComida = objeto.transform;
                agent.SetDestination(objeto.transform.position);
                Debug.Log($"[{name}] Comida válida detectada (tag+layer): {objeto.name}");
                StartCoroutine(EsperarAlLlegar(objeto.transform.position));
                return;
            }
        }
    }


    private System.Collections.IEnumerator EsperarAlLlegar(Vector3 destino)
    {
        while (estadoActual != Estado.Muerta && Vector3.Distance(transform.position, destino) > 0.3f)
        {
            yield return null;
        }

        if (estadoActual != Estado.Muerta)
        {
            CambiarEstado(Estado.Comiendo);
        }
    }

    private void CambiarEstado(Estado nuevoEstado)
    {
        if (estadoActual == Estado.Muerta) return;

        if (estadoActual != nuevoEstado)
        {
            Debug.Log($"[{name}] Cambio de estado: {estadoActual} → {nuevoEstado}");
            estadoActual = nuevoEstado;
        }

        switch (nuevoEstado)
        {
            case Estado.Comiendo:
                agent.isStopped = true;
                break;

            case Estado.Muerta:
                agent.isStopped = true;
                Debug.Log($"[{name}] ¡Cucaracha murió!");
                Destroy(gameObject);
                break;
        }
    }

    private void OnMouseDown()
    {
        Morir();
    }

    private void Morir()
    {
        CambiarEstado(Estado.Muerta);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}
