using UnityEngine;
using System.Collections;

public class ProgramStart : MonoBehaviour
{
    [Header("Refer�ncias")]
    public MoveBallsOutOfScreen moveBallsOutOfScreen;
    public ResetAllBalls resetAllBalls;
    public BallCleaner ballCleaner;

    [Header("Canvas de Tela Branca")]
    public CanvasGroup whiteCanvasGroup; // Refer�ncia ao CanvasGroup do Canvas branco

    [Header("Configura��es")]
    public float fadeDuration = 0.3f; // Dura��o do fade-out em segundos

    void Start()
    {
        Debug.Log("Iniciando a sequ�ncia...");

        //Invoke(nameof(MoveBallsOut), 0.01f);
        MoveBallsOut();

        // Ativa o reset ap�s 4 segundos
        Invoke(nameof(ActivateReset), 3f);

        // Ativa o BallCleaner ap�s 14 segundos (4s + 10s)
        Invoke(nameof(ActivateBallCleaner), 10f);
    }

    void MoveBallsOut()
    {
        if (moveBallsOutOfScreen != null)
        {
            moveBallsOutOfScreen.MoveBalls(); // Move as bolas para fora da tela
            Debug.Log("Bolas movidas para fora da tela.");
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
