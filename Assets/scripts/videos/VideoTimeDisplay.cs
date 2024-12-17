using UnityEngine;
using UnityEngine.UI;         // Para UI Text
using UnityEngine.Video;      // Para VideoPlayer
using System.Collections;    // Para usar Coroutine

public class VideoTimeDisplay : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Referência ao VideoPlayer
    public Text timeText;            // Referência ao componente UI Text para exibir o tempo
    public VideoClip[] videoClips;   // Array de VideoClips para selecionar aleatoriamente
    public CanvasGroup canvasGroup;  // Referência ao CanvasGroup para controle de fade (se usar UI)
    public float fadeDuration = 0f;  // Duração do fade in

    void Start()
    {
        // Verifica se o VideoPlayer e os outros componentes estão atribuídos
        if (videoPlayer == null || timeText == null || videoClips.Length == 0)
        {
            Debug.LogError("Referências não atribuídas corretamente. Verifique o Inspector.");
            return;
        }
         
        // Seleciona um vídeo aleatório da lista de VideoClips
        VideoClip randomVideoClip = videoClips[Random.Range(0, videoClips.Length)];

        // Define o VideoClip no VideoPlayer
        videoPlayer.clip = randomVideoClip;

        // Inicia o fade in do vídeo
        //StartCoroutine(FadeInVideo());

        // Inicia a reprodução do vídeo
        videoPlayer.Play();
    }

    void Update()
    {
        if (videoPlayer == null || timeText == null)
        {
            Debug.LogError("VideoPlayer ou timeText não estão atribuídos. Verifique o Inspector.");
            return;
        }

        // Verifica se o vídeo está preparado para reproduzir
        if (videoPlayer.isPlaying)
        {
            // Calcula o tempo atual e a duração do vídeo em segundos
            double currentTime = videoPlayer.time;
            double duration = videoPlayer.length;

            // Atualiza o texto com o tempo formatado
            timeText.text = FormatTime(currentTime) + " / " + FormatTime(duration);
        }
    }

    // Função para formatar o tempo em minutos e segundos (MM:SS)
    string FormatTime(double timeInSeconds)
    {
        int minutes = Mathf.FloorToInt((float)timeInSeconds / 60);
        int seconds = Mathf.FloorToInt((float)timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Coroutine para realizar o fade in
   /* IEnumerator FadeInVideo()
    {
        float elapsedTime = 0f;

        // Começa com o canvasGroup invisível (alpha 0)
        canvasGroup.alpha = 0f;

        // Executa o fade in durante o tempo especificado
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assegura que a opacidade final será 1
        canvasGroup.alpha = 1f;
    }*/
}
