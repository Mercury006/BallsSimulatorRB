using UnityEngine;

public class MouseDragForceField : MonoBehaviour
{
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float baseForceStrength = 5f;
    [SerializeField] private float falloffStrength = 10f;
    [SerializeField] private float smoothness = 0.5f;
    [SerializeField] private float impulseMultiplier = 2f;

    private Rigidbody rb;
    private Vector3 previousMouseWorldPos;
    private Vector3 mouseVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning($"Objeto {gameObject.name} não possui um Rigidbody. Adicionando automaticamente.");
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Desativar a gravidade permanentemente
        rb.useGravity = false;

        previousMouseWorldPos = GetMouseWorldPosition();
    }

    void Update()
    {
        Vector3 currentMouseWorldPos = GetMouseWorldPosition();
        mouseVelocity = (currentMouseWorldPos - previousMouseWorldPos) / Time.deltaTime;
        previousMouseWorldPos = currentMouseWorldPos;

        float distance = Vector3.Distance(currentMouseWorldPos, transform.position);

        if (distance <= maxDistance)
        {
            Vector3 directionalForce = mouseVelocity.normalized;

            if (directionalForce != Vector3.zero)
            {
                float distanceFalloff = Mathf.Pow(1 - (distance / maxDistance), falloffStrength);
                float dynamicForce = (baseForceStrength + mouseVelocity.magnitude * impulseMultiplier) * distanceFalloff;

                if (float.IsNaN(dynamicForce))
                {
                    dynamicForce = 0f;
                }

                Vector3 force = directionalForce * dynamicForce;

                if (float.IsNaN(force.x) || float.IsNaN(force.y) || float.IsNaN(force.z))
                {
                    force = Vector3.zero;
                }

                rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, force, Time.deltaTime / smoothness);
            }
        }
        else
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.deltaTime / smoothness);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    // Atualiza as propriedades dinamicamente
    public void UpdateProperties(float maxDist, float baseForce, float falloff, float smooth, float impulse)
    {
        maxDistance = maxDist;
        baseForceStrength = baseForce;
        falloffStrength = falloff;
        smoothness = smooth;
        impulseMultiplier = impulse;
    }
}