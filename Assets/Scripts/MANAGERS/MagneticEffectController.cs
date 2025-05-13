using UnityEngine;

public class MagneticEffectController : MonoBehaviour
{
    public Shader shader;
    public string shaderProperty = "_VignettePower";
    public float activeValue = 1f;
    public float resetValue = 0f;
    public float effectDuration = 2f;
    public bool isActive = false;

    [Header("Debug Testing")]
    public bool triggerEffectInEditor = false;

    private void Start()
    {
        Shader.SetGlobalFloat(shaderProperty, resetValue);
    }

    void Update()
    {
        if (triggerEffectInEditor)
        {
            triggerEffectInEditor = false;
            Shader.SetGlobalFloat("_EffectToggle", 1f);
            Invoke(nameof(ResetEffect), 2f); // or however long the effect should last
        }
    }

    public void TriggerEffect()
    {
        //Debug.Log("Activating fullscreen magnetic effect!");
        isActive = true;
        Shader.SetGlobalFloat(shaderProperty, activeValue);  // turn effect on
        Invoke(nameof(ResetEffect), effectDuration);          // schedule turn off
        
    }

    void ResetEffect()
    {
        Shader.SetGlobalFloat(shaderProperty, resetValue);    // turn effect off
        isActive = false;
    }


}
