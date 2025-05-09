using UnityEngine;

public class HologramProjector : MonoBehaviour
{
    public GameObject hologramPrefab;
    public Transform projectionPoint;
    public GameObject beamEffect;
    public Light projectionLight;
    public AudioClip hologramAudio;
    private GameObject currentHologram;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (currentHologram == null)
            {
                StartProjection();
            }
            else
            {
                StopProjection();
            }
        }
    }

    void StartProjection()
    {
        currentHologram = Instantiate(hologramPrefab, projectionPoint.position, Quaternion.identity);
        beamEffect.gameObject.SetActive(true);
        projectionLight.enabled = true;
        // Play the hologram audio oneshot
        AudioSource.PlayClipAtPoint(hologramAudio, projectionPoint.position);
    }

    void StopProjection()
    {
        Destroy(currentHologram);
        beamEffect.gameObject.SetActive(false);
        projectionLight.enabled = false;
        // stop the hologram audio

    }
}
