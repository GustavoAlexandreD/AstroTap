using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [Header("Referências das Telas")]
    [SerializeField] private GameObject telaMenuInicial;
    [SerializeField] private GameObject telaLoja;
    [SerializeField] private GameObject telaInventario;
    [SerializeField] private GameObject telaOpcoes;

    [Header("Nome da Cena do Jogo")]
    [SerializeField] private Object cenaJogo;
    private string nomeCenaJogo;

    private void Awake()
    {
        nomeCenaJogo = cenaJogo.name;
    }

    private void Start()
    {
        MostrarMenuInicial();
    }

    #region Funções de Navegação

    public void MostrarMenuInicial()
    {
        DesativarTodasTelas();
        telaMenuInicial.SetActive(true);
    }

    public void AbrirLoja()
    {
        DesativarTodasTelas();
        telaLoja.SetActive(true);
    }

    public void AbrirInventario()
    {
        DesativarTodasTelas();
        telaInventario.SetActive(true);
    }

    public void AbrirOpcoes()
    {
        telaOpcoes.SetActive(true);
    }

    public void VoltarParaMenu()
    {
        MostrarMenuInicial();
    }

    #endregion

    #region Controle de Cena

    public void Jogar()
    {
        SceneManager.LoadScene(nomeCenaJogo);
    }

    #endregion

    #region Utilitário

    private void DesativarTodasTelas()
    {
        telaMenuInicial.SetActive(false);
        telaLoja.SetActive(false);
        telaInventario.SetActive(false);
        telaOpcoes.SetActive(false);
    }

    #endregion
}