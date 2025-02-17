using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseAdditionalStorage : BaseCoreObjectBehaviour
    {
        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                foreach (var s in this.GetComponentsInChildren<BaseView>(true))
                {
                    Transform transf = s.transform.parent;
                    bool flag = true;

                    while (transf)
                    {
                        if (transf.TryGetComponent<BaseView>(out var _))
                        {
                            flag = false;

                            break;
                        }

                        transf = transf.transform.parent;
                    }

                    if (flag)
                    {
                        s.OnInit(default);

                        s.OnStart(default);
                    }
                }
                foreach (var s in this.GetComponentsInChildren<BaseViewUI>(true))
                {
                    Transform transf = s.transform.parent;
                    bool flag = true;

                    while (transf)
                    {
                        if (transf.TryGetComponent<BaseViewUI>(out var _))
                        {
                            flag = false;

                            break;
                        }

                        transf = transf.transform.parent;
                    }

                    if (flag)
                    {
                        s.OnInit(default);

                        s.OnStart(default);
                    }
                }

                return initEvent?.Invoke() ?? true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() => 
            {
                var flag = disposeEvent?.Invoke() ?? true;

                foreach (var s in this.GetComponentsInChildren<BaseView>(true))
                {
                    Transform transf = s.transform.parent;
                    bool _flag = true;
                    
                    while (transf)
                    {
                        if (transf.TryGetComponent<BaseView>(out var _))
                        {
                            _flag = false;

                            break;
                        }

                        transf = transf.transform.parent;
                    }

                    if (_flag)
                    {
                        s.OnDispose(default);
                    }
                }
                foreach (var s in this.GetComponentsInChildren<BaseViewUI>(true))
                {
                    Transform transf = s.transform.parent;
                    bool _flag = true;

                    while (transf)
                    {
                        if (transf.TryGetComponent<BaseViewUI>(out var _))
                        {
                            _flag = false;

                            break;
                        }

                        transf = transf.transform.parent;
                    }

                    if (_flag)
                    {
                        s.OnDispose(default);
                    }
                }

                return flag;
            });
        }
    }
}
