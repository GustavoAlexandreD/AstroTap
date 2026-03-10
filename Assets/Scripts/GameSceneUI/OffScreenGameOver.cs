using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Quando o jogador sai completamente da tela, chama Game Over (pausa tudo e abre a tela de game over).
/// Oferece métodos para os botões: Menu, Retry e exibe a pontuação no painel.
/// </summary>
public class OffScreenGameOver : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject painelGameOver;
    [SerializeField] private Camera cam;

    [Header("Pontuação no Game Over")]
    [Tooltip("Texto que mostra a pontuação no painel de game over (mesma do contador em jogo).")]
    [SerializeField] private TextMeshProUGUI textoPontuacaoGameOver;

    [Header("Cena do Menu")]
    [Tooltip("Nome da cena do menu principal (ex: MenuScene).")]
    [SerializeField] private string nomeCenaMenu = "MenuScene";

    [Header("Margem (opcional)")]
    [Tooltip("Margem além da tela para considerar 'fora'. Ex: 0,1 = 10% além da borda.")]
    [SerializeField] [Range(0f, 0.5f)] private float margem = 0f;

    private bool gameOverDisparado;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;

        if (painelGameOver != null)
            painelGameOver.SetActive(false);
    }

    private void Update()
    {
        if (gameOverDisparado)
            return;

        if (GameManager.Instance == null || GameManager.Instance.EstadoAtual != GameState.Playing)
            return;

        if (player == null || cam == null)
            return;

        if (EstaForaDaTela())
        {
            gameOverDisparado = true;
            GameManager.Instance.GameOver();

            if (painelGameOver != null)
            {
                painelGameOver.SetActive(true);
                AtualizarPontuacaoNoPainel();
            }
        }
    }

    /// <summary>
    /// Atualiza o texto de pontuação do painel de game over com a mesma pontuação do jogo.
    /// </summary>
    private void AtualizarPontuacaoNoPainel()
    {
        if (textoPontuacaoGameOver == null) return;
        if (ScoreManager.Instance == null) return;

        textoPontuacaoGameOver.text = ScoreManager.Instance.GetScore().ToString();
    }

    /// <summary>
    /// Para usar no botão "Menu": retorna à cena do menu principal.
    /// </summary>
    public void VoltarAoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nomeCenaMenu);
    }

    /// <summary>
    /// Para usar no botão "Retry": reinicia a tentativa (recarrega a cena do jogo).
    /// </summary>
    public void ReiniciarTentativa()
    {
        GameManager.Instance.ReiniciarJogo();
    }

    /// <summary>
    /// True se o jogador estiver completamente fora da área visível da câmera (com margem opcional).
    /// </summary>
    private bool EstaForaDaTela()
    {
        Vector3 viewport = cam.WorldToViewportPoint(player.position);

        float min = -margem;
        float max = 1f + margem;

        return viewport.x < min || viewport.x > max || viewport.y < min || viewport.y > max;
    }

    /// <summary>
    /// Chame ao reiniciar o jogo para permitir novo game over por sair da tela.
    /// </summary>
    public void Resetar()
    {
        gameOverDisparado = false;
    }
}
