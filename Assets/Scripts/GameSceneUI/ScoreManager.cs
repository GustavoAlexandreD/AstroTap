using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float maiorAltura;
    private int scoreAtual;

    private void Update()
    {
        if (player == null) return;

        if (player.position.y > maiorAltura)
        {
            maiorAltura = player.position.y;
            scoreAtual = Mathf.FloorToInt(maiorAltura);
            AtualizarUI();
        }
    }

    private void AtualizarUI()
    {
        scoreText.text = scoreAtual.ToString();
    }

    public int GetScore()
    {
        return scoreAtual;
    }
}