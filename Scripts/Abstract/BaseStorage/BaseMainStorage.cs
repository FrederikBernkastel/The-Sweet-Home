#if ENABLE_ERRORS

using NaughtyAttributes;
//using RAY_Cossacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseMainStorage<T> : BaseStorage<T> where T : BaseStorage<T>
    {
        [SerializeField][Required] public MainData Data;

        public ViewMessageBox PresenterMessageBox { get; private protected set; }
        private protected virtual BaseState loadingState => new LoadingState() { PrefabView = Data.PrefabViewLoading };

        private protected override void _OnInit()
        {
            PresenterMessageBox = GameObject.Instantiate(Data.PrefabViewMessageBox, GameObject.FindGameObjectWithTag(Data.RefStorageView).transform);

            PresenterMessageBox.Show(false);
        }
    }
}
#endif