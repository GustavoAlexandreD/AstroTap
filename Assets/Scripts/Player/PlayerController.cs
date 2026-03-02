using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerOrbit))]
public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Orbiting,
        Flying
    }

    [Header("Velocidade")]
    [SerializeField] private float velocidadeOrbitaBase = 120f;
    [SerializeField] private float multiplicadorVelocidade = 1.8f;
    [SerializeField] private float forcaLancamento = 6f;

    private PlayerState estadoAtual = PlayerState.Flying;

    private Rigidbody2D rb;
    private PlayerOrbit orbit;

    private bool estavaPressionando;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        orbit = GetComponent<PlayerOrbit>();
    }

    private void Update()
    {
        if (estadoAtual != PlayerState.Orbiting)
            return;

        bool pressionando = EstaPressionando();

        ControlarVelocidadeOrbita(pressionando);

        // Detecta soltou
        if (estavaPressionando && !pressionando)
        {
            Lancar();
        }

        estavaPressionando = pressionando;
    }

    private void ControlarVelocidadeOrbita(bool pressionando)
    {
        if (pressionando)
            orbit.SetVelocidade(velocidadeOrbitaBase * multiplicadorVelocidade);
        else
            orbit.SetVelocidade(velocidadeOrbitaBase);
    }

    private void Lancar()
    {
        estadoAtual = PlayerState.Flying;

        Vector2 direcaoTangente = orbit.ObterDirecaoTangente();
        orbit.PararOrbita();

        rb.linearVelocity = direcaoTangente * forcaLancamento;
    }

    public void IniciarEmOrbita(Transform planeta)
    {
        estadoAtual = PlayerState.Orbiting;

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;

        orbit.IniciarOrbita(planeta);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Planet planeta = collision.collider.GetComponent<Planet>();

        if (planeta != null)
        {
            estadoAtual = PlayerState.Orbiting;

            rb.linearVelocity = Vector2.zero;
            orbit.IniciarOrbita(planeta.transform);
        }
    }

    // =============================
    // INPUT UNIVERSAL (Desktop + Mobile)
    // =============================

    private bool EstaPressionando()
    {
        // Mobile (Toque)
        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
                return true;
        }

        // Desktop (Mouse)
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.isPressed)
                return true;
        }

        return false;
    }
}