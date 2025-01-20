using Cinemachine;
using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Video;

namespace RAY_CuteHome
{
    public class ViewTV : BaseView
    {
        public event Action<Collider> OnTriggerEnterEvent = delegate { };
        public event Action<Collider> OnTriggerExitEvent = delegate { };

        [BoxGroup("General")]
        [SerializeField][Required] private protected VideoPlayer _videoPlayer;
        [BoxGroup("General")]
        [SerializeField][Required] private protected Renderer _renderer;
        [BoxGroup("General")]
        [SerializeField][Required] private protected AudioSource _audio;
        [BoxGroup("General")]
        [SerializeField][Required] private protected TriggerObject _trigger;
        [BoxGroup("General")]
        [SerializeField][Required] private protected GameObject _object;

        [BoxGroup("VideoClips")]
        [SerializeField] private protected SerializePairChannel<TypeChannel, VideoClip>[] listDefaultClips;

        [BoxGroup("TagsTriggers")]
        [SerializeField][Tag] private protected string tagTrigger;

        [BoxGroup("Keys")]
        [SerializeField] private protected KeyCode keyOnOffTV;
        [BoxGroup("Keys")]
        [SerializeField] private protected KeyCode keyChangeChannelTV;
        [BoxGroup("Keys")]
        [SerializeField] private protected KeyCode keyPauseTV;

        [BoxGroup("Outline")]
        [SerializeField][Layer] private protected int layerOutline;

        [BoxGroup("Limbs")]
        [SerializeField] private protected Renderer[] listRenderer;

        public Dictionary<TypeChannel, VideoClip> PairChannel { get; private protected set; } = default;
        private List<VideoClip> videoClips { get; set; } = default;
        public VideoPlayer VideoPlayer => _videoPlayer;
        public Material Material => _renderer.sharedMaterial;
        public AudioSource AudioSource => _audio;
        public ITriggerObject TriggerObject => _trigger;
        public bool IsWithinTrigger { get; private protected set; } = false;

        private int lastLayer { get; set; } = default;

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
        private void InitPairChannel()
        {
            PairChannel = new Dictionary<TypeChannel, VideoClip>().Init();
            videoClips = new(PairChannel.Values);

            PairChannel.Add(listDefaultClips);

            _videoPlayer.clip = PairChannel.GetValueOrDefault(TypeChannel.Channel1);
        }
        private void DisposePairChannel()
        {
            PairChannel.Dispose();
            videoClips = default;
        }
        private protected override void __OnInit()
        {
            InitPairChannel();

            SetVolume(1f);

            Play(false);
        }
        private protected override void __OnDispose()
        {
            DisposePairChannel();
        }
        private protected override void __Show()
        {
            _object.SetActive(true);
        }
        private protected override void __Hide()
        {
            _object.SetActive(false);
        }
        public void ChangeTypeChannel(TypeChannel typeChannel)
        {
            _videoPlayer.clip = PairChannel[typeChannel] ?? throw new Exception();
        }
        public void ChangeNextChannel()
        {
            _videoPlayer.clip = videoClips[UnityEngine.Random.Range(0, videoClips.Count)] ?? throw new Exception();
        }
        public void Pause(bool flag)
        {
            if (flag)
            {
                _videoPlayer.Pause();
            }
            else
            {
                Play(true);
            }
        }
        public void Play(bool flag)
        {
            if (flag)
            {
                _videoPlayer.Play();

                _renderer.sharedMaterial.color = Color.white;
            }
            else
            {
                _videoPlayer.Stop();

                _renderer.sharedMaterial.color = Color.black;
            }
        }
        public void SetVolume(float volume)
        {
            _audio.volume = volume;
        }
        private protected override void __DisableIO()
        {
            SetOutline(false);

            _trigger.OnTriggerEnterEvent -= TriggerEnterEvent;
            _trigger.OnTriggerExitEvent -= TriggerExitEvent;
        }
        private protected override void __EnableIO()
        {
            SetOutline(IsWithinTrigger);

            _trigger.OnTriggerEnterEvent += TriggerEnterEvent;
            _trigger.OnTriggerExitEvent += TriggerExitEvent;
        }
        private void TriggerExitEvent(Collider collider)
        {
            if (collider.CompareTag(tagTrigger))
            {
                SetOutline(false);

                UserInput.RemoveEvent(InputUpdate);

                OnTriggerExitEvent.Invoke(collider);

                IsWithinTrigger = false;
            }
        }
        private void TriggerEnterEvent(Collider collider)
        {
            if (collider.CompareTag(tagTrigger))
            {
                SetOutline(true);

                UserInput.AddEvent(InputUpdate);

                OnTriggerEnterEvent.Invoke(collider);

                IsWithinTrigger = true;
            }
        }
        private void InputUpdate()
        {
            if (Input.GetKeyDown(keyOnOffTV))
            {
                Play(!_videoPlayer.isPlaying);
            }
            else if (Input.GetKeyDown(keyChangeChannelTV))
            {
                ChangeNextChannel();
            }
            else if (Input.GetKeyDown(keyPauseTV))
            {
                Pause(!_videoPlayer.isPaused);
            }
        }
    }
}
