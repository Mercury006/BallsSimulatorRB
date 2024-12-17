using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public float maxVolume = 1.0f;
    public float maxPitch = 2.0f;
    public float speedThreshold = 5.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float speed = rb.linearVelocity.magnitude;

        if (speed > 0.1f)
        {
            // Adjust volume and pitch based on speed
            audioSource.volume = Mathf.Clamp(speed / speedThreshold, 0, maxVolume);
            audioSource.pitch = Mathf.Clamp(1 + (speed / speedThreshold), 1, maxPitch);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
