using UnityEngine;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        orbit = GetComponent<PlayerOrbit>();
    }

    private void Update()
    {
        if (estadoAtual == PlayerState.Orbiting)
        {
            ControlarVelocidadeOrbita();

            if (Input.GetMouseButtonUp(0))
            {
                Lançar();
            }
        }
    }

    private void ControlarVelocidadeOrbita()
    {
        if (Input.GetMouseButton(0))
            orbit.SetVelocidade(velocidadeOrbitaBase * multiplicadorVelocidade);
        else
            orbit.SetVelocidade(velocidadeOrbitaBase);
    }

    private void Lançar()
    {
        estadoAtual = PlayerState.Flying;

        Vector2 direcaoTangente = orbit.ObterDirecaoTangente();
        orbit.PararOrbita();

        rb.linearVelocity = direcaoTangente * forcaLancamento;
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
}