using UnityEngine;
using System.Collections.Generic;

public class ResetAllBalls : MonoBehaviour
{
    private List<Transform> ballTransforms = new List<Transform>();
    private List<Vector3> initialPositions = new List<Vector3>();
    private bool isResetting = true; // Por padrão, o reset contínuo está sempre ativo
    public float resetSpeed; // Intensidade baixa por padrão

    public bool IsInitialReset { get; private set; } = false; // Flag para identificar o reset inicial
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
        // Reset manual com a tecla R (ativa/desativa o reset contínuo)
        if (Input.GetKeyDown(KeyCode.R) && !IsInitialReset)
        {
            isResetting = !isResetting;
            Debug.Log(isResetting ? "Reset suave ativado!" : "Reset suave desativado!");
        }

        // Executa o reset se estiver ativo e não estiver durante o reset inicial
        if (isResetting && !IsInitialReset)
        {
            PerformReset(resetSpeed);
        }
    }

    // Método para realizar o reset com a velocidade especificada
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

    // Método para ativar o reset inicial
    public void EnableReset()
    {
        IsInitialReset = true;
        isResetting = true;
        HasCompleted = false;
        resetSpeed = 1f;

        Invoke(nameof(DisableInitialReset), 3f);
    }

    // Desliga o reset inicial e permite o reset contínuo novamente
    private void DisableInitialReset()
    {
        IsInitialReset = false;
        isResetting = true; // Ativa o reset contínuo após o reset inicial
        resetSpeed = 0.05f; // Volta à intensidade baixa após o reset inicial
        HasCompleted = true;

        Debug.Log("Reset inicial concluído e desativado.");
    }
}
