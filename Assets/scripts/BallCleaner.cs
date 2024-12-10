using UnityEngine;

public class BallCleaner : MonoBehaviour
{
    public BallCoverageVisualizer ballCoverageVisualizer; // Refer�ncia direta no Inspector
    public float pushForce = 10f; // For�a com a qual as bolinhas ser�o empurradas para fora da tela
    public float coverageThreshold = 0.4f; // Percentual de cobertura para ativar o empurr�o

    private bool isPushing = false; // Flag para verificar se as bolinhas est�o sendo empurradas
    private Vector3 pushDirection; // Dire��o em que as bolinhas ser�o empurradas
    private bool hasStartedChecking = false; // Para controlar quando iniciar a verifica��o

    void Start()
    {
        // Certifique-se de que o script de visualiza��o da cobertura est� atribu�do
        if (ballCoverageVisualizer == null)
        {
            Debug.LogError("BallCoverageVisualizer n�o atribu�do. Por favor, arraste o script BallCoverageVisualizer para o campo correspondente no Inspector.");
        }

        // Iniciar o atraso para come�ar a verificar a cobertura
        Invoke(nameof(StartChecking), 10f); // Espera 10 segundos antes de come�ar a verificar
    }

    void StartChecking()
    {
        hasStartedChecking = true;
        Debug.Log("Iniciando verifica��o de cobertura.");
    }

    void Update()
    {
        // Somente come�a a verificar se j� passou o tempo de espera
        if (hasStartedChecking)
        {
            // Verifica a porcentagem de cobertura atingiu o limite
            if (ballCoverageVisualizer != null)
            {
                float coverage = ballCoverageVisualizer.CalculateCoveragePercentage();

                // Come�a a empurrar as bolinhas quando a cobertura for inferior ao limite configurado
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
        Camera mainCamera = ballCoverageVisualizer.mainCamera; // Usa a c�mera principal referenciada no BallCoverageVisualizer

        foreach (GameObject ball in ballCoverageVisualizer.GetBalls())
        {
            if (ball == null) continue;

            // Obt�m a posi��o da bolinha na tela
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(ball.transform.position);

            // Calcula a dire��o para empurrar a bolinha para fora da tela
            // Aqui, vamos garantir que a dire��o seja para fora da tela, nunca em dire��o � c�mera
            Vector3 directionToCenter = (screenPosition - new Vector3(Screen.width / 2, Screen.height / 2, 0)).normalized;

            // Dire��o para empurrar as bolinhas de volta para fora da tela
            Vector3 pushDirection = directionToCenter;

            // Aplica a for�a para empurrar a bolinha
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }

        // Checa novamente a cobertura da tela ap�s o empurr�o
        if (ballCoverageVisualizer.CalculateCoveragePercentage() <= 0f)
        {
            isPushing = false; // Para de empurrar as bolinhas quando a cobertura atingir 0%
            Debug.Log("Cobertura da tela chegou a 0%, fim do empurr�o.");
        }
    }

    // Fun��o para remover todas as bolinhas da tela
    void RemoveAllBalls()
    {
        foreach (GameObject ball in ballCoverageVisualizer.GetBalls())
        {
            if (ball != null)
            {
                ball.SetActive(false); // Desativa a bolinha para remov�-la da tela
                // Ou, se preferir, pode destru�-la: Destroy(ball);
            }
        }
        Debug.Log("Todas as bolinhas foram removidas.");
    }
}