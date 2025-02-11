#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RAY_Cossacks
{
    public class ApplicationGameEntry : BaseApplicationEntry<ApplicationGameEntry>
    {
        private protected override void OnInit()
        {
            GameStorage.Instance.StateMachine.SetState("ContextMessageBox");

            //if (Physics.Raycast(
            //    GameStorage.Instance.mainCamera.transform.position, 
            //    GameStorage.Instance.mainCamera.transform.forward, 
            //    out var hitInfo, 
            //    float.MaxValue, 
            //    GameStorage.Instance.layerMaskGround))
            //{
            //    SpawnerUnitExt.SpawnUnitRadius(
            //        GameStorage.Instance.objectPoolGuard,
            //        GameStorage.Instance.unitGuard,
            //        hitInfo.point, 
            //        GameStorage.Instance.radiusSpawnUnit, 
            //        GameStorage.Instance.countSpawnUnit, 
            //        delegate { });
            //}
        }

        private protected override void OnDispose()
        {
            
        }
    }
}
#endif