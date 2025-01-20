using Cinemachine;
using Fungus;
using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_CuteHome
{
    public class ViewFlowchart : BaseView
    {
        [BoxGroup("General")]
        [SerializeField][Required] private protected Flowchart flowchart;

        public Flowchart Flowchart => flowchart;
    }
}
