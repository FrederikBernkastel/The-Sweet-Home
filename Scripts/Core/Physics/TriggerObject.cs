using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public sealed class TriggerObject : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEnterEvent = delegate { };
        public event Action<Collider> OnTriggerExitEvent = delegate { };

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterEvent.Invoke(other);
        }
        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitEvent.Invoke(other);
        }
    }
}
