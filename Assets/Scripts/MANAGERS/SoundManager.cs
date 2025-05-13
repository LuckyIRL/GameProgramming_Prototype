using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private float noiseLevel = 0f;
    private Vector3 noisePosition;

    public RobotCloak robotCloak;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

    public void MakeNoise(float intensity, Vector3 position)
    {
        noiseLevel = intensity;
        noisePosition = position;
    }

    public Vector3 GetNoisePosition() => noisePosition;

    public float GetNoiseLevel()
    {
        float adjustedNoise = noiseLevel;

        if (robotCloak != null && robotCloak.IsCloaked)
        {
            adjustedNoise *= 0.5f; // Reduce noise if cloaked (tweak as needed)
        }

        return adjustedNoise;
    }

    public void ClearNoise()
    {
        noiseLevel = 0f;
        noisePosition = Vector3.zero;
    }
}
