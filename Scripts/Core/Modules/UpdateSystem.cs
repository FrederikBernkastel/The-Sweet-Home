using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public sealed class ContextUpdate<T> where T : class
    {
        public List<T> listObjects { get; } = new(100);

        public bool IsEnable { get; set; } = true;
    }
    public sealed class UpdateSystem : BaseCoreObject
    {
        public static UpdateSystem Instance { get; } = new();
        
        public static Stack<KeyValuePair<string, bool>> TempList { get; } = new();

        private Stack<BaseState> stackDispose { get; } = new();

        private Dictionary<string, ContextUpdate<BaseState>> pairContextUpdate { get; } = new();

        public IReadOnlyDictionary<string, ContextUpdate<BaseState>> PairContextUpdate => pairContextUpdate;

        private UpdateSystem() { }

        public void AddUpdateObject(string name, BaseState updateObject)
        {
            if (IsInited)
            {
                if (!pairContextUpdate.ContainsKey(name))
                {
                    pairContextUpdate.Add(name, new());
                }

                pairContextUpdate[name].listObjects.Add(updateObject);
            }
        }
        public void RemoveUpdateObject(BaseState updateObject)
        {
            if (IsInited)
            {
                foreach (var s in pairContextUpdate.Values)
                {
                    if (s.listObjects.Remove(updateObject))
                    {
                        break;
                    }
                }
            }
        }

        public void EnableUpdate(string name)
        {
            if (IsInited)
            {
                if (pairContextUpdate.ContainsKey(name))
                {
                    pairContextUpdate[name].IsEnable = true;
                }
            }
        }
        public void DisableUpdate(string name)
        {
            if (IsInited)
            {
                if (pairContextUpdate.ContainsKey(name))
                {
                    pairContextUpdate[name].IsEnable = false;
                }
            }
        }
        public void EnableUpdate()
        {
            if (IsInited)
            {
                foreach (var s in pairContextUpdate.Values)
                {
                    s.IsEnable = true;
                }
            }
        }
        public void DisableUpdate()
        {
            if (IsInited)
            {
                foreach (var s in pairContextUpdate.Values)
                {
                    s.IsEnable = false;
                }
            }
        }

        public override void Reset(Action resetEvent)
        {
            base.Reset(() => 
            {
                stackDispose.Clear();
                pairContextUpdate.Clear();
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

        public void AddUpdateObjectToDispose(BaseState state)
        {
            if (IsInited)
            {
                stackDispose.Push(state);
            }
        }

        public void Update()
        {
            if (IsInited)
            {
                foreach (var s in pairContextUpdate.Values)
                {
                    if (s.IsEnable)
                    {
                        foreach (var n in s.listObjects)
                        {
                            if (n.IsEnableUpdateType(TypeUpdate.Update))
                            {
                                n.OnUpdate(default);
                            }
                        }
                    }
                }

                while (stackDispose.TryPop(out var result))
                {
                    RemoveUpdateObject(result);
                }
            }
        }
        public void FixedUpdate()
        {
            if (IsInited)
            {
                foreach (var s in pairContextUpdate.Values)
                {
                    if (s.IsEnable)
                    {
                        foreach (var n in s.listObjects)
                        {
                            if (n.IsEnableUpdateType(TypeUpdate.FixedUpdate))
                            {
                                n.OnFixedUpdate(default);
                            }
                        }
                    }
                }
            }
        }
        public void GUI()
        {
            if (IsInited)
            {
                foreach (var s in pairContextUpdate.Values)
                {
                    if (s.IsEnable)
                    {
                        foreach (var n in s.listObjects)
                        {
                            if (n.IsEnableUpdateType(TypeUpdate.GUI))
                            {
                                n.OnGUI(default);
                            }
                        }
                    }
                }
            }
        }
        public void DrawGizmos()
        {
            if (IsInited)
            {
                foreach (var s in pairContextUpdate.Values)
                {
                    if (s.IsEnable)
                    {
                        foreach (var n in s.listObjects)
                        {
                            if (n.IsEnableUpdateType(TypeUpdate.DrawGizmos))
                            {
                                n.OnDrawGizmos(default);
                            }
                        }
                    }
                }
            }
        }
    }
}
