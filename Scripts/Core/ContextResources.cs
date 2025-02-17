using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RAY_Core
{
    public sealed class ContextResources
    {
        public BaseAdditionalStorage NameResources { get; } = default;
        public TypeResources TypeResources { get; } = default;
        public BaseAdditionalStorage CurrentRes { get; set; } = default;

        public ContextResources(BaseAdditionalStorage name, TypeResources typeResources)
        {
            NameResources = name;
            TypeResources = typeResources;
        }
    }
}
