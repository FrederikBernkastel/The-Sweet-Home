using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private RectTransform settings;

    public void OnSettings()
    {
        settings.gameObject.SetActive(true);
    }
    public void OffSettings()
    {
        settings.gameObject.SetActive(false);
    }
}
