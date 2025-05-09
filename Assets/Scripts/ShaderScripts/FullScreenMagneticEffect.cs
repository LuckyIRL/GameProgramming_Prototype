using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FullScreenMagneticEffect : ScriptableRendererFeature
{
    public Material material;
    private MagneticEffectPass magneticPass;
    private bool activeEffect = false;
    private float disableTime = -1f;
    public static FullScreenMagneticEffect instance;

    public override void Create()
    {
        instance = this;
        magneticPass = new MagneticEffectPass(material);
        magneticPass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (material == null) return;

        if (activeEffect)
        {
            if (Time.time >= disableTime)
                activeEffect = false;
            else
                renderer.EnqueuePass(magneticPass);
        }
    }

    public void EnableEffectForDuration(float seconds)
    {
        activeEffect = true;
        disableTime = Time.time + seconds;
    }

    class MagneticEffectPass : ScriptableRenderPass
    {
        private Material effectMaterial;
        private RTHandle tempTexture;
        private RenderTextureDescriptor descriptor;

        public MagneticEffectPass(Material material)
        {
            this.effectMaterial = material;
        }

        [Obsolete]
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            descriptor = cameraTextureDescriptor;
            descriptor.depthBufferBits = 0;
            descriptor.msaaSamples = 1;

            RenderingUtils.ReAllocateHandleIfNeeded(ref tempTexture, descriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, name: "MagneticEffectTemp");
        }

        [Obsolete("Use Configure(CommandBuffer, RenderTextureDescriptor) instead.")]
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get("MagneticEffectPass");

            RTHandle cameraTargetHandle = renderingData.cameraData.renderer.cameraColorTargetHandle;

            // Blit from camera target to temporary texture
            Blitter.BlitCameraTexture(cmd, cameraTargetHandle, tempTexture, effectMaterial, 0);

            // Blit from temporary texture back to camera target
            Blitter.BlitCameraTexture(cmd, tempTexture, cameraTargetHandle, effectMaterial, 1);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            if (tempTexture != null)
                tempTexture.Release();
        }
    }
}
