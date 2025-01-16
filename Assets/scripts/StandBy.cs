using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.SceneManagement; // Para reiniciar a cena

public class StandBy : MonoBehaviour
{
    public VideoPlayer videoPlayer;          // Refer�ncia ao componente VideoPlayer
    public RawImage videoDisplay;            // RawImage para exibir o v�deo na tela
    public CanvasGroup canvasGroup;          // CanvasGroup para controlar o fade in/out
    public GameObject videoDisplayObject;    // GameObject que cont�m o RawImage
    public float idleTime = 10f;             // Tempo de inatividade (10 segundos para testes)
    public float mouseMovementThreshold = 0.1f; // Toler�ncia para movimento do mouse

    private float timer;                     // Cron�metro de inatividade
    private bool isStandBy = false;          // Estado do modo standby
    private bool standbyEnabled = true;      // Controle se o standby est� ativo ou n�o
    private Vector3 lastMousePosition;       // �ltima posi��o conhecida do mouse

    void Start()
    {
        timer = 0f;
        canvasGroup.alpha = 0f;              // Garante que o v�deo come�a invis�vel
        videoDisplayObject.SetActive(false); // Desativa o GameObject do v�deo no in�cio
        videoPlayer.SetDirectAudioVolume(0, 0f); // Inicia com o volume zerado
        lastMousePosition = Input.mousePosition; // Inicializa a posi��o do mouse
        EnableStandBy();                     // Ativa o standby no in�cio da cena
    }

    void Update()
    {
        if (!standbyEnabled) return; // Se standby estiver desativado, n�o faz nada

        // Calcula a dist�ncia do movimento do mouse
        float mouseMovement = Vector3.Distance(Input.mousePosition, lastMousePosition);

        if (mouseMovement > mouseMovementThreshold)
        {
            ResetTimer();
            if (isStandBy)
            {
                StopStandBy();
            }
        }
        else
        {
            // Incrementa o cron�metro se o mouse n�o se mover acima da toler�ncia
            timer += Time.deltaTime;

            Debug.Log($"Mouse parado por {timer:F2} segundos");

            // Ativa o modo standby ap�s o tempo definido
            if (timer >= idleTime && !isStandBy)
            {
                StartStandBy();
            }
        }

        // Atualiza a �ltima posi��o do mouse
        lastMousePosition = Input.mousePosition;
    }

    void StartStandBy()
    {
        isStandBy = true;
        videoDisplayObject.SetActive(true);
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoFinished; // Adiciona o listener para quando o v�deo termina
        Debug.Log("Entrando no modo StandBy");
        StartCoroutine(FadeIn());
    }

    void StopStandBy()
    {
        isStandBy = false;
        videoPlayer.loopPointReached -= OnVideoFinished; // Remove o listener
        Debug.Log("Saindo do modo StandBy");
        StartCoroutine(FadeOut());
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("V�deo terminou. Saindo do modo StandBy.");
        StopStandBy();
        ResetTimer();
    }

    void ResetTimer()
    {
        Debug.Log("Mouse em movimento. Resetando o cron�metro.");
        timer = 0f;
    }

    IEnumerator FadeIn()
    {
        float duration = 1.5f; // Dura��o do fade in
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            videoPlayer.SetDirectAudioVolume(0, Mathf.Lerp(0f, 1f, t));
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
        videoPlayer.SetDirectAudioVolume(0, 1f);
    }

    IEnumerator FadeOut()
    {
        float duration = 1.5f; // Dura��o do fade out
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
            videoPlayer.SetDirectAudioVolume(0, Mathf.Lerp(1f, 0f, t));
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        videoPlayer.SetDirectAudioVolume(0, 0f);
        videoDisplayObject.SetActive(false);
        videoPlayer.Stop();
    }

    // Fun��o para desligar o standby (para chamar em outras cenas)
    public void DisableStandBy()
    {
        standbyEnabled = false;
        StopStandBy();
        Debug.Log("StandBy foi desativado.");
    }

    // Fun��o para reativar o standby
    public void EnableStandBy()
    {
        standbyEnabled = true;
        ResetTimer();
        Debug.Log("StandBy foi reativado.");
    }

    // Exemplo de fun��o para reiniciar a cena
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
