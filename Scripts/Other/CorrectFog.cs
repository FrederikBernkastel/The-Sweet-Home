#if ENABLE_ERRORS

using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor.AI;
using UnityEngine;

public class CorrectFog : MonoBehaviour
{
    [SerializeField] public BaseEssence startPosition;
    [SerializeField] public Camera mainCamera;
    
    public void OnLateUpdate()
    {
        transform.position = mainCamera.transform.position + (startPosition.transform.position - mainCamera.transform.position) * 0.5f;
    }
}

#endif