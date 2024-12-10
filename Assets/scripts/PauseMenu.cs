using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [Header("UI Menus")]
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    [Header("Sliders")]
    public Slider maxDistanceSlider;
    public Slider baseForceSlider;
    public Slider falloffSlider;
    public Slider smoothnessSlider;
    public Slider impulseSlider;
    public Slider resetSpeedSlider;

    [Header("Texts")]
    public TextMeshProUGUI maxDistanceText;
    public TextMeshProUGUI baseForceText;
    public TextMeshProUGUI falloffText;
    public TextMeshProUGUI smoothnessText;
    public TextMeshProUGUI impulseText;
    public TextMeshProUGUI resetSpeedText;

    [Header("Force Field Manager")]
    public ForceFieldManager forceFieldManager;

    [Header("Reset All Balls")]
    public ResetAllBalls resetAllBalls;

    void Start()
    {
        // Carregar as configurações salvas
        LoadSettings();

        // Configurar Listeners para salvar as configurações ao alterar sliders
        maxDistanceSlider.onValueChanged.AddListener(value =>
        {
            forceFieldManager.maxDistance = value;
            UpdateSliderText(maxDistanceText, value);
            SaveSettings();
        });
        baseForceSlider.onValueChanged.AddListener(value =>
        {
            forceFieldManager.baseForceStrength = value;
            UpdateSliderText(baseForceText, value);
            SaveSettings();
        });
        falloffSlider.onValueChanged.AddListener(value =>
        {
            forceFieldManager.falloffStrength = value;
            UpdateSliderText(falloffText, value);
            SaveSettings();
        });
        smoothnessSlider.onValueChanged.AddListener(value =>
        {
            forceFieldManager.smoothness = value;
            UpdateSliderText(smoothnessText, value);
            SaveSettings();
        });
        impulseSlider.onValueChanged.AddListener(value =>
        {
            forceFieldManager.impulseMultiplier = value;
            UpdateSliderText(impulseText, value);
            SaveSettings();
        });
        resetSpeedSlider.onValueChanged.AddListener(value =>
        {
            resetAllBalls.resetSpeed = value;
            UpdateSliderText(resetSpeedText, value);
            SaveSettings();
        });

        // Atualizar os textos iniciais
        UpdateSliderText(maxDistanceText, maxDistanceSlider.value);
        UpdateSliderText(baseForceText, baseForceSlider.value);
        UpdateSliderText(falloffText, falloffSlider.value);
        UpdateSliderText(smoothnessText, smoothnessSlider.value);
        UpdateSliderText(impulseText, impulseSlider.value);
        UpdateSliderText(resetSpeedText, resetSpeedSlider.value);
    }

    void Update()
    {
        // Verificar se a tecla ESC foi pressionada para alternar entre pausar/despausar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resuming game...");
        pauseMenuUI.SetActive(false); // Desativa o menu de pausa
        settingsMenuUI.SetActive(false); // Certifica-se de que as configurações estão fechadas
        Time.timeScale = 1f; // Retorna o tempo ao normal
        GameIsPaused = false; // Atualiza o estado de pausa
    }

    void Pause()
    {
        Debug.Log("Pausing game...");
        pauseMenuUI.SetActive(true); // Ativa o menu de pausa
        settingsMenuUI.SetActive(false); // Certifica-se de que as configurações estão fechadas
        Time.timeScale = 0f; // Pausa o tempo do jogo
        GameIsPaused = true; // Atualiza o estado de pausa
    }

    public void OpenSettings()
    {
        Debug.Log("Opening settings menu...");
        pauseMenuUI.SetActive(false); // Esconde o menu de pausa
        settingsMenuUI.SetActive(true); // Ativa o menu de configurações
    }

    public void BackToPauseMenu()
    {
        Debug.Log("Returning to pause menu...");
        pauseMenuUI.SetActive(true); // Ativa o menu de pausa
        settingsMenuUI.SetActive(false); // Esconde o menu de configurações
    }

    private void UpdateSliderText(TextMeshProUGUI textElement, float value)
    {
        textElement.text = value.ToString("F2"); // Formatar o valor para duas casas decimais
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("MaxDistance", maxDistanceSlider.value);
        PlayerPrefs.SetFloat("BaseForce", baseForceSlider.value);
        PlayerPrefs.SetFloat("Falloff", falloffSlider.value);
        PlayerPrefs.SetFloat("Smoothness", smoothnessSlider.value);
        PlayerPrefs.SetFloat("Impulse", impulseSlider.value);
        PlayerPrefs.SetFloat("ResetSpeed", resetSpeedSlider.value);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        maxDistanceSlider.value = PlayerPrefs.GetFloat("MaxDistance", forceFieldManager.maxDistance);
        baseForceSlider.value = PlayerPrefs.GetFloat("BaseForce", forceFieldManager.baseForceStrength);
        falloffSlider.value = PlayerPrefs.GetFloat("Falloff", forceFieldManager.falloffStrength);
        smoothnessSlider.value = PlayerPrefs.GetFloat("Smoothness", forceFieldManager.smoothness);
        impulseSlider.value = PlayerPrefs.GetFloat("Impulse", forceFieldManager.impulseMultiplier);
        resetSpeedSlider.value = PlayerPrefs.GetFloat("ResetSpeed", resetAllBalls.resetSpeed);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Apenas para teste no Editor
#endif
    }
}
