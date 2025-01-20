using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_CuteHome
{
    [MainEntry(TypeApplication = TypeApplication.TheSweetHome)]
    public class MainEntryPoint : BaseMainEntryPoint
    {
        public override void OnInit(BaseApplicationEntry applicationEntry)
        {
            applicationEntry.EventInit += () =>
            {
                LoadingSystem.StartNonAsyncLoadUnloadResources(u =>
                {
                    u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.CameraResources]);
                    //u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.LoadingResources]);
                    //u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.PrologeResources]);

                    u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.GameResources]);
                    u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.StoryUIResources]);
                    u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.GameUIResources]);
                    u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.AdditionalUIResources]);
                    u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.MainActorResources]);
                });
            };
            applicationEntry.EventStart += () =>
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
            };
        }
    }
}
