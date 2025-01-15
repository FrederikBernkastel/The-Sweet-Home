using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Video;

[Serializable]
public class Interaction
{
    public InputActionReference reference;
    public UnityEvent unityEvent;
}
public class ListInteractions : MonoBehaviour
{
    [SerializeField] public Interaction[] inputInteractions;
    [SerializeField] public RectTransform storage;
    [SerializeField] public TMP_Text[] texts;
    [SerializeField] public UnityEvent unityEventHover;
    [SerializeField] public UnityEvent unityEventLeave;
    [SerializeField] public Renderer meshRenderer;
}
