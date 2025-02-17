using Fungus;
using KinematicCharacterController;
using KinematicCharacterController.Walkthrough.NoClipState;
using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace RAY_CuteHome
{
    public sealed class MainCharacter : BaseCharacter
    {
        public static MainCharacter Character { get; set; } = default;

        public ViewMainCharacter ViewMainCharacter { get; set; } = default;
        public ViewVirtualCamera ViewVCDialogeNPC { get; set; } = default;
        public ViewVirtualCamera ViewVCController { get; set; } = default;

        //public void EnableIOController()
        //{
        //    ViewMainCharacter.EnableIO();
        //}
        //public void DisableIOController()
        //{
        //    ViewMainCharacter.DisableIO();
        //}
        //public void EnableIOController(bool isEnable)
        //{
        //    if (isEnable)
        //    {
        //        ViewMainCharacter.EnableIO();
        //    }
        //    else
        //    {
        //        ViewMainCharacter.DisableIO();
        //    }
        //}

        //public void EnableCameraController()
        //{
        //    ViewVCController.EnableIO();
        //}
        //public void DisableCameraController()
        //{
        //    ViewVCController.DisableIO();
        //}
        //public void EnableCameraController(bool isEnable)
        //{
        //    if (isEnable)
        //    {
        //        ViewMainCharacter.EnableIO();
        //    }
        //    else
        //    {
        //        ViewMainCharacter.DisableIO();
        //    }
        //}
    }
    //public sealed class StateInteractableIO : BaseState
    //{
    //    public ViewMainCharacter viewMainCharacter { get; set; } = default;
    //    public ViewVirtualCamera viewVirtualCamera { get; set; } = default;

    //    public override void OnEnter(Action enterEvent)
    //    {
    //        base.OnEnter(() => 
    //        {
    //            viewMainCharacter.DisableIO();
    //            viewVirtualCamera.DisableIO();
    //        });
    //    }
    //    public override void OnExit(Action exitEvent)
    //    {
    //        base.OnExit(() => 
    //        {
    //            viewMainCharacter.EnableIO();
    //            viewVirtualCamera.EnableIO();
    //        });
    //    }
    //}
    public sealed class ViewMainCharacter : BaseView, IIO//, IStory
    {
        [BoxGroup("General")]
        [SerializeField][Required] private MyCharacterController characterController;
        [BoxGroup("General")]
        [SerializeField][Required] private KinematicCharacterMotor kinematicCharacterMotor;
        [BoxGroup("General")]
        [SerializeField][Required] private MyPlayer player;
        [BoxGroup("General")]
        [SerializeField][Required] private GameObject _object;
        //[BoxGroup("General")]
        //[SerializeField][Required] private Transform head;

        public MainCharacter CurrentBindCharacter { get; set; } = default;

        //[BoxGroup("RigsIK")]
        //[SerializeField][Required] private protected Rig rigHead;

        //[BoxGroup("Keys")]
        //[SerializeField] private protected KeyCode keyUp;
        //[BoxGroup("Keys")]
        //[SerializeField] private protected KeyCode keyDown;
        //[BoxGroup("Keys")]
        //[SerializeField] private protected KeyCode keyLeft;
        //[BoxGroup("Keys")]
        //[SerializeField] private protected KeyCode keyRight;
        //[BoxGroup("Keys")]
        //[SerializeField] private protected KeyCode keyJump;

        [BoxGroup("VirtualCameras")]
        [SerializeField][Required] private ViewVirtualCamera virtualCameraDialogeNPC;
        [BoxGroup("VirtualCameras")]
        [SerializeField][Required] private ViewVirtualCamera virtualCameraController;

        //[BoxGroup("Fungus")]
        //[SerializeField] private string nameBlock;

        //public bool IsDialogeRunning { get; private set; } = false;

        public override bool OnInit(Func<bool> initEvent)
        {
            return base.OnInit(() => 
            {
                if (MainCharacter.Character == default)
                {
                    MainCharacter character = new();

                    character.OnInit(default);

                    character.ViewMainCharacter = this;
                    character.ViewVCController = virtualCameraController;
                    character.ViewVCDialogeNPC = virtualCameraDialogeNPC;

                    CurrentBindCharacter = character;

                    MainCharacter.Character = character;
                }

                return true;
            });
        }
        public override bool OnDispose(Func<bool> disposeEvent)
        {
            return base.OnDispose(() => 
            {
                
                
                return true;
            });
        }

        //public void StartDialoge()
        //{
        //    if (!IsDialogeRunning)
        //    {
        //        IStory.CurrentStoryObject = this;

        //        BaseCameraSystem.Instance.ChangeVirtualCamera(virtualCameraDialogeNPC);

        //        if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewFlowchart, out var flowchart))
        //        {
        //            ViewFlowchart flow = (ViewFlowchart)flowchart;

        //            flow.Flowchart.ExecuteBlock(nameBlock);
        //        }

        //        UserInput.RemoveEvent(InputUpdate);

        //        IsDialogeRunning = true;
        //    }
        //}
        //public void CloseDialoge()
        //{
        //    if (IsDialogeRunning)
        //    {
        //        IStory.CurrentStoryObject = default;

        //        BaseCameraSystem.Instance.ChangeVirtualCamera(virtualCameraController);

        //        UserInput.AddEvent(InputUpdate);

        //        if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewFlowchart, out var flowchart))
        //        {
        //            ViewFlowchart flow = (ViewFlowchart)flowchart;

        //            flow.Flowchart.StopBlock(nameBlock);
        //        }

        //        IsDialogeRunning = false;
        //    }
        //}
        //public void BindingVirtualCamera()
        //{
        //    BaseCameraSystem.Instance.ChangeVirtualCamera(virtualCameraController);
        //}
        private protected override void OnShow()
        {
            _object.SetActive(true);
        }
        private protected override void OnHide()
        {
            _object.SetActive(false);
        }

        public void EnableIO(bool isEnable)
        {
            if (isEnable)
            {
                EnableIO();
            }
            else
            {
                DisableIO();
            }
        }
        public void EnableIO()
        {
            characterController.enabled = true;
            kinematicCharacterMotor.enabled = true;
            player.enabled = true;
        }
        public void DisableIO()
        {
            characterController.enabled = false;
            kinematicCharacterMotor.enabled = false;
            player.enabled = false;
        }
        //private protected override void __OnInit()
        //{
        //    if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewNPC, out var viewNPC))
        //    {
        //        (viewNPC as ViewNPC).OnDialogeNPCEnterEvent += BindDisable;
        //        (viewNPC as ViewNPC).OnDialogeNPCExitEvent += BindEnable;

        //        (viewNPC as ViewNPC).AddBindingAimLookAt(head);
        //    }
        //    if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewGhost, out var viewGhostNPC))
        //    {
        //        (viewGhostNPC as ViewGhostNPC).OnDialogeNPCEnterEvent += BindDisable;
        //        (viewGhostNPC as ViewGhostNPC).OnDialogeNPCExitEvent += BindEnable;
        //    }
        //}
        //private protected override void __OnDispose()
        //{
        //    if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewNPC, out var viewNPC))
        //    {
        //        (viewNPC as ViewNPC).OnDialogeNPCEnterEvent -= BindDisable;
        //        (viewNPC as ViewNPC).OnDialogeNPCExitEvent -= BindEnable;
        //    }
        //    if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewGhost, out var viewGhostNPC))
        //    {
        //        (viewGhostNPC as ViewGhostNPC).OnDialogeNPCEnterEvent -= BindDisable;
        //        (viewGhostNPC as ViewGhostNPC).OnDialogeNPCExitEvent -= BindEnable;
        //    }
        //}
        //private void BindEnable(ViewNPC npc) => EnableIO(true);
        //private void BindDisable(ViewNPC npc) => EnableIO(false);
        //private protected override void __DisableIO()
        //{
        //    characterController.enabled = false;
        //    kinematicCharacterMotor.enabled = false;
        //    player.enabled = false;

        //    UserInput.RemoveEvent(InputUpdate);

        //    if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewTV, out var viewTV))
        //    {
        //        viewTV.EnableIO(false);
        //    }
        //    if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewNPC, out var viewNPC))
        //    {
        //        viewNPC.EnableIO(false);
        //    }
        //    if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewGhost, out var viewGhostNPC))
        //    {
        //        viewGhostNPC.EnableIO(false);
        //    }
        //}
        //private protected override void __EnableIO()
        //{
        //    characterController.enabled = true;
        //    kinematicCharacterMotor.enabled = true;
        //    player.enabled = true;

        //    UserInput.AddEvent(InputUpdate);

        //    if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewTV, out var viewTV))
        //    {
        //        viewTV.EnableIO(true);
        //    }
        //    if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewNPC, out var viewNPC))
        //    {
        //        viewNPC.EnableIO(true);
        //    }
        //    if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewGhost, out var viewGhostNPC))
        //    {
        //        viewGhostNPC.EnableIO(true);
        //    }
        //}
        //private void InputUpdate()
        //{
            
        //}
    }
}
