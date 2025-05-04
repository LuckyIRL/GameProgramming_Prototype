using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class RobotCloak : MonoBehaviour
{
    public Material cloakMaterial; // your Predator-style shader
    private bool isCloaked = false;

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
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ToggleCloak();
        }
    }

    void ToggleCloak()
    {
        isCloaked = !isCloaked;

        foreach (var renderer in meshRenderers)
        {
            if (isCloaked)
            {
                // Create a list of cloak materials with same length
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
            }
        }
    }
}
