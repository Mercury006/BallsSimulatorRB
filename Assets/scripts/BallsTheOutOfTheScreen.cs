using UnityEngine;

public class MoveBallsOutOfScreen : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public Vector3 movementAreaMin = new Vector3(-20, -10, 0); // Área mínima
    public Vector3 movementAreaMax = new Vector3(20, 10, 0);   // Área máxima

    [Header("Referências")]
    public ResetAllBalls resetAllBalls; // Referência ao ResetAllBalls para controlar o reset

    void Start()
    {
        // Inicia o movimento das bolas após 1/2 segundo
        Invoke(nameof(MoveBalls), 0.5f);
    }

    public void MoveBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (var ball in balls)
        {
            // Move as bolas aleatoriamente para a área definida fora da tela
            Vector3 randomPosition = new Vector3(
                Random.Range(movementAreaMin.x, movementAreaMax.x),
                Random.Range(movementAreaMin.y, movementAreaMax.y),
                ball.transform.position.z
            );

            ball.transform.position = randomPosition;

            Debug.Log($"Bola movida para: {randomPosition}");
        }

        // Chama o reset após 1 segundos para evitar conflito com o movimento
        Invoke("ActivateReset", 1f);
    }

    void ActivateReset()
    {
        if (resetAllBalls != null)
        {
            resetAllBalls.EnableReset();
            Debug.Log("Reset ativado!");
        }
    }

    // Desenha Gizmos para visualizar a área onde as bolas podem ser movidas
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Desenha um retângulo para representar a área onde as bolas podem ser movidas
        Gizmos.DrawLine(new Vector3(movementAreaMin.x, movementAreaMin.y, 0), new Vector3(movementAreaMax.x, movementAreaMin.y, 0)); // Linha inferior
        Gizmos.DrawLine(new Vector3(movementAreaMin.x, movementAreaMin.y, 0), new Vector3(movementAreaMin.x, movementAreaMax.y, 0)); // Linha esquerda
        Gizmos.DrawLine(new Vector3(movementAreaMax.x, movementAreaMin.y, 0), new Vector3(movementAreaMax.x, movementAreaMax.y, 0)); // Linha direita
        Gizmos.DrawLine(new Vector3(movementAreaMin.x, movementAreaMax.y, 0), new Vector3(movementAreaMax.x, movementAreaMax.y, 0)); // Linha superior
    }
}
