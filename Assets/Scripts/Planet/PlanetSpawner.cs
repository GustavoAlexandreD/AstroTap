using UnityEngine;
using System.Collections.Generic;

public class PlanetSpawner : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform planetsContainer;

    [Header("Sprites dos Planetas")]
    [SerializeField] private List<Sprite> planetSprites = new List<Sprite>();

    [Header("Spawn Config")]
    [SerializeField] private float distanciaVerticalMin = 4.0f;
    [SerializeField] private float distanciaVerticalMax = 6.0f;
    [SerializeField] private float distanciaParaSpawn = 15f;
    [SerializeField] private int planetasIniciais = 6;

    [Header("Segurança de Tela")]
    [SerializeField] private float raioOrbitaPlayer = 1.3f;
    [SerializeField] private float margemSeguranca = 0.2f;

    private float alturaUltimoPlaneta;

    private Camera cam;
    private float limiteEsquerda;
    private float limiteDireita;

    private Queue<int> ultimosPlanetas = new Queue<int>();
    private const int historicoMaximo = 3;

    private void Start()
    {
        if (planetPrefab == null || player == null || planetsContainer == null)
        {
            Debug.LogError("PlanetSpawner não está configurado corretamente.");
            enabled = false;
            return;
        }

        cam = Camera.main;

        float altura = cam.orthographicSize;
        float largura = altura * cam.aspect;

        limiteEsquerda = -largura;
        limiteDireita = largura;

        alturaUltimoPlaneta = player.position.y - 2f;

        Planet primeiroPlaneta = null;

        for (int i = 0; i < planetasIniciais; i++)
        {
            Planet planetaCriado = SpawnPlaneta();

            if (i == 0)
                primeiroPlaneta = planetaCriado;
        }

        if (primeiroPlaneta != null)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            controller.IniciarEmOrbita(primeiroPlaneta.transform);
        }
    }

    private void Update()
    {
        if (player == null) return;

        if (player.position.y + distanciaParaSpawn > alturaUltimoPlaneta)
        {
            SpawnPlaneta();
        }
    }

    private Planet SpawnPlaneta()
    {
        float distanciaY = Random.Range(distanciaVerticalMin, distanciaVerticalMax);
        alturaUltimoPlaneta += distanciaY;

        // 🔥 PREVÊ A ESCALA ANTES DO SPAWN
        float escalaPrevista = Random.Range(1.1f, 1.5f);

        // 🔥 Calcula raio do planeta baseado na escala
        float raioPlaneta = 0.5f * escalaPrevista;

        float margemTotal = raioPlaneta + raioOrbitaPlayer + margemSeguranca;

        float minX = limiteEsquerda + margemTotal;
        float maxX = limiteDireita - margemTotal;

        float posX = Random.Range(minX, maxX);

        Vector3 posicaoSpawn = new Vector3(posX, alturaUltimoPlaneta, 0f);

        int indiceSprite = EscolherSpriteSemRepeticao();

        GameObject novoPlaneta = Instantiate(
            planetPrefab,
            posicaoSpawn,
            Quaternion.identity,
            planetsContainer
        );

        Planet planetScript = novoPlaneta.GetComponent<Planet>();

        if (planetScript != null && planetSprites.Count > 0)
        {
            planetScript.ConfigurarPlaneta(planetSprites[indiceSprite], escalaPrevista);
        }

        return planetScript;
    }

    private int EscolherSpriteSemRepeticao()
    {
        if (planetSprites.Count == 0)
            return 0;

        if (planetSprites.Count <= historicoMaximo)
            return Random.Range(0, planetSprites.Count);

        int indice;

        do
        {
            indice = Random.Range(0, planetSprites.Count);
        }
        while (ultimosPlanetas.Contains(indice));

        ultimosPlanetas.Enqueue(indice);

        if (ultimosPlanetas.Count > historicoMaximo)
            ultimosPlanetas.Dequeue();

        return indice;
    }
}