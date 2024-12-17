using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SmoothTransition : MonoBehaviour
{
    public Animator animator;

    public void ChangeScene()
    {
        animator.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene("Video Interacao");
    }

}
