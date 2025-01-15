#if !ENABLE_ERRORS

using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public class LoadingStorage : BaseAdditionalStorage
    {
        public override string Name { get; } = "LoadingStorage";
    }
}
#endif