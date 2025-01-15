using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_CuteHome
{
    public class MainEntryPoint : BaseApplicationEntry
    {
        public override string Name { get; } = "MainEntryPoint";

        private protected override void __OnInit()
        {
            LoadingSystem.StartNonAsyncLoadUnloadResources(u =>
            {
                u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.CameraResources]);
                //u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.LoadingResources]);
                //u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.PrologeResources]);

                u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.GameResources]);
                u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.GameUIResources]);
                u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.AdditionalUIResources]);
                u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.StoryUIResources]);
                u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.MainActorResources]);
            });
        }
        private protected override void __OnStart()
        {
            var view = BaseMainStorage.MainStorage.PairView[TypeView.ViewPrologeDefault];

            if (view != default)
            {
                view.Show(true);
                view.EnableIO(true);
            }
            else
            {
                //throw new Exception();
            }
        }
    }
}
