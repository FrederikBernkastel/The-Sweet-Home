using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class ListInteractionsTV : ListInteractions
{
    [SerializeField] public VideoPlayer videoPlayer;
    [SerializeField] public AudioSource audioSource;

    [Space(15)]

    [SerializeField] public VideoClip[] listClips;
}
