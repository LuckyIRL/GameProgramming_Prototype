using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private float noiseLevel = 0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void MakeNoise(float intensity)
    {
        noiseLevel = intensity;
    }

    public float GetNoiseLevel()
    {
        return noiseLevel;
    }
}
