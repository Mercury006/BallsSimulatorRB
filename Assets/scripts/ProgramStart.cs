using UnityEngine;

public class ProgramStart : MonoBehaviour
{
    [Header("Referências")]
    public MoveBallsOutOfScreen moveBallsOutOfScreen;
    public ResetAllBalls resetAllBalls;
    public BallCleaner ballCleaner;

    [Header("Canvas de Tela Branca")]
    public GameObject whiteCanvas; // Referência ao Canvas branco que cobre a tela

    void Start()
    {
        Debug.Log("Iniciando a sequência...");

        // Torna o Canvas branco visível ao iniciar
        if (whiteCanvas != null)
        {
            whiteCanvas.SetActive(true); // Exibe o Canvas branco
        }

        // Move as bolas para fora da tela após 1 segundo
        Invoke(nameof(MoveBallsOut), 1f);

        // Ativa o reset após 4 segundos
        Invoke(nameof(ActivateReset), 4f);

        // Ativa o BallCleaner após 14 segundos (4s + 10s)
        Invoke(nameof(ActivateBallCleaner), 14f);
    }

    void MoveBallsOut()
    {
        if (moveBallsOutOfScreen != null)
        {
            moveBallsOutOfScreen.MoveBalls(); // Move as bolas para fora da tela
            Debug.Log("Bolas movidas para fora da tela.");

            // Desativa o Canvas branco assim que as bolas forem movidas
            if (whiteCanvas != null)
            {
                whiteCanvas.SetActive(false); // Oculta o Canvas branco
            }
        }
    }

    void ActivateReset()
    {
        if (resetAllBalls != null)
        {
            resetAllBalls.EnableReset();
            Debug.Log("Ativando o reset das bolas.");
        }
    }

    void ActivateBallCleaner()
    {
        if (ballCleaner != null)
        {
            ballCleaner.enabled = true;
            Debug.Log("BallCleaner ativado.");
        }
    }
}
