using UnityEngine;

public class BallCleaner : MonoBehaviour
{
    public BallCoverageVisualizer ballCoverageVisualizer; // Referência direta no Inspector
    public float pushForce = 10f; // Força com a qual as bolinhas serão empurradas para fora da tela
    public float coverageThreshold = 0.4f; // Percentual de cobertura para ativar o empurrão

    private bool isPushing = false; // Flag para verificar se as bolinhas estão sendo empurradas
    private Vector3 pushDirection; // Direção em que as bolinhas serão empurradas
    private bool hasStartedChecking = false; // Para controlar quando iniciar a verificação

    void Start()
    {
        // Certifique-se de que o script de visualização da cobertura está atribuído
        if (ballCoverageVisualizer == null)
        {
            Debug.LogError("BallCoverageVisualizer não atribuído. Por favor, arraste o script BallCoverageVisualizer para o campo correspondente no Inspector.");
        }

        // Iniciar o atraso para começar a verificar a cobertura
        Invoke(nameof(StartChecking), 10f); // Espera 10 segundos antes de começar a verificar
    }

    void StartChecking()
    {
        hasStartedChecking = true;
        Debug.Log("Iniciando verificação de cobertura.");
    }

    void Update()
    {
        // Somente começa a verificar se já passou o tempo de espera
        if (hasStartedChecking)
        {
            // Verifica a porcentagem de cobertura atingiu o limite
            if (ballCoverageVisualizer != null)
            {
                float coverage = ballCoverageVisualizer.CalculateCoveragePercentage();

                // Começa a empurrar as bolinhas quando a cobertura for inferior ao limite configurado
                if (coverage < coverageThreshold && !isPushing)
                {
                    isPushing = true;
                    Debug.Log($"Cobertura atingiu {coverage * 100}%, empurrando as bolinhas para fora.");
                }

                // Se a cobertura chegar a 2% ou menos, remove todas as bolinhas
                if (coverage <= 0.0001f && ballCoverageVisualizer.GetBalls().Length > 0)
                {
                    RemoveAllBalls();
                }
            }

            if (isPushing)
            {
                PushBallsOutOfScreen();
            }
        }
    }

    // Empurra todas as bolinhas para fora da tela
    void PushBallsOutOfScreen()
    {
        Camera mainCamera = ballCoverageVisualizer.mainCamera; // Usa a câmera principal referenciada no BallCoverageVisualizer

        foreach (GameObject ball in ballCoverageVisualizer.GetBalls())
        {
            if (ball == null) continue;

            // Obtém a posição da bolinha na tela
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(ball.transform.position);

            // Calcula a direção para empurrar a bolinha para fora da tela
            // Aqui, vamos garantir que a direção seja para fora da tela, nunca em direção à câmera
            Vector3 directionToCenter = (screenPosition - new Vector3(Screen.width / 2, Screen.height / 2, 0)).normalized;

            // Direção para empurrar as bolinhas de volta para fora da tela
            Vector3 pushDirection = directionToCenter;

            // Aplica a força para empurrar a bolinha
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }

        // Checa novamente a cobertura da tela após o empurrão
        if (ballCoverageVisualizer.CalculateCoveragePercentage() <= 0f)
        {
            isPushing = false; // Para de empurrar as bolinhas quando a cobertura atingir 0%
            Debug.Log("Cobertura da tela chegou a 0%, fim do empurrão.");
        }
    }

    // Função para remover todas as bolinhas da tela
    void RemoveAllBalls()
    {
        foreach (GameObject ball in ballCoverageVisualizer.GetBalls())
        {
            if (ball != null)
            {
                ball.SetActive(false); // Desativa a bolinha para removê-la da tela
                // Ou, se preferir, pode destruí-la: Destroy(ball);
            }
        }
        Debug.Log("Todas as bolinhas foram removidas.");
    }
}
