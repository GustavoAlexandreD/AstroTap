using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("Painel de Pause")]
    [SerializeField] private GameObject pausePanel;

    [Header("Botão de Áudio")]
    [SerializeField] private Button botaoAudio;
    [SerializeField] private Image iconeAudio;
    [SerializeField] private Sprite spriteAudioLigado;
    [SerializeField] private Sprite spriteAudioDesligado;

    [Header("Botão Pause/Play")]
    [SerializeField] private Image iconePausePlay;
    [SerializeField] private Sprite spritePause; 
    [SerializeField] private Sprite spritePlay;  

    private const string AUDIO_PREF_KEY = "AUDIO_LIGADO";
    private bool audioLigado;

    private void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Carregar estado do áudio
        CarregarEstadoAudio();
        AtualizarVisualBotao();

        if (botaoAudio != null)
        {
            botaoAudio.onClick.AddListener(ToggleAudio);
        }

        AtualizarIconePausePlay();
    }

    private void Update()
    {
        // Novo Input System
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void Pausar()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);

        GameManager.Instance.PausarJogo();
        AtualizarIconePausePlay();
    }

    public void Retomar()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        GameManager.Instance.RetomarJogo();
        AtualizarIconePausePlay();
    }

    public void TogglePause()
    {
        if (GameManager.Instance.EstadoAtual == GameState.Playing)
        {
            Pausar();
        }
        else if (GameManager.Instance.EstadoAtual == GameState.Paused)
        {
            Retomar();
        }
    }

    public void SairDoJogo()
    {
        Debug.Log("Jogador solicitou sair do jogo.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SairDoJogo();
        }
        else
        {
            Debug.LogWarning("GameManager.Instance não encontrado ao tentar sair do jogo.");
        }
    }

    public void Instagram()
    {
        Debug.Log("Abrindo Instagram do desenvolvedor.");
        Application.OpenURL("https://www.instagram.com/gustavo_alexandred/");
    }

    private void AtualizarIconePausePlay()
    {
        if (iconePausePlay == null || GameManager.Instance == null) return;

        if (GameManager.Instance.EstadoAtual == GameState.Paused)
        {
            if (spritePlay != null)
                iconePausePlay.sprite = spritePlay;
        }
        else
        {
            if (spritePause != null)
                iconePausePlay.sprite = spritePause;
        }
    }

    #region Áudio

    private void ToggleAudio()
    {
        audioLigado = !audioLigado;

        PlayerPrefs.SetInt(AUDIO_PREF_KEY, audioLigado ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Áudio agora está: " + (audioLigado ? "LIGADO" : "DESLIGADO"));

        AtualizarVisualBotao();

        // Futuro:
        // AudioManager.Instance.AtualizarEstadoAudio(audioLigado);
    }

    private void CarregarEstadoAudio()
    {
        audioLigado = PlayerPrefs.GetInt(AUDIO_PREF_KEY, 1) == 1;
    }

    private void AtualizarVisualBotao()
    {
        if (iconeAudio == null) return;

        iconeAudio.sprite = audioLigado
            ? spriteAudioLigado
            : spriteAudioDesligado;
    }

    #endregion
}