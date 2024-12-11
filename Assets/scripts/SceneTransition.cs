using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public BallCoverageVisualizer ballCoverageVisualizer;
    public float coverageThreshold = 0.02f;
    public float cooldownTime = 5f;
    public float sceneTransitionDelay = 2f;

    private void Update()
    {
        if (ballCoverageVisualizer == null)
        {
            Debug.LogError("BallCoverageVisualizer não está atribuído. Verifique o Inspector.");
            return;
        }


        float coverage = ballCoverageVisualizer.CalculateCoveragePercentage();
        Debug.Log($"Cobertura atual: {coverage * 100}%");

        if (coverage <= coverageThreshold)
        {
            ChangeScene();
        }
    }


    public void ChangeScene()
    {
        Debug.Log("Tentando carregar a cena 'Video Interacao'...");
        SceneManager.LoadScene("Video Interacao");
    }
}
