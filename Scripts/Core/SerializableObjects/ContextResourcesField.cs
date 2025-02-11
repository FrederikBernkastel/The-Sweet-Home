using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    [Serializable]
    public sealed class ContextResourcesField
    {
        [BoxGroup("ResourcesField")]
        [SerializeField][Scene] private string nameResources;
        [BoxGroup("ResourcesField")]
        [SerializeField] private TypeResources typeResources;

        public void Execute()
        {
            ContextResources context = new(nameResources, typeResources);

            LoadingSystem.Instance.AddContextResources(context);
        }
    }
}
