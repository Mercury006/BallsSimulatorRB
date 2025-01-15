using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneTransition : MonoBehaviour
{
    public VideoPlayer videoPlayer;        // Refer�ncia ao VideoPlayer
    public float delay = 0f;               // Delay adicional ap�s o fim do v�deo (para testes)
    public Animator animator;

    void Start()
    {

        // Adiciona um listener para quando o v�deo terminar
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // Fun��o chamada quando o v�deo termina
    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("V�deo terminou. Trocando de cena ap�s delay...");
        //StartCoroutine(StartFadeOut(0f));
        StartFadeOut();
    }

    // Fun��o para trocar de cena
    public void StartFadeOut()
    {
        animator.SetTrigger("FadeOutVideos");
    }
    public void OnFadeCompleteVideos()
    {
        SceneManager.LoadScene("Bolas");
    }
}
