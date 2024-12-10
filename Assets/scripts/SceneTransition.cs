using UnityEngine;
using UnityEngine.SceneManagement; // Para carregar cenas

public class SceneTransitionOnCoverage : MonoBehaviour
{
    public BallCoverageVisualizer ballCoverageVisualizer; // Referência ao visualizador da cobertura
    public GameObject cubePrefab; // Prefab do cubo a ser instanciado
    public float coverageThreshold = 0f; // Quando a cobertura for igual ou menor que isso, o cubo será spawnado
    public float cooldownTime = 5f; // Tempo de cooldown em segundos

    private bool hasSpawned = false; // Para garantir que apenas um cubo seja spawnado
    private float cooldownTimer = 0f; // Timer para controlar o cooldown

    void Start()
    {
        // Inicia o cooldown quando o script começar
        StartCooldown();
    }

    void Update()
    {
        // Se o cooldown ainda não acabou, decrementa o tempo
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            // Se o cooldown acabou, começamos a verificar a cobertura
            if (ballCoverageVisualizer != null && !hasSpawned)
            {
                float coverage = ballCoverageVisualizer.CalculateCoveragePercentage();

                // Verifica se a cobertura atingiu 0% ou menos
                if (coverage <= coverageThreshold)
                {
                    SpawnCubeAtCenter();
                }
            }
        }
    }

    // Função para spawnar um cubo no centro da tela
    void SpawnCubeAtCenter()
    {
        if (cubePrefab != null)
        {
            // Calcula a posição no centro da tela
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 10f); // Distância da câmera (ajuste conforme necessário)
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenCenter);

            // Cria o cubo na posição central
            Instantiate(cubePrefab, worldPosition, Quaternion.identity);
            Debug.Log("Cubo spawnado no centro da tela.");

            // Marca que o cubo foi spawnado e não deve ser mais spawnado
            hasSpawned = true;
        }
        else
        {
            Debug.LogError("Prefab do cubo não atribuído. Atribua um prefab de cubo no Inspector.");
        }
    }

    // Função para iniciar o cooldown e começar a detecção
    public void StartCooldown()
    {
        cooldownTimer = cooldownTime; // Reseta o timer de cooldown
        hasSpawned = false; // Garante que o cubo será spawnado
        Debug.Log("Cooldown iniciado. Aguardando 5 segundos para começar a detecção...");
    }

    // Função para trocar de cena (a ser chamada mais tarde)
    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Carrega a cena com o nome especificado
    }
}
