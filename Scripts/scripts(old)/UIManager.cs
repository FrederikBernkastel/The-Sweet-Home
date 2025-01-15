using Cinemachine;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenuManager mainMenuManager;
    [SerializeField] private HelpOverlayManager helpOverlayManager;
    [SerializeField] private SettingsManager settingsManager;

    [Space(15)]

    [SerializeField] private ChangeCameraController camerController;

    [Space(15)]

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [Space(15)]

    [SerializeField] private TVManager tvManager;
    [SerializeField] private LightManager lightManager;
    [SerializeField] private PhoneManager phoneManager;
    [SerializeField] private EscapeController escapeController;

    public void StartUI()
    {
        mainMenuManager.OnMainMenu();
        mainMenuManager.OnButtonStartGame();
        mainMenuManager.OnButtonSettings();
        mainMenuManager.OnButtonExit();

        helpOverlayManager.OffHelpOverlay();
        helpOverlayManager.OnButtonExitNo();
        helpOverlayManager.OnButtonExitYes();

        settingsManager.OffSettings();

        OnCamera();
    }
    public void OnCamera()
    {
        virtualCamera.enabled = true;
    }
    public void OffCamera()
    {
        virtualCamera.enabled = false;
    }
    public void MainMenuToHelpOverlay()
    {
        mainMenuManager.OffMainMenu();
        helpOverlayManager.OnHelpOverlay();
    }
    public void HelpOverlayToMainManu()
    {
        mainMenuManager.OnMainMenu();
        helpOverlayManager.OffHelpOverlay();
    }
    public void MainMenuToSettings()
    {
        mainMenuManager.OffMainMenu();
        settingsManager.OnSettings();
    }
    public void SettingsToMainMenu()
    {
        mainMenuManager.OnMainMenu();
        settingsManager.OffSettings();
    }
    public void ExitApplication()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void StartGame()
    {
        OffCamera();

        mainMenuManager.OffMainMenu();

        camerController.OnController();
        camerController.SetDefaultCamera();

        escapeController.OnController();
    }
    public void StopGame()
    {
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

        StartUI();
    }
}
