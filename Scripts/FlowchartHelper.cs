using RAY_CuteHome;
using System.Collections;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;

namespace RAY_Core
{
    public class FlowchartHelper : MonoBehaviour
    {
        public void CloseStory()
        {
            IStory.CurrentStoryObject?.CloseDialoge();
        }
    }
}
