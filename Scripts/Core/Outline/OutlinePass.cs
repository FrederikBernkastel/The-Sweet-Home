using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

namespace PracticeProject_Lesson7
{
    public class OutlinePass : ScriptableRenderPass
    {
        private string _profilerTag = "Outline";
        private Material _material;

        public OutlinePass(Material material)
        {
            _material = material;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get(_profilerTag);

            using (new ProfilingScope(cmd, new ProfilingSampler("CVCVCV")))
            {
                var mesh = RenderingUtils.fullscreenMesh;
                cmd.DrawMesh(mesh, Matrix4x4.identity, _material);
                
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

    }
}
