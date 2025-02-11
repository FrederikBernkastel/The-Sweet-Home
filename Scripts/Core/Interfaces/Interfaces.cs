using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public interface IDispose
    {
        public void Dispose();
    }
    public interface IViewCanvas
    {
        public Canvas Canvas { get; }
    }
    public interface IIO
    {
        public void EnableIO();
        public void DisableIO();
    }
}
