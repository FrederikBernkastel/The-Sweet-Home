using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineBrainFix : MonoBehaviour
{
    private void Awake()
    {
        Camera.main.GetComponent<Cinemachine.CinemachineBrain>().m_UpdateMethod = Cinemachine.CinemachineBrain.UpdateMethod.LateUpdate;
    }
}
