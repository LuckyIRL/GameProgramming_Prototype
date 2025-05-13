using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProjectileNoise : MonoBehaviour
{
    public float noiseIntensity = 1.5f;
    public float noiseDuration = 1f;
    public AudioClip impactSound;

    private bool hasMadeNoise = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1f; // 3D sound
        audioSource.volume = 1f;
        audioSource.spatialBlend = 1f; // 1 = fully 3D
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 25f;
        audioSource.playOnAwake = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasMadeNoise) return;
        hasMadeNoise = true;

        Debug.Log("Projectile hit and made noise");

        // Play audio
        if (impactSound != null)
        {
            audioSource.PlayOneShot(impactSound);
        }

        // Make noise for AI
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.MakeNoise(noiseIntensity, transform.position);

            if (noiseDuration > 0)
                Invoke(nameof(ClearNoise), noiseDuration);
        }
    }

    void ClearNoise()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.MakeNoise(0f, Vector3.zero);
    }
}
