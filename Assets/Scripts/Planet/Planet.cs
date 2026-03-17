using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Planet : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    private float escalaAtual;

    public float RaioReal => circleCollider.radius * escalaAtual;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void ConfigurarPlaneta(Sprite sprite, float escala)
    {
        spriteRenderer.sprite = sprite;

        escalaAtual = escala;
        transform.localScale = Vector3.one * escalaAtual;
    }
}