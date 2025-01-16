using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneTransition : MonoBehaviour
{
    public VideoPlayer videoPlayer;        // Referência ao VideoPlayer
    public Animator animator;

    void Start()
    {

        // Adiciona um listener para quando o vídeo terminar
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // Função chamada quando o vídeo termina
    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Vídeo terminou. Trocando de cena ...");
        //StartCoroutine(StartFadeOut(0f));
        StartFadeOut();
    }

    // Função para trocar de cena
    public void StartFadeOut()
    {
        animator.SetTrigger("FadeOutVideos");
        OnFadeCompleteVideos();
    }
    public void OnFadeCompleteVideos()
    {
        Debug.Log("OnFadeCompleteVideos chamado!");//função para chamar o nova scene
        SceneManager.LoadScene("Bolas");
    }
}
