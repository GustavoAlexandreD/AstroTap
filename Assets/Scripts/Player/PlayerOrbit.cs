using UnityEngine;

public class PlayerOrbit : MonoBehaviour
{
    [SerializeField] private float raioOrbita = 1.5f;

    private Transform planetaAtual;
    private float angulo;
    private float velocidadeAngular;

    private bool orbitando = false;

    public void IniciarOrbita(Transform planeta)
    {
        planetaAtual = planeta;

        Vector2 direcao = (transform.position - planeta.position).normalized;
        angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        orbitando = true;
    }

    public void PararOrbita()
    {
        orbitando = false;
        planetaAtual = null;
    }

    public void SetVelocidade(float velocidade)
    {
        velocidadeAngular = velocidade;
    }

    private void Update()
    {
        if (!orbitando || planetaAtual == null)
            return;

        angulo += velocidadeAngular * Time.deltaTime;

        float rad = angulo * Mathf.Deg2Rad;

        Vector2 offset = new Vector2(
            Mathf.Cos(rad),
            Mathf.Sin(rad)
        ) * raioOrbita;

        transform.position = (Vector2)planetaAtual.position + offset;
    }

    public Vector2 ObterDirecaoTangente()
    {
        float rad = angulo * Mathf.Deg2Rad;

        Vector2 tangente = new Vector2(
            -Mathf.Sin(rad),
            Mathf.Cos(rad)
        );

        return tangente.normalized;
    }

    public Vector2 ObterDirecaoRadial()
    {
        // Direção do planeta para o jogador (radial, para fora)
        float rad = angulo * Mathf.Deg2Rad;

        Vector2 radial = new Vector2(
            Mathf.Cos(rad),
            Mathf.Sin(rad)
        );

        return radial.normalized;
    }
}