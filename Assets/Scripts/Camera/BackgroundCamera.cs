using UnityEngine;

[ExecuteAlways]
public class BackgroundScaler : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;

    private void Awake()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        AjustarTamanho();
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            if (targetCamera == null)
                targetCamera = Camera.main;
            AjustarTamanho();
        }
    }

    private void AjustarTamanho()
    {
        if (targetCamera == null) return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null) return;

        float worldScreenHeight = targetCamera.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * targetCamera.aspect;

        Vector2 spriteSize = sr.sprite.bounds.size;

        float scaleX = worldScreenWidth / spriteSize.x;
        float scaleY = worldScreenHeight / spriteSize.y;

        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}