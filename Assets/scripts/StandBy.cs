using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.SceneManagement; // Para reiniciar a cena

public class StandBy : MonoBehaviour
{
    public VideoPlayer videoPlayer;          // Referência ao componente VideoPlayer
    public RawImage videoDisplay;            // RawImage para exibir o vídeo na tela
    public CanvasGroup canvasGroup;          // CanvasGroup para controlar o fade in/out
    public GameObject videoDisplayObject;    // GameObject que contém o RawImage
    public float idleTime = 10f;             // Tempo de inatividade (10 segundos para testes)
    public float mouseMovementThreshold = 0.1f; // Tolerância para movimento do mouse

    private float timer;                     // Cronômetro de inatividade
    private bool isStandBy = false;          // Estado do modo standby
    private bool standbyEnabled = true;      // Controle se o standby está ativo ou não
    private Vector3 lastMousePosition;       // Última posição conhecida do mouse

    void Start()
    {
        timer = 0f;
        canvasGroup.alpha = 0f;              // Garante que o vídeo começa invisível
        videoDisplayObject.SetActive(false); // Desativa o GameObject do vídeo no início
        videoPlayer.SetDirectAudioVolume(0, 0f); // Inicia com o volume zerado
        lastMousePosition = Input.mousePosition; // Inicializa a posição do mouse
        EnableStandBy();                     // Ativa o standby no início da cena
    }

    void Update()
    {
        if (!standbyEnabled) return; // Se standby estiver desativado, não faz nada

        // Calcula a distância do movimento do mouse
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
            // Incrementa o cronômetro se o mouse não se mover acima da tolerância
            timer += Time.deltaTime;

            Debug.Log($"Mouse parado por {timer:F2} segundos");

            // Ativa o modo standby após o tempo definido
            if (timer >= idleTime && !isStandBy)
            {
                StartStandBy();
            }
        }

        // Atualiza a última posição do mouse
        lastMousePosition = Input.mousePosition;
    }

    void StartStandBy()
    {
        isStandBy = true;
        videoDisplayObject.SetActive(true);
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoFinished; // Adiciona o listener para quando o vídeo termina
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
        Debug.Log("Vídeo terminou. Saindo do modo StandBy.");
        StopStandBy();
        ResetTimer();
    }

    void ResetTimer()
    {
        Debug.Log("Mouse em movimento. Resetando o cronômetro.");
        timer = 0f;
    }

    IEnumerator FadeIn()
    {
        float duration = 1.5f; // Duração do fade in
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
        float duration = 1.5f; // Duração do fade out
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

    // Função para desligar o standby (para chamar em outras cenas)
    public void DisableStandBy()
    {
        standbyEnabled = false;
        StopStandBy();
        Debug.Log("StandBy foi desativado.");
    }

    // Função para reativar o standby
    public void EnableStandBy()
    {
        standbyEnabled = true;
        ResetTimer();
        Debug.Log("StandBy foi reativado.");
    }

    // Exemplo de função para reiniciar a cena
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
