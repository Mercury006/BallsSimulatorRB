using UnityEngine;
using System.Collections;

public class BallCoverageVisualizer : MonoBehaviour
{
    [Header("Configuração das Bolinhas")]
    public GameObject[] balls; // Agora a variável é pública

    public Camera mainCamera; // Referência à câmera principal

    [Header("Exibição da Cobertura")]
    public bool showCoverage = true; // Flag para mostrar a cobertura no console

    void Start()
    {
        // Inicia a corrotina que aguarda 2 segundos antes de buscar as bolinhas
        StartCoroutine(FindBallsAfterDelay(2f));
    }

    // Corrotina para aguardar 2 segundos e depois encontrar as bolinhas pela tag
    IEnumerator FindBallsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Encontrar as bolinhas pela tag 'Ball'
        balls = GameObject.FindGameObjectsWithTag("Ball");

        Debug.Log($"Encontradas {balls.Length} bolinhas.");
    }

    void Update()
    {
        if (balls != null && balls.Length > 0)
        {
            float coverage = CalculateCoveragePercentage();
            if (showCoverage)
            {
                Debug.Log($"Cobertura da tela por bolinhas: {coverage * 100}%");
            }
        }
        else
        {
            Debug.Log("Nenhuma bolinha encontrada.");
        }
    }

    // Função que calcula a porcentagem da tela coberta pelas bolinhas
    public float CalculateCoveragePercentage()
    {
        if (balls == null || balls.Length == 0 || mainCamera == null)
            return 0f;

        int ballsInViewport = 0;

        foreach (GameObject ball in balls)
        {
            if (ball == null) continue;

            // Convertendo a posição da bolinha para coordenadas da tela
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(ball.transform.position);

            // Verifica se a bolinha está dentro da tela
            if (screenPosition.x >= 0 && screenPosition.x <= Screen.width &&
                screenPosition.y >= 0 && screenPosition.y <= Screen.height)
            {
                ballsInViewport++;
            }
        }

        // Calcula a porcentagem da tela coberta pelas bolinhas
        float coverage = (float)ballsInViewport / balls.Length;
        return coverage;
    }

    public GameObject[] GetBalls()
    {
        return balls;
    }
}