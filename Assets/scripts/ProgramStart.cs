using UnityEngine;
using System.Collections;

public class ProgramStart : MonoBehaviour
{
    [Header("Referências")]
    public MoveBallsOutOfScreen moveBallsOutOfScreen;
    public ResetAllBalls resetAllBalls;
    public BallCleaner ballCleaner;

    [Header("Canvas de Tela Branca")]
    public CanvasGroup whiteCanvasGroup; // Referência ao CanvasGroup do Canvas branco

    [Header("Configurações")]
    public float fadeDuration = 0.3f; // Duração do fade-out em segundos

    void Start()
    {
        Debug.Log("Iniciando a sequência...");

        // Torna o Canvas branco visível ao iniciar
        if (whiteCanvasGroup != null)
        {
            whiteCanvasGroup.gameObject.SetActive(true); // Garante que o Canvas está ativo
            whiteCanvasGroup.alpha = 1f; // Define a opacidade inicial como 100%
        }

        // Move as bolas para fora da tela após 1 segundo
        Invoke(nameof(MoveBallsOut), 0.01f);

        // Ativa o reset após 4 segundos
        Invoke(nameof(ActivateReset), 3f);

        // Ativa o BallCleaner após 14 segundos (4s + 10s)
        Invoke(nameof(ActivateBallCleaner), 10f);
    }

    void MoveBallsOut()
    {
        if (moveBallsOutOfScreen != null)
        {
            moveBallsOutOfScreen.MoveBalls(); // Move as bolas para fora da tela
            Debug.Log("Bolas movidas para fora da tela.");

            // Inicia o fade-out do Canvas branco
            if (whiteCanvasGroup != null)
            {
                StartCoroutine(FadeOutCanvas());
            }
        }
    }

    IEnumerator FadeOutCanvas()
    {
        float startAlpha = whiteCanvasGroup.alpha;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            whiteCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, timeElapsed / fadeDuration);
            yield return null;
        }

        whiteCanvasGroup.alpha = 0f; // Garante que a opacidade final é zero
        whiteCanvasGroup.gameObject.SetActive(false); // Desativa o Canvas após o fade-out
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
