using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public void OnLight(ListInteractionsLight lig)
    {
        lig.lightInteraction.enabled = true;
    }
    public void OffLight(ListInteractionsLight lig)
    {
        lig.lightInteraction.enabled = false;
    }
    public void OnOffLight(ListInteractionsLight lig)
    {
        lig.lightInteraction.enabled = !lig.lightInteraction.enabled;
    }
}
