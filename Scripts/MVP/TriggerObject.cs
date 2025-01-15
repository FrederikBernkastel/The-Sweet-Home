using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public interface ITriggerObject
    {
        public event Action<Collider> OnTriggerEnterEvent;
        public event Action<Collider> OnTriggerExitEvent;
    }
    public class TriggerObject : MonoBehaviour, ITriggerObject
    {
        public event Action<Collider> OnTriggerEnterEvent = delegate { };
        public event Action<Collider> OnTriggerExitEvent = delegate { };

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterEvent?.Invoke(other);
        }
        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitEvent?.Invoke(other);
        }
    }
}
