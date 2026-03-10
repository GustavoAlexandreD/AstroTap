using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    [SerializeField] private TextMeshProUGUI scoreText;

    private int scoreAtual;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AdicionarPontos(){
        scoreAtual++;
        AtualizarUI();
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