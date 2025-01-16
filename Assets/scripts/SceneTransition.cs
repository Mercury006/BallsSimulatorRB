using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneTransition : MonoBehaviour
{
    public VideoPlayer videoPlayer;        // Refer�ncia ao VideoPlayer
    public Animator animator;

    void Start()
    {

        // Adiciona um listener para quando o v�deo terminar
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // Fun��o chamada quando o v�deo termina
    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("V�deo terminou. Trocando de cena ...");
        //StartCoroutine(StartFadeOut(0f));
        StartFadeOut();
    }

    // Fun��o para trocar de cena
    public void StartFadeOut()
    {
        animator.SetTrigger("FadeOutVideos");
        OnFadeCompleteVideos();
    }
    public void OnFadeCompleteVideos()
    {
        Debug.Log("OnFadeCompleteVideos chamado!");//fun��o para chamar o nova scene
        SceneManager.LoadScene("Bolas");
    }
}
