using UnityEngine;
using UnityEngine.UI;         // Para UI Text
using UnityEngine.Video;      // Para VideoPlayer

public class VideoTimeDisplay : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Referência ao VideoPlayer
    public Text timeText;            // Referência ao componente UI Text para exibir o tempo

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
}
