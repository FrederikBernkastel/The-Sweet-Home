using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVManager : MonoBehaviour
{
    public void OnVideo(ListInteractionsTV tv)
    {
        tv.videoPlayer.enabled = true;
        tv.meshRenderer.enabled = true;
    }
    public void OffVideo(ListInteractionsTV tv)
    {
        tv.videoPlayer.enabled = false;
        tv.meshRenderer.enabled = false;
    }
    public void OnAudio(ListInteractionsTV tv)
    {
        tv.audioSource.enabled = true;
    }
    public void OffAudio(ListInteractionsTV tv)
    {
        tv.audioSource.enabled = false;
    }
    public void OnOffTV(ListInteractionsTV tv)
    {
        tv.videoPlayer.enabled = !tv.videoPlayer.enabled;
        tv.audioSource.enabled = !tv.audioSource.enabled;

        ChangeChannel(tv);

        tv.meshRenderer.enabled = !tv.meshRenderer.enabled;
    }
    public void ChangeChannel(ListInteractionsTV tv)
    {
        var index = Random.Range(0, tv.listClips.Length);

        tv.videoPlayer.clip = tv.listClips[index];
    }
}
