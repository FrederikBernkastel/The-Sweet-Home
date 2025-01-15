using Fungus;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ChangeCameraController camerController;
    [SerializeField] private TVManager tvManager;
    [SerializeField] private LightManager lightManager;
    [SerializeField] private PhoneManager phoneManager;
    [SerializeField] private InputActionAsset actionMap;
    [SerializeField] private EscapeController escapeController;
    [SerializeField] private ActionExtensions actionExtensions;

    private void Awake()
    {
        actionExtensions.SetSettings();
        actionExtensions.CompleteSettings();


        foreach (var s in actionMap.actionMaps)
        {
            s.Enable();

            foreach (var j in s.actions)
            {
                j.Enable();
            }
        }

        escapeController.OffController();

        camerController.OffController();
        camerController.StopCameras();

        foreach (var s in GameObject.FindGameObjectsWithTag("TV").Select(u => u.GetComponentInChildren<ListInteractionsTV>()))
        {
            tvManager.OffAudio(s);
            tvManager.OffVideo(s);
            s.meshRenderer.enabled = false;
        }
        foreach (var s in GameObject.FindGameObjectsWithTag("Phone").Select(u => u.GetComponentInChildren<ListInteractionsPhone>()))
        {
            phoneManager.OnFlowchart(s);
        }
        foreach (var s in GameObject.FindGameObjectsWithTag("LightTor").Select(u => u.GetComponentInChildren<ListInteractionsLight>()))
        {
            lightManager.OnLight(s);
        }

        uiManager.StartUI();
    }
}
