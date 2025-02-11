using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_CuteHome
{
    public sealed class ViewSpawnPlayer : BaseView
    {
        [BoxGroup("General")]
        [SerializeField][Required] private Transform pointSpawnPlayer;

        public void SpawnObject(BaseView view)
        {
            view.GetTransform().position = pointSpawnPlayer.position;
            view.GetTransform().forward = new(view.transform.forward.x, pointSpawnPlayer.forward.y, view.transform.forward.z);
        }
    }
}
