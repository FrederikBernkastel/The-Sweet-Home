using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public sealed class MainEntryAttribute : Attribute
    {
        public TypeApplication TypeApplication { get; set; }
    }
}
