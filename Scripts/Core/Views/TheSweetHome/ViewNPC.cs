using Fungus;
using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Video;

namespace RAY_CuteHome
{
    public interface IStory
    {
        public static IStory CurrentStoryObject { get; set; }

        public void StartDialoge();
        public void CloseDialoge();
    }
    public class ViewNPC : BaseView//, IStory
    {
        public event Action<Collider> OnTriggerEnterEvent = delegate { };
        public event Action<Collider> OnTriggerExitEvent = delegate { };

        public event Action<ViewNPC> OnDialogeNPCEnterEvent = delegate { };
        public event Action<ViewNPC> OnDialogeNPCExitEvent = delegate { };

        [BoxGroup("General")]
        [SerializeField][Required] private protected Renderer _renderer;
        [BoxGroup("General")]
        [SerializeField][Required] private protected TriggerObject _trigger;
        [BoxGroup("General")]
        [SerializeField][Required] private protected GameObject _object;

        [BoxGroup("Limbs")]
        [SerializeField] private protected Renderer[] listRenderer;

        [BoxGroup("RigsIK")]
        [SerializeField] private protected Rig rigHead;
        [SerializeField] private protected MultiAimConstraint rigHeadConstraint;

        [BoxGroup("TagsTriggers")]
        [SerializeField][Tag] private protected string tagTrigger;

        [BoxGroup("Keys")]
        [SerializeField] private protected KeyCode keyStartDialogeNPC;
        [SerializeField] private protected KeyCode keyCloseDialogeNPC;

        [BoxGroup("VirtualCameras")]
        [SerializeField][Required] private protected ViewVirtualCamera virtualCameraDialogeNPC;

        [BoxGroup("Fungus")]
        [SerializeField] private protected string nameBlock;

        [BoxGroup("Outline")]
        [SerializeField][Layer] private protected int layerOutline;

        public Material Material => _renderer.sharedMaterial;
        public TriggerObject TriggerObject => _trigger;
        public bool IsDialogeRunning { get; private protected set; } = false;
        public bool IsWithinTrigger {  get; private protected set; } = false;

        private int lastLayer { get; set; } = default;
        private BaseViewVirtualCamera lastVirtualCamera { get; set; } = default;

        public void PlayHeadLookAt(bool flag)
        {
            if (rigHead != default)
            {
                rigHead.weight = flag ? 1 : 0;
            }
        }
        public void AddBindingAimLookAt(Transform transform)
        {
            if (rigHeadConstraint != default)
            {
                rigHeadConstraint.data.sourceObjects.Add(new WeightedTransform(transform, 1));
            }
        }
        //private protected override void __OnInit()
        //{
        //    lastLayer = listRenderer.Length != 0 ? listRenderer[0].gameObject.layer : -1;

        //    PlayHeadLookAt(false);
        //}
        //public void StartDialoge()
        //{
        //    if (!IsDialogeRunning)
        //    {
        //        IStory.CurrentStoryObject = this;

        //        PlayHeadLookAt(true);

        //        SetOutline(false);

        //        lastVirtualCamera = BaseCameraSystem.Instance.CurrentVirtualCamera;
        //        BaseCameraSystem.Instance.ChangeVirtualCamera(virtualCameraDialogeNPC);

        //        if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewFlowchart, out var flowchart))
        //        {
        //            ViewFlowchart flow = (ViewFlowchart)flowchart;

        //            flow.Flowchart.ExecuteBlock(nameBlock);
        //        }

        //        UserInput.RemoveEvent(InputUpdate);

        //        OnDialogeNPCEnterEvent.Invoke(this);

        //        IsDialogeRunning = true;
        //    }
        //}
        //public void CloseDialoge()
        //{
        //    if (IsDialogeRunning)
        //    {
        //        IStory.CurrentStoryObject = default;

        //        PlayHeadLookAt(false);

        //        SetOutline(IsWithinTrigger);

        //        BaseCameraSystem.Instance.ChangeVirtualCamera(lastVirtualCamera);

        //        if (IsWithinTrigger)
        //        {
        //            UserInput.AddEvent(InputUpdate);
        //        }

        //        if (BaseMainStorage.MainStorage.PairView.TryGetValueWithoutKey(TypeView.ViewFlowchart, out var flowchart))
        //        {
        //            ViewFlowchart flow = (ViewFlowchart)flowchart;

        //            flow.Flowchart.StopBlock(nameBlock);
        //        }

        //        OnDialogeNPCExitEvent.Invoke(this);

        //        IsDialogeRunning = false;
        //    }
        //}
        public void SetOutline(bool flag)
        {
            if (flag)
            {
                foreach (var s in listRenderer)
                {
                    lastLayer = s.gameObject.layer;
                    s.gameObject.layer = layerOutline;
                }
            }
            else
            {
                foreach (var s in listRenderer)
                {
                    s.gameObject.layer = lastLayer != -1 ? lastLayer : s.gameObject.layer;
                }
            }
        }
        private protected override void OnShow()
        {
            _object.SetActive(true);
        }
        private protected override void OnHide()
        {
            _object.SetActive(false);
        }
        //private protected override void __DisableIO()
        //{
        //    _trigger.OnTriggerEnterEvent -= TriggerEnterEvent;
        //    _trigger.OnTriggerExitEvent -= TriggerExitEvent;
        //}
        //private protected override void __EnableIO()
        //{
        //    _trigger.OnTriggerEnterEvent += TriggerEnterEvent;
        //    _trigger.OnTriggerExitEvent += TriggerExitEvent;
        //}
        //private void TriggerExitEvent(Collider collider)
        //{
        //    if (collider.CompareTag(tagTrigger))
        //    {
        //        SetOutline(false);
        //        Debug.LogError("EXIT");
        //        UserInput.RemoveEvent(InputUpdate);

        //        OnTriggerExitEvent.Invoke(collider);

        //        IsWithinTrigger = false;
        //    }
        //}
        //private void TriggerEnterEvent(Collider collider)
        //{
        //    if (collider.CompareTag(tagTrigger))
        //    {
        //        SetOutline(true);
        //        Debug.LogError("ENTER");
        //        UserInput.AddEvent(InputUpdate);

        //        OnTriggerEnterEvent.Invoke(collider);

        //        IsWithinTrigger = true;
        //    }
        //}
        //private void InputUpdate()
        //{
        //    if (Input.GetKeyDown(keyStartDialogeNPC))
        //    {
        //        StartDialoge();
        //    }
        //    else if (Input.GetKeyDown(keyCloseDialogeNPC))
        //    {
        //        CloseDialoge();
        //    }
        //}
    }
}
