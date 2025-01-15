using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PhoneManager : MonoBehaviour
{
    public void OnFlowchart(ListInteractionsPhone phone)
    {
        phone.flowchart.enabled = true;
    }
    public void OffFlowchart(ListInteractionsPhone phone)
    {
        phone.flowchart.enabled = false;
    }
    public void CallPhone(ListInteractionsPhone phone)
    {
        phone.playerManager.OffMovement();
        phone.playerManager.OffInteractions();
        phone.playerManager.OffMovementCamera();

        phone.flowchart.SendFungusMessage("MESS");
    }
    public void EndCallPhone(ListInteractionsPhone phone)
    {
        phone.playerManager.OnMovement();
        phone.playerManager.OnInteractions();
        phone.playerManager.OnMovementCamera();

        phone.flowchart.StopAllBlocks();
    }
}
