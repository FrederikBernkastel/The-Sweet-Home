using Cinemachine;
using NaughtyAttributes;
using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Video;

namespace RAY_CuteHome
{
    public interface IViewTV : IBaseView
    {
        public event Action<Collider> OnTriggerEnterEvent;
        public event Action<Collider> OnTriggerExitEvent;

        public VideoPlayer VideoPlayer { get; }
        public Material Material { get; }
        public AudioSource AudioSource { get; }
        public Dictionary<TypeChannel, VideoClip> PairChannel { get; }
        public ITriggerObject TriggerObject { get; }
        public string TagTrigger { get; set; }

        public void ChangeTypeChannel(TypeChannel typeChannel);
        public void ChangeNextChannel();
        public void Pause();
        public void SetVolume(float volume);
    }
    public enum TypeChannel
    {
        Channel1,
        Channel2,
        Channel3,
        Channel4,
    }
    public class ViewTV : BaseView, IViewTV
    {
        public override string Name { get; } = "ViewTV";

        public event Action<Collider> OnTriggerEnterEvent = delegate { };
        public event Action<Collider> OnTriggerExitEvent = delegate { };

        [BoxGroup("General")]
        [SerializeField][Required] private protected VideoPlayer _videoPlayer;
        [SerializeField][Required] private protected Renderer _renderer;
        [SerializeField][Required] private protected AudioSource _audio;
        [SerializeField][Required] private protected TriggerObject _trigger;

        [BoxGroup("VideoClips")]
        [SerializeField] private protected SerializePairChannel<TypeChannel, VideoClip>[] listDefaultClips;

        public Dictionary<TypeChannel, VideoClip> PairChannel { get; private protected set; } = default;
        private List<VideoClip> videoClips { get; set; } = default;
        public VideoPlayer VideoPlayer => _videoPlayer;
        public Material Material => _renderer.sharedMaterial;
        public AudioSource AudioSource => _audio;
        public ITriggerObject TriggerObject => _trigger;
        public string TagTrigger { get; set; } = "MainCharacter";

        private protected override void CreateGlobalInstance()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewTV] ??= this;
        }
        private protected override void RemoveGlobalInstance()
        {
            BaseMainStorage.MainStorage.PairView[TypeView.ViewTV] = default;
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
        }
        private protected override void __OnDispose()
        {
            DisposePairChannel();
        }
        private protected override void __Show()
        {
            _videoPlayer.Play();

            _renderer.sharedMaterial.color = Color.white;
        }
        private protected override void __Hide()
        {
            _videoPlayer.Stop();

            _renderer.sharedMaterial.color = Color.black;
        }
        public void ChangeTypeChannel(TypeChannel typeChannel)
        {
            _videoPlayer.clip = PairChannel[typeChannel] ?? throw new Exception();
        }
        public void ChangeNextChannel()
        {
            _videoPlayer.clip = videoClips[UnityEngine.Random.Range(0, videoClips.Count)] ?? throw new Exception();
        }
        public void Pause()
        {
            _videoPlayer.Pause();
        }
        public void SetVolume(float volume)
        {
            _audio.volume = volume;
        }
        private protected override void __DisableIO()
        {
            _trigger.OnTriggerEnterEvent -= TriggerEnterEvent;
            _trigger.OnTriggerExitEvent -= TriggerExitEvent;
        }
        private protected override void __EnableIO()
        {
            _trigger.OnTriggerEnterEvent += TriggerEnterEvent;
            _trigger.OnTriggerExitEvent += TriggerExitEvent;
        }
        private void TriggerExitEvent(Collider collider)
        {
            if (collider.CompareTag(TagTrigger))
            {
                Debug.Log("TriggerEnter");
                
                MainActor.MainActorEvents -= InputUpdate;

                OnTriggerExitEvent?.Invoke(collider);
            }
        }
        private void TriggerEnterEvent(Collider collider)
        {
            if (collider.CompareTag(TagTrigger))
            {
                Debug.Log("TriggerExit");

                MainActor.MainActorEvents += InputUpdate;

                OnTriggerEnterEvent?.Invoke(collider);
            }
        }
        private void InputUpdate()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Show(!IsVisible);
            }
        }
    }
    [Serializable]
    public abstract class BaseSerializeKeyValuePair<T, Y> where T: Enum
    {
        public abstract T Key { get; }
        public abstract Y Value { get; }
    }
    [Serializable]
    public class SerializePairChannel<T, Y> : BaseSerializeKeyValuePair<T, Y> where T: Enum
    {
        [SerializeField] private protected T typeChannel;
        [SerializeField][Required] private protected Y clip;

        public override T Key => typeChannel;
        public override Y Value => clip;
    }
    public static class DictionaryExtensions
    {
        public static Dictionary<T, Y> Init<T, Y>(this Dictionary<T, Y> pairs) where T : Enum
        {
            pairs.Clear();

            foreach (var s in Enum.GetValues(typeof(T)))
            {
                pairs.Add((T)s, default);
            }

            return pairs;
        }
        public static void Add<T, Y>(this Dictionary<T, Y> pairs, IEnumerable<BaseSerializeKeyValuePair<T, Y>> valuePairs) where T : Enum
        {
            foreach (var s in valuePairs)
            {
                if (pairs.ContainsKey(s.Key))
                {
                    pairs[s.Key] = s.Value;

                    return;
                }
                else
                {
                    throw new Exception();
                }
            }
        }
        public static void Dispose<T, Y>(this Dictionary<T, Y> pairs)
        {
            pairs.Clear();
        }
    }
}
