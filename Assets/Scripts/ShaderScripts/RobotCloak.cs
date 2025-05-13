using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class RobotCloak : MonoBehaviour
{
    public Material cloakMaterial; // your Predator-style shader
    private bool isCloaked = false;
    public HealthBar energyBar;

    [Header("Energy Usage")]
    public float energyDrainRate = 5f; // per second
    private bool isEnergyDepleted = false;
    public MagneticEffectController effectController; // assign in Inspector

    public bool IsCloaked => isCloaked;

    private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    private Dictionary<MeshRenderer, Material[]> originalMaterials = new Dictionary<MeshRenderer, Material[]>();

    void Start()
    {
        // Find all MeshRenderers in the robot's hierarchy
        meshRenderers.AddRange(GetComponentsInChildren<MeshRenderer>());

        // Store original materials
        foreach (var renderer in meshRenderers)
        {
            originalMaterials[renderer] = renderer.materials;
        }
    }

    void Update()
    {
        if (isEnergyDepleted) return;

        // Toggle cloak ON/OFF on C key press
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ToggleCloak();
        }

        // If cloaked, drain energy continuously
        if (isCloaked)
        {
            energyBar.Drain(energyDrainRate * Time.deltaTime);
        }
    }

    void ToggleCloak()
    {
        isCloaked = !isCloaked;

        foreach (var renderer in meshRenderers)
        {
            if (isCloaked)
            {
                Material[] cloakedMats = new Material[renderer.materials.Length];
                for (int i = 0; i < cloakedMats.Length; i++)
                {
                    cloakedMats[i] = cloakMaterial;
                }
                renderer.materials = cloakedMats;
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }
            else
            {
                renderer.materials = originalMaterials[renderer];
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

                // Start regeneration after cloak turns off
                energyBar.StartRegenerationOverTime(5f, 0.5f); // Regenerate 5 per second after 0.5s delay
            }
        }
    }


    public void OnEnergyDepleted()
    {
        //Debug.Log("Energy depleted");
        isEnergyDepleted = true;

        if (isCloaked)
        {
            //Debug.Log("Cloak turned off");
            isCloaked = false;
            foreach (var renderer in meshRenderers)
            {
                renderer.materials = originalMaterials[renderer];
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }

        effectController.TriggerEffect(); // trigger the post-processing shader
        Invoke(nameof(StartRegenerationOverTime), 3f);
    }

    private void StartRegenerationOverTime()
    {
        energyBar.StartRegenerationOverTime(5f, 0.5f); // 5 per second, start after 0.5s
        isEnergyDepleted = false;
    }

    private void OnEnable()
    {
        energyBar.onEnergyDepleted.AddListener(OnEnergyDepleted);
    }

    private void OnDisable()
    {
        energyBar.onEnergyDepleted.RemoveListener(OnEnergyDepleted);
    }
}
