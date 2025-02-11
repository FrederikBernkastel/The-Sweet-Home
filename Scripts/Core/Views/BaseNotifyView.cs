#if ENABLE_ERRORS

using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseNotifyView : BaseMouseUIView
    {
        private Coroutine coroutine;

        private IEnumerator StartCorNotify(BaseView view)
        {
            yield return new WaitForSeconds(1);

            view.Show(true);

            while (true)
            {
                view.SetPosition(Input.mousePosition);

                yield return null;
            }
        }
        private protected void StartNotify(IEnumerator enumerator)
        {
            coroutine = StartCoroutine(enumerator);
        }
        private protected void StartNotify(BaseView view)
        {
            coroutine = StartCoroutine(StartCorNotify(view));
        }
        private protected void StopNotify()
        {
            StopCoroutine(coroutine);
        }
        private protected void StopNotify(BaseView view)
        {
            StopCoroutine(coroutine);

            view.Show(false);
        }
    }
}
#endif