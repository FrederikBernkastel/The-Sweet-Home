#if ENABLE_ERRORS

using Cinemachine;
using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace RAY_Cossacks
{
    public class CameraLifeState : BaseState
    {
        public override string NameState => "Camera";

        private CinemachineOrbitalTransposer orbitalTransposer;

        private protected override void Init()
        {
            orbitalTransposer = GameStorage.Instance.vcam.GetCinemachineComponent<CinemachineOrbitalTransposer>();

            GameStorage.Instance.inputProvider.enabled = false;
        }
        private protected override void Update()
        {
            orbitalTransposer.m_XAxis.m_MaxSpeed = GameStorage.Instance.SpeedRotation;

            Vector3 velocity = new();

            if (Input.GetKey(GameStorage.Instance.Up))
            {
                velocity += new Vector3(GameStorage.Instance.mainCamera.transform.forward.x, 0, GameStorage.Instance.mainCamera.transform.forward.z);
            }
            else if (Input.GetKey(GameStorage.Instance.Down))
            {
                velocity += new Vector3(-GameStorage.Instance.mainCamera.transform.forward.x, 0, -GameStorage.Instance.mainCamera.transform.forward.z);
            }
            if (Input.GetKey(GameStorage.Instance.Left))
            {
                velocity += new Vector3(-GameStorage.Instance.mainCamera.transform.right.x, 0, -GameStorage.Instance.mainCamera.transform.right.z);
            }
            else if (Input.GetKey(GameStorage.Instance.Right))
            {
                velocity += new Vector3(GameStorage.Instance.mainCamera.transform.right.x, 0, GameStorage.Instance.mainCamera.transform.right.z);
            }

            if (Input.GetMouseButtonDown(GameStorage.Instance.buttonRotation))
            {
                GameStorage.Instance.inputProvider.enabled = true;
            }
            else if (Input.GetMouseButtonUp(GameStorage.Instance.buttonRotation))
            {
                GameStorage.Instance.inputProvider.enabled = false;
            }

            orbitalTransposer.m_FollowOffset.y += Input.mouseScrollDelta.y * GameStorage.Instance.SpeedScale * Time.deltaTime;
            orbitalTransposer.m_FollowOffset.y = Mathf.Clamp(orbitalTransposer.m_FollowOffset.y, GameStorage.Instance.minValueScale, GameStorage.Instance.maxValueScale);

            GameStorage.Instance.markRef.transform.position += velocity * GameStorage.Instance.SpeedMovement * orbitalTransposer.m_FollowOffset.y * Time.deltaTime;
        }
    }
}
#endif