using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float offsetY = 2f;

    private float maiorAlturaAlcancada;

    private void Start()
    {
        maiorAlturaAlcancada = transform.position.y;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        if (player.position.y > maiorAlturaAlcancada)
        {
            maiorAlturaAlcancada = player.position.y;
        }

        float targetY = maiorAlturaAlcancada + offsetY;

        Vector3 novaPosicao = new Vector3(
            transform.position.x,
            Mathf.Lerp(transform.position.y, targetY, smoothSpeed * Time.deltaTime),
            transform.position.z
        );

        transform.position = novaPosicao;
    }
}