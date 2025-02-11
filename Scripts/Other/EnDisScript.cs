#if ENABLE_ERRORS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnDisScript : MonoBehaviour
{
    private void OnDisable()
    {
        ContextGameState.sources.RemoveAll(u => u.component.gameObject == this.gameObject);
    }
}

#endif
