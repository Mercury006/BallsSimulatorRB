using UnityEngine;

public class SpawningBalls : MonoBehaviour
{
    [Header("Configurações de Spawn")]
    public GameObject ballPrefab;
    public int numberOfBalls = 100;
    public Vector3 spawnAreaSize = new Vector3(10, 10, 10);
    public Vector2 ballSizeRange = new Vector2(0.5f, 2f);

    [Header("Referências")]
    public ForceFieldManager forceFieldManager;
    public ResetAllBalls resetAllBalls;

    void Start()
    {
        // Usando FindFirstObjectByType para encontrar os componentes na cena
        if (forceFieldManager == null)
        {
            forceFieldManager = Object.FindFirstObjectByType<ForceFieldManager>();
        }

        if (resetAllBalls == null)
        {
            resetAllBalls = Object.FindFirstObjectByType<ResetAllBalls>();
        }

        // Se ainda não foi encontrado, pode-se colocar um log para informar que o objeto está faltando
        if (forceFieldManager == null)
        {
            Debug.LogError("ForceFieldManager não encontrado na cena!");
        }
        if (resetAllBalls == null)
        {
            Debug.LogError("ResetAllBalls não encontrado na cena!");
        }

        SpawnBalls();
    }

    public void SpawnBalls()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            Vector3 randomPosition = transform.position + new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            GameObject newBall = Instantiate(ballPrefab, randomPosition, Quaternion.identity);

            // Gerar tamanho aleatório para a bola
            float randomScale = Random.Range(ballSizeRange.x, ballSizeRange.y);
            newBall.transform.localScale = Vector3.one * randomScale;

            // Adiciona Rigidbody se não tiver
            if (newBall.GetComponent<Rigidbody>() == null)
            {
                newBall.AddComponent<Rigidbody>().useGravity = false;
            }

            // Adiciona SphereCollider se não tiver
            if (newBall.GetComponent<SphereCollider>() == null)
            {
                newBall.AddComponent<SphereCollider>();
            }

            // Adiciona MouseDragForceField se não tiver
            if (newBall.GetComponent<MouseDragForceField>() == null)
            {
                newBall.AddComponent<MouseDragForceField>();
            }

            // Registra a bola nos sistemas
            if (forceFieldManager != null)
            {
                forceFieldManager.RegisterBall(newBall);
            }

            if (resetAllBalls != null)
            {
                resetAllBalls.RegisterBall(newBall.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
