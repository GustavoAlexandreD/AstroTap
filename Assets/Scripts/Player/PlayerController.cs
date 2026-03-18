using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

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

    public PlayerState EstadoAtual { get; private set; } = PlayerState.Flying;

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
        // Não processa input se o jogo não estiver rodando
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.EstadoAtual != GameState.Playing)
            return;

        if (EstadoAtual != PlayerState.Orbiting)
            return;

        bool pressionando = EstaPressionando();

        ControlarVelocidadeOrbita(pressionando);

        // Detecta quando o jogador solta o toque/clique
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
        EstadoAtual = PlayerState.Flying;

        // Lançamento perpendicular à órbita (radial, saindo do planeta)
        Vector2 direcaoLancamento = orbit.ObterDirecaoRadial();

        orbit.PararOrbita();

        rb.linearVelocity = direcaoLancamento * forcaLancamento;
    }

    public void IniciarEmOrbita(Transform planeta)
    {
        EstadoAtual = PlayerState.Orbiting;

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;

        orbit.IniciarOrbita(planeta);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Planet planeta = collision.collider.GetComponent<Planet>();

        if (planeta != null)
        {
            EstadoAtual = PlayerState.Orbiting;

            rb.linearVelocity = Vector2.zero;

            orbit.IniciarOrbita(planeta.transform);

            ScoreManager.Instance.AdicionarPontos();
        }
    }

    // =================================================
    // INPUT UNIVERSAL (MOBILE + DESKTOP)
    // =================================================

    private bool EstaPressionando()
    {
        if (ClickOuToqueNaUI())
            return false;

        // TOUCH (Mobile)
        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
                return true;
        }

        // MOUSE (Desktop)
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.isPressed)
                return true;
        }

        return false;
    }

    // =================================================
    // DETECTAR SE INPUT FOI NA UI
    // =================================================

    private bool ClickOuToqueNaUI()
    {
        if (EventSystem.current == null)
            return false;

        // Mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        // Touch
        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                int touchId = Touchscreen.current.primaryTouch.touchId.ReadValue();

                if (EventSystem.current.IsPointerOverGameObject(touchId))
                    return true;
            }
        }

        return false;
    }
}