using Fungus;
using KinematicCharacterController;
using KinematicCharacterController.Walkthrough.NoClipState;
using NaughtyAttributes;
using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace RAY_CuteHome
{
    public class ViewMainCharacter : BaseView, IStory
    {
        [BoxGroup("General")]
        [SerializeField][Required] private protected MyCharacterController characterController;
        [BoxGroup("General")]
        [SerializeField][Required] private protected KinematicCharacterMotor kinematicCharacterMotor;
        [BoxGroup("General")]
        [SerializeField][Required] private protected MyPlayer player;
        [BoxGroup("General")]
        [SerializeField][Required] private protected GameObject _object;
        [BoxGroup("General")]
        [SerializeField][Required] private protected Transform head;

        //[BoxGroup("RigsIK")]
        //[SerializeField][Required] private protected Rig rigHead;

        [BoxGroup("Keys")]
        [SerializeField] private protected KeyCode keyUp;
        [BoxGroup("Keys")]
        [SerializeField] private protected KeyCode keyDown;
        [BoxGroup("Keys")]
        [SerializeField] private protected KeyCode keyLeft;
        [BoxGroup("Keys")]
        [SerializeField] private protected KeyCode keyRight;
        [BoxGroup("Keys")]
        [SerializeField] private protected KeyCode keyJump;

        [BoxGroup("VirtualCameras")]
        [SerializeField][Required] private protected ViewVirtualCamera virtualCameraDialogeNPC;
        [BoxGroup("VirtualCameras")]
        [SerializeField][Required] private protected ViewVirtualCamera virtualCameraController;

        [BoxGroup("Fungus")]
        [SerializeField] private protected string nameBlock;

        public bool IsDialogeRunning { get; private protected set; } = false;

        public void StartDialoge()
        {
            if (!IsDialogeRunning)
            {
                IStory.CurrentStoryObject = this;

                BaseCameraSystem.Instance.ChangeVirtualCamera(virtualCameraDialogeNPC);

                if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewFlowchart, out var flowchart))
                {
                    ViewFlowchart flow = (ViewFlowchart)flowchart;

                    flow.Flowchart.ExecuteBlock(nameBlock);
                }

                UserInput.RemoveEvent(InputUpdate);

                IsDialogeRunning = true;
            }
        }
        public void CloseDialoge()
        {
            if (IsDialogeRunning)
            {
                IStory.CurrentStoryObject = default;

                BaseCameraSystem.Instance.ChangeVirtualCamera(virtualCameraController);

                UserInput.AddEvent(InputUpdate);

                if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewFlowchart, out var flowchart))
                {
                    ViewFlowchart flow = (ViewFlowchart)flowchart;

                    flow.Flowchart.StopBlock(nameBlock);
                }

                IsDialogeRunning = false;
            }
        }
        public void BindingVirtualCamera()
        {
            BaseCameraSystem.Instance.ChangeVirtualCamera(virtualCameraController);
        }
        private protected override void __Show()
        {
            _object.SetActive(true);
        }
        private protected override void __Hide()
        {
            _object.SetActive(false);
        }
        private protected override void __OnInit()
        {
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewNPC, out var viewNPC))
            {
                (viewNPC as ViewNPC).OnDialogeNPCEnterEvent += BindDisable;
                (viewNPC as ViewNPC).OnDialogeNPCExitEvent += BindEnable;

                (viewNPC as ViewNPC).AddBindingAimLookAt(head);
            }
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewGhost, out var viewGhostNPC))
            {
                (viewGhostNPC as ViewGhostNPC).OnDialogeNPCEnterEvent += BindDisable;
                (viewGhostNPC as ViewGhostNPC).OnDialogeNPCExitEvent += BindEnable;
            }
        }
        private protected override void __OnDispose()
        {
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewNPC, out var viewNPC))
            {
                (viewNPC as ViewNPC).OnDialogeNPCEnterEvent -= BindDisable;
                (viewNPC as ViewNPC).OnDialogeNPCExitEvent -= BindEnable;
            }
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewGhost, out var viewGhostNPC))
            {
                (viewGhostNPC as ViewGhostNPC).OnDialogeNPCEnterEvent -= BindDisable;
                (viewGhostNPC as ViewGhostNPC).OnDialogeNPCExitEvent -= BindEnable;
            }
        }
        private void BindEnable(ViewNPC npc) => EnableIO(true);
        private void BindDisable(ViewNPC npc) => EnableIO(false);
        private protected override void __DisableIO()
        {
            characterController.enabled = false;
            kinematicCharacterMotor.enabled = false;
            player.enabled = false;

            UserInput.RemoveEvent(InputUpdate);

            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewTV, out var viewTV))
            {
                viewTV.EnableIO(false);
            }
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewNPC, out var viewNPC))
            {
                viewNPC.EnableIO(false);
            }
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewGhost, out var viewGhostNPC))
            {
                viewGhostNPC.EnableIO(false);
            }
        }
        private protected override void __EnableIO()
        {
            characterController.enabled = true;
            kinematicCharacterMotor.enabled = true;
            player.enabled = true;

            UserInput.AddEvent(InputUpdate);

            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewTV, out var viewTV))
            {
                viewTV.EnableIO(true);
            }
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewNPC, out var viewNPC))
            {
                viewNPC.EnableIO(true);
            }
            if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewGhost, out var viewGhostNPC))
            {
                viewGhostNPC.EnableIO(true);
            }
        }
        private void InputUpdate()
        {
            
        }
    }
}
