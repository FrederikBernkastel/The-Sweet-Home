using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_CuteHome
{
    [MainEntry(TypeApplication = TypeApplication.TheSweetHome)]
    public sealed class MainEntryPoint : BaseMainEntryPoint
    {
        public override void OnInit(ApplicationEntry applicationEntry)
        {
            LoadingSystem.Instance.StartNonAsyncLoadUnloadResources(u =>
            {
                if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.CameraResources, out var camera))
                {
                    u.AddLoadContext(camera);
                }

                //u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.LoadingResources]);
                //u.AddLoadContext(BaseMainStorage.MainStorage.PairContextResources[TypeResources.PrologeResources]);

                //if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.GameResources, out var game))
                //{
                //    u.AddLoadContext(game);
                //}
                //if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.StoryUIResources, out var story))
                //{
                //    u.AddLoadContext(story);
                //}
                //if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.GameUIResources, out var gameUI))
                //{
                //    u.AddLoadContext(gameUI);
                //}
                //if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.AdditionalUIResources, out var additionalUI))
                //{
                //    u.AddLoadContext(additionalUI);
                //}
                
                if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.DebugTest, out var debugTest))
                {
                    u.AddLoadContext(debugTest);
                }
                if (LoadingSystem.Instance.PairContextResources.TryGetValueWithoutKey(TypeResources.MainActorResources, out var mainActor))
                {
                    u.AddLoadContext(mainActor);
                }
            });
        }
        public override void OnStart(ApplicationEntry applicationEntry)
        {
            //if (GraphicsSystem.Instance.PairViewsUI.TryGetValueWithoutKey(TypeViewUI.ViewPrologeDefault, out var loadingView))
            //{
            //    loadingView.Show(true);
            //    loadingView.EnableIO(true);
            //}

            if (MainCharacter.Character != default)
            {
                CameraSystem.Instance.ChangeVirtualCamera(MainCharacter.Character.ViewVCController);
                
                MainCharacter.Character.ViewMainCharacter.EnableIO();

                MainCharacter.Character.ViewVCController.EnableIO();
            }
        }
    }
}
