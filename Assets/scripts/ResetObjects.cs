using UnityEngine;
using System.Collections.Generic;

public class ResetAllBalls : MonoBehaviour
{
    private List<Transform> ballTransforms = new List<Transform>();
    private List<Vector3> initialPositions = new List<Vector3>();
    private bool isResetting = false;
    public float resetSpeed = 0.5f;

    public bool IsInitialReset { get; set; } = false; // Flag para identificar o reset inicial
    public bool HasCompleted { get; private set; } = false; // Flag para indicar conclusão do reset inicial

    void Start()
    {
        // Encontrar bolas com a tag "Ball" e registrá-las automaticamente no início
        GameObject[] initialBalls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in initialBalls)
        {
            RegisterBall(ball.transform);
        }
    }

    // Método para registrar uma bola
    public void RegisterBall(Transform ball)
    {
        if (!ballTransforms.Contains(ball))
        {
            ballTransforms.Add(ball);
            initialPositions.Add(ball.position);
            Debug.Log($"Bola {ball.name} registrada para reset.");
        }
    }

    void Update()
    {
        // Reset manual com a tecla R
        if (Input.GetKeyDown(KeyCode.R) && !IsInitialReset)
        {
            isResetting = !isResetting;
            Debug.Log(isResetting ? "Reset suave ativado!" : "Reset suave desativado!");
        }

        if (isResetting)
        {
            for (int i = 0; i < ballTransforms.Count; i++)
            {
                if (ballTransforms[i] == null) continue;

                ballTransforms[i].position = Vector3.Lerp(
                    ballTransforms[i].position,
                    initialPositions[i],
                    resetSpeed * Time.deltaTime
                );
            }
        }
    }

    // Método para ativar o reset inicial
    public void EnableReset()
    {
        IsInitialReset = true;
        isResetting = true;
        HasCompleted = false;
        resetSpeed = 1f;

        Invoke("DisableInitialReset", 3f); // Desliga o reset inicial após 5 segundos
    }

    // Desliga o reset inicial e permite o reset manual novamente
    private void DisableInitialReset()
    {
        isResetting = false;
        IsInitialReset = false;
        HasCompleted = true;
        Debug.Log("Reset inicial concluído e desativado.");
    }
}
