using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public sealed class IOSystem : BaseCoreObject
    {
        public static IOSystem Instance { get; } = new();

        public static Stack<KeyValuePair<string, bool>> TempList { get; } = new();

        public event Action UIEvent = delegate { };

        private Dictionary<string, ContextUpdate<IIO>> pairContextIO { get; } = new();

        public IReadOnlyDictionary<string, ContextUpdate<IIO>> PairContextIO => pairContextIO;

        private IOSystem() { }

        public void CreateIOObject(BaseView view)
        {

        }
        public void DestroyIOObject(BaseView view)
        {

        }
        
        public void AddIOObject(string name, IIO ioObject)
        {
            if (IsInited)
            {
                if (!pairContextIO.ContainsKey(name))
                {
                    pairContextIO.Add(name, new());
                }

                pairContextIO[name].listObjects.Add(ioObject);
            }
        }
        public void RemoveIOObject(IIO ioObject)
        {
            if (IsInited)
            {
                foreach (var s in pairContextIO.Values)
                {
                    if (s.listObjects.Remove(ioObject))
                    {
                        break;
                    }
                }
            }
        }

        public void EnableIO(string name)
        {
            
            if (IsInited)
            {
                if (pairContextIO.ContainsKey(name))
                {
                    pairContextIO[name].IsEnable = true;
                }
            }
        }
        public void DisableIO(string name)
        {
            if (IsInited)
            {
                if (pairContextIO.ContainsKey(name))
                {
                    pairContextIO[name].IsEnable = false;
                }
            }
        }
        public void EnableIO()
        {
            if (IsInited)
            {
                foreach (var s in pairContextIO.Values)
                {
                    s.IsEnable = true;
                }
            }
        }
        public void DisableIO()
        {
            if (IsInited)
            {
                foreach (var s in pairContextIO.Values)
                {
                    foreach (var n in s.listObjects)
                    {
                        n.DisableIO();
                    }
                }
            }
        }

        public override void Reset(Action resetEvent)
        {
            base.Reset(() => 
            {
                UIEvent = default;

                UIEvent = delegate { };

                pairContextIO.Clear();
            });
        }

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                Reset(default);

                return true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() => 
            {
                Reset(default);

                return true;
            });
        }

        public void Update()
        {
            if (IsInited)
            {
                UIEvent.Invoke();
            }
        }
    }
}
