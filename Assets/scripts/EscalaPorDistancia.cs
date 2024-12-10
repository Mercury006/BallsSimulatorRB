using UnityEngine;

public class ScaleOnMouseProximity : MonoBehaviour
{
    public float maxDistance = 100f;  // Distância máxima para influência (em pixels)
    public float maxScale = 1f;     // Escala máxima (quando o objeto está mais distante)
    public float minScale = 0.001f;     // Escala mínima (quando o objeto está mais perto)

    private Vector3 originalScale;

    void Start()
    {
        // Armazenar a escala original do objeto
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Obter a posição do mouse na tela
        Vector3 mouseScreenPos = Input.mousePosition;

        // Calcular a distância entre o mouse e o objeto (usando a posição do objeto na tela)
        Vector3 objectScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        float distance = Vector3.Distance(mouseScreenPos, objectScreenPos);

        // Se a distância for menor que o raio máximo, calcular a escala baseada na distância
        if (distance < maxDistance)
        {
            // Calcular a porcentagem de influencia com base na distância
            float scaleFactor = distance / maxDistance;  // Quanto maior a distancia, maior a escala

            // Interpolacaoo entre a escala minima e maxima com base na distância
            float targetScale = Mathf.Lerp(minScale, maxScale, scaleFactor);

            // Aplicar a nova escala ao objeto
            transform.localScale = originalScale * targetScale;
        }
        else
        {
            // Se a distancia for maior que o maximo, a escala e a maxima
            transform.localScale = originalScale * maxScale;
        }
    }
}
