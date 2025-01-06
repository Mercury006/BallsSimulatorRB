using UnityEngine;
using System.Collections.Generic;

public class ResetAllBalls : MonoBehaviour
{
    private List<Transform> ballTransforms = new List<Transform>();
    private List<Vector3> initialPositions = new List<Vector3>();
    private bool isResetting = true; // Por padr�o, o reset cont�nuo est� sempre ativo
    public float resetSpeed; // Intensidade baixa por padr�o

    public bool IsInitialReset { get; private set; } = false; // Flag para identificar o reset inicial
    public bool HasCompleted { get; private set; } = false; // Flag para indicar conclus�o do reset inicial

    void Start()
    {
        // Encontrar bolas com a tag "Ball" e registr�-las automaticamente no in�cio
        GameObject[] initialBalls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in initialBalls)
        {
            RegisterBall(ball.transform);
        }
    }

    // M�todo para registrar uma bola
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
        // Reset manual com a tecla R (ativa/desativa o reset cont�nuo)
        if (Input.GetKeyDown(KeyCode.R) && !IsInitialReset)
        {
            isResetting = !isResetting;
            Debug.Log(isResetting ? "Reset suave ativado!" : "Reset suave desativado!");
        }

        // Executa o reset se estiver ativo e n�o estiver durante o reset inicial
        if (isResetting && !IsInitialReset)
        {
            PerformReset(resetSpeed);
        }
    }

    // M�todo para realizar o reset com a velocidade especificada
    private void PerformReset(float speed)
    {
        for (int i = 0; i < ballTransforms.Count; i++)
        {
            if (ballTransforms[i] == null) continue;

            ballTransforms[i].position = Vector3.Lerp(
                ballTransforms[i].position,
                initialPositions[i],
                speed * Time.deltaTime
            );
        }
    }

    // M�todo para ativar o reset inicial
    public void EnableReset()
    {
        IsInitialReset = true;
        isResetting = true;
        HasCompleted = false;
        resetSpeed = 1.5f;

        Invoke(nameof(DisableInitialReset), 3f);
    }

    // Desliga o reset inicial e permite o reset cont�nuo novamente
    private void DisableInitialReset()
    {
        IsInitialReset = false;
        isResetting = true; // Ativa o reset cont�nuo ap�s o reset inicial
        resetSpeed = 0.05f; // Volta � intensidade baixa ap�s o reset inicial
        HasCompleted = true;

        Debug.Log("Reset inicial conclu�do e desativado.");
    }
}
