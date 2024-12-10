using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ForceFieldManager : MonoBehaviour
{
    [Header("UI Sliders")]
    public Slider maxDistanceSlider;
    public Slider baseForceSlider;
    public Slider falloffSlider;
    public Slider smoothnessSlider;
    public Slider impulseSlider;

    [Header("Configurações")]
    public float maxDistance = 5f;
    public float baseForceStrength = 5f;
    public float falloffStrength = 10f;
    public float smoothness = 0.5f;
    public float impulseMultiplier = 2f;

    private List<GameObject> ballList = new List<GameObject>();

    void Start()
    {
        GameObject[] initialBalls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in initialBalls)
        {
            RegisterBall(ball);
        }

        maxDistanceSlider.value = maxDistance;
        baseForceSlider.value = baseForceStrength;
        falloffSlider.value = falloffStrength;
        smoothnessSlider.value = smoothness;
        impulseSlider.value = impulseMultiplier;

        maxDistanceSlider.onValueChanged.AddListener(value => maxDistance = value);
        baseForceSlider.onValueChanged.AddListener(value => baseForceStrength = value);
        falloffSlider.onValueChanged.AddListener(value => falloffStrength = value);
        smoothnessSlider.onValueChanged.AddListener(value => smoothness = value);
        impulseSlider.onValueChanged.AddListener(value => impulseMultiplier = value);
    }

    public void RegisterBall(GameObject ball)
    {
        if (!ballList.Contains(ball))
        {
            ballList.Add(ball);
            Debug.Log($"Bola {ball.name} registrada no ForceFieldManager.");
        }
    }

    void Update()
    {
        foreach (GameObject ball in ballList)
        {
            if (ball == null) continue;

            MouseDragForceField forceField = ball.GetComponent<MouseDragForceField>();
            if (forceField != null)
            {
                forceField.UpdateProperties(maxDistance, baseForceStrength, falloffStrength, smoothness, impulseMultiplier);
            }
        }
    }
}
