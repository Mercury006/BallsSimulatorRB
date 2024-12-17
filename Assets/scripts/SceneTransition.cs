using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneTransition : MonoBehaviour
{
    public VideoPlayer videoPlayer;        // Referência ao VideoPlayer
    public string nextSceneName;           // Nome da cena para a qual deseja trocar
    public float delay = 0f;               // Delay adicional após o fim do vídeo (para testes)
    public Animator animator;

    void Start()
    {

        // Adiciona um listener para quando o vídeo terminar
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // Função chamada quando o vídeo termina
    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Vídeo terminou. Trocando de cena após delay...");
        Invoke(nameof(StartFadeOut), delay);
    }

    // Função para trocar de cena
    public void StartFadeOut()
    {
        animator.SetTrigger("FadeOutVideos");
    }
    public void OnFadeCompleteVideos()
    {
        Debug.Log($"Carregando cena '{nextSceneName}'...");
        SceneManager.LoadScene("Bolas");
    }
}
