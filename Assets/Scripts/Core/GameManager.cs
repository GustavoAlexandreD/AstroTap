using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState EstadoAtual { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        AlterarEstado(GameState.Playing);
    }

    public void AlterarEstado(GameState novoEstado)
    {
        EstadoAtual = novoEstado;

        switch (EstadoAtual)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
        }
    }

    public void PausarJogo()
    {
        AlterarEstado(GameState.Paused);
    }

    public void RetomarJogo()
    {
        AlterarEstado(GameState.Playing);
    }

    public void GameOver()
    {
        AlterarEstado(GameState.GameOver);
    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }
}