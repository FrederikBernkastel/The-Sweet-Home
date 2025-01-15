using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button buttonStartGame;
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonExit;

    [Space(15)]

    [SerializeField] private RectTransform mainMenu;

    public void OnMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
    }
    public void OffMainMenu()
    {
        mainMenu.gameObject.SetActive(false);
    }
    public void OnButtonStartGame()
    {
        buttonStartGame.gameObject.SetActive(true);
    }
    public void OffButtonStartGame()
    {
        buttonStartGame.gameObject.SetActive(false);
    }
    public void OnButtonSettings()
    {
        buttonSettings.gameObject.SetActive(true);
    }
    public void OffButtonSettings()
    {
        buttonSettings.gameObject.SetActive(false);
    }
    public void OnButtonExit()
    {
        buttonExit.gameObject.SetActive(true);
    }
    public void OffButtonExit()
    {
        buttonExit.gameObject.SetActive(false);
    }
}
