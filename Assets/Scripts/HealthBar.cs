using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private Image healthBarTrailingImage;
    [SerializeField] private float trailDelay = 0.5f;

    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;

        healthBarFillImage.fillAmount = 1f;
        healthBarTrailingImage.fillAmount = 1f;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            DrainHealthBar();
        }
    }

    private void DrainHealthBar()
    {
        currentHealth -= 10f;
        float ratio = currentHealth / maxHealth;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(healthBarFillImage.DOFillAmount(ratio, 0.25f))
            .SetEase(Ease.InOutSine);
        sequence.AppendInterval(trailDelay);
        sequence.Append(healthBarTrailingImage.DOFillAmount(ratio, 0.3f))
            .SetEase(Ease.InOutSine);

        sequence.Play();
    }
}
