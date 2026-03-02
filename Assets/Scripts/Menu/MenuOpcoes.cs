using UnityEngine;
using UnityEngine.UI;

public class MenuOpcoes : MonoBehaviour
{
    [Header("Botão de Áudio")]
    [SerializeField] private Button botaoAudio;
    [SerializeField] private Image iconeAudio;
    [SerializeField] private Sprite spriteAudioLigado;
    [SerializeField] private Sprite spriteAudioDesligado;

    [Header("Links Externos")]
    [SerializeField] private string urlInstagram = "https://www.instagram.com/gustavo_alexandred/";
    [SerializeField] private string emailSuporte = "gustavo.alexandre.tech@gmail.com";

    private const string AUDIO_PREF_KEY = "AUDIO_LIGADO";

    private bool audioLigado;

    private void Awake()
    {
        CarregarEstadoAudio();
        AtualizarVisualBotao();
        botaoAudio.onClick.AddListener(ToggleAudio);
    }

    #region Áudio

    private void ToggleAudio()
    {
        audioLigado = !audioLigado;

        PlayerPrefs.SetInt(AUDIO_PREF_KEY, audioLigado ? 1 : 0);
        PlayerPrefs.Save();

        AtualizarVisualBotao();

        // Futuramente o AudioManager poderá ler isso
        // AudioManager.Instance.AtualizarEstadoAudio(audioLigado);
    }

    private void CarregarEstadoAudio()
    {
        audioLigado = PlayerPrefs.GetInt(AUDIO_PREF_KEY, 1) == 1;
    }

    private void AtualizarVisualBotao()
    {
        iconeAudio.sprite = audioLigado 
            ? spriteAudioLigado 
            : spriteAudioDesligado;
    }

    public static bool AudioEstaLigado()
    {
        return PlayerPrefs.GetInt(AUDIO_PREF_KEY, 1) == 1;
    }

    #endregion

    #region Links

    public void AbrirInstagram()
    {
        Application.OpenURL(urlInstagram);
    }

    public void ChamarSuporte()
    {
        string mailto = $"mailto:{emailSuporte}?subject=Suporte%20AstroTap";
        Application.OpenURL(mailto);
    }

    #endregion
}