using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpOverlayManager : MonoBehaviour
{
    [SerializeField] private Button buttonExitYes;
    [SerializeField] private Button buttonExitNo;

    [Space(15)]

    [SerializeField] private string textHelpOverlay;

    [Space(15)]

    [SerializeField] private TMP_Text textHelp;

    [Space(15)]

    [SerializeField] private RectTransform helpOverlay;

    public void OnHelpOverlay()
    {
        helpOverlay.gameObject.SetActive(true);
        textHelp.text = textHelpOverlay;
    }
    public void OffHelpOverlay()
    {
        helpOverlay.gameObject.SetActive(false);
    }
    public void OnButtonExitYes()
    {
        buttonExitYes.gameObject.SetActive(true);
    }
    public void OffButtonExitYes()
    {
        buttonExitYes.gameObject.SetActive(false);
    }
    public void OnButtonExitNo()
    {
        buttonExitNo.gameObject.SetActive(true);
    }
    public void OffButtonExitNo()
    {
        buttonExitNo.gameObject.SetActive(false);
    }
}
