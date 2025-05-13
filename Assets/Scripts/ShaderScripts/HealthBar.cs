using DG.Tweening;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private Image healthBarTrailingImage;
    [SerializeField] private float trailDelay = 0.5f;
    [SerializeField] private float maxHealth = 100f;
    //[SerializeField] private float regenerationRate = 5f; // Health per second
    [SerializeField] private float criticalThreshold = 0.2f; // 20% energy
    private bool hasDepleted = false;

    private float currentHealth;
    public bool isDepleted => currentHealth <= 0f;

    public UnityEvent onEnergyDepleted;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBarFillImage.fillAmount = 1f;
        healthBarTrailingImage.fillAmount = 1f;
    }

    public void Drain(float amount)
    {
        if (currentHealth <= 0f) return;

        currentHealth = Mathf.Max(0f, currentHealth - amount);
        float ratio = currentHealth / maxHealth;

        DOTween.Kill(this); // Optional: prevent overlapping sequences

        Sequence sequence = DOTween.Sequence();
        sequence.Append(healthBarFillImage.DOFillAmount(ratio, 0.25f).SetEase(Ease.InOutSine));
        sequence.AppendInterval(trailDelay);
        sequence.Append(healthBarTrailingImage.DOFillAmount(ratio, 0.3f).SetEase(Ease.InOutSine));
        sequence.Play();

        // Only trigger low energy warning once
        if (!hasDepleted && ratio <= criticalThreshold)
        {
            hasDepleted = true;
            // Debug.Log("Warning: Energy low!");
            // You could raise a separate event here if needed, like onLowEnergy?.Invoke();
        }

        // Actual depletion logic (when energy fully drains)
        if (currentHealth <= 0f)
        {
            onEnergyDepleted?.Invoke();
        }
    }


    public void Regenerate(float amount)
    {
        if (currentHealth >= maxHealth) return;
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount * Time.deltaTime);
        float ratio = currentHealth / maxHealth;
        DOTween.Kill(this); // Optional: prevent overlapping sequences
        Sequence sequence = DOTween.Sequence();
        sequence.Append(healthBarFillImage.DOFillAmount(ratio, 0.25f).SetEase(Ease.InOutSine));
        sequence.AppendInterval(trailDelay);
        sequence.Append(healthBarTrailingImage.DOFillAmount(ratio, 0.3f).SetEase(Ease.InOutSine));
        sequence.Play();
    }

    public void ResetEnergy()
    {
        currentHealth = maxHealth;
        healthBarFillImage.fillAmount = 1f;
        healthBarTrailingImage.fillAmount = 1f;
    }

    public void StartRegenerationOverTime(float regenAmountPerSecond, float delayBeforeStart = 0f)
    {
        StopAllCoroutines();
        StartCoroutine(RegenerateOverTime(regenAmountPerSecond, delayBeforeStart));
    }

    private System.Collections.IEnumerator RegenerateOverTime(float regenAmountPerSecond, float delay)
    {
        yield return new WaitForSeconds(delay);

        while (currentHealth < maxHealth)
        {
            Regenerate(regenAmountPerSecond);
            yield return null;
        }

        hasDepleted = false; // Allow future energy depletion events
    }

}
