using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Planet : MonoBehaviour
{
    [Header("Escala")]
    [SerializeField] private float tamanhoMin = 0.8f;
    [SerializeField] private float tamanhoMax = 1.8f;

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private float escalaAtual;

    public float RaioReal => circleCollider.radius * escalaAtual;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void ConfigurarPlaneta(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;

        escalaAtual = Random.Range(tamanhoMin, tamanhoMax);
        transform.localScale = Vector3.one * escalaAtual;
    }
}