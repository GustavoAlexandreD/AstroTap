using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.EstadoAtual == GameState.Playing)
                Pausar();
            else if (GameManager.Instance.EstadoAtual == GameState.Paused)
                Retomar();
        }
    }

    public void Pausar()
    {
        pausePanel.SetActive(true);
        GameManager.Instance.PausarJogo();
    }

    public void Retomar()
    {
        pausePanel.SetActive(false);
        GameManager.Instance.RetomarJogo();
    }

    public void SairDoJogo()
    {
        GameManager.Instance.SairDoJogo();
    }
}