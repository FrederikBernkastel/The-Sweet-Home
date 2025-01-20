using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.Video;
using Fungus;

[Serializable]
public class SettingsJson
{
    public string ScreenResolution;
    public string Mode;
    public bool Vsync;
    public string Monitor;
    public bool ThreeBuffer;
    public float Fov;
}
[Serializable]
public class Channel
{
    public VideoClip videoClip;
    public AudioClip audioClip;
}
public class ActionExtensions : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown1;
    [SerializeField] private TMP_Dropdown dropdown2;
    [SerializeField] private TMP_Dropdown dropdown3;
    [SerializeField] private TMP_Dropdown dropdown4;
    [SerializeField] private TMP_Dropdown dropdown5;

    [Space(15)]

    [SerializeField] private Slider slider1;
    [SerializeField] private TMP_Text textSlider;

    [Space(15)]

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Flowchart flowchart;

    public void CompleteSettings()
    {
        int width = 0, height = 0;

        switch (dropdown1.options[dropdown1.value].text)
        {
            case "1280x720":
                width = 1280; height = 720;
                break;
            case "1366x768":
                width = 1366; height = 768;
                break;
            case "1440x900":
                width = 1440; height = 900;
                break;
            case "1536x764":
                width = 1536; height = 764;
                break;
            case "1600x900":
                width = 1600; height = 900;
                break;
            case "1920x1080":
                width = 1920; height = 1080;
                break;
            case "2560x1440":
                width = 2560; height = 1440;
                break;
        }

        Screen.SetResolution(width, height, dropdown2.options[dropdown2.value].text == "В окне" ? 
            FullScreenMode.Windowed : FullScreenMode.FullScreenWindow);
        QualitySettings.vSyncCount = dropdown3.options[dropdown3.value].text == "On" ? 1 : 0;
        //Display.displays.First(u => u.ToString() == dropdown4.options[dropdown4.value].text).Activate();
        playerManager.SetFoV(slider1.value);

        string saveFile = Application.streamingAssetsPath + "/gamedata.json";

        SettingsJson settingsJson = new()
        {
            ScreenResolution = dropdown1.options[dropdown1.value].text,
            Mode = dropdown2.options[dropdown2.value].text,
            Vsync = dropdown3.options[dropdown3.value].text == "On",
            Monitor = dropdown4.options[dropdown3.value].text,
            ThreeBuffer = dropdown5.options[dropdown3.value].text == "On",
            Fov = slider1.value,
        };

        System.IO.File.WriteAllText(saveFile, JsonUtility.ToJson(settingsJson));
    }
    public void SetSettings()
    {
        string saveFile = Application.streamingAssetsPath + "/gamedata.json";

        if (!System.IO.File.Exists(saveFile))
        {
            SetDefaultSettings();

            SettingsJson settingsJson = new()
            {
                ScreenResolution = dropdown1.options[dropdown1.value].text,
                Mode = dropdown2.options[dropdown2.value].text,
                Vsync = dropdown3.options[dropdown3.value].text == "On",
                Monitor = dropdown4.options[dropdown3.value].text,
                ThreeBuffer = dropdown5.options[dropdown3.value].text == "On",
                Fov = slider1.value,
            };

            System.IO.File.WriteAllText(saveFile, JsonUtility.ToJson(settingsJson));
        }
        else
        {
            SettingsJson settingsJson = JsonUtility.FromJson<SettingsJson>(System.IO.File.ReadAllText(saveFile));

            dropdown1.value = dropdown1.options.IndexOf(new() { text = settingsJson.ScreenResolution });
            dropdown2.value = dropdown2.options.IndexOf(new() { text = settingsJson.Mode });
            dropdown3.value = dropdown3.options.IndexOf(new() { text = settingsJson.Vsync ? "On" : "Off" });

            dropdown4.options.Clear();
            foreach (var s in Display.displays)
            {
                dropdown4.options.Add(new(s.ToString()));
            }
            dropdown4.value = dropdown4.options.IndexOf(new() { text = settingsJson.Monitor });

            dropdown5.value = dropdown5.options.IndexOf(new() { text = settingsJson.ThreeBuffer ? "On" : "Off" });
            slider1.value = settingsJson.Fov;
        }
        textSlider.text = (int)slider1.value + "*";
    }
    public void SetDefaultSettings()
    {
        dropdown1.value = default;
        dropdown2.value = default;
        dropdown3.value = default;

        dropdown4.options.Clear();
        foreach (var s in Display.displays)
        {
            dropdown4.options.Add(new(s.ToString()));
        }
        dropdown4.value = default;

        dropdown5.value = default;
        slider1.value = slider1.maxValue - (slider1.maxValue - slider1.minValue) * 0.5f;
    }
    public void PrintLog(string log)
    {
        Debug.Log(log);
    }
    public void CallbackChangeValueSlider()
    {
        textSlider.text = (int)slider1.value + "*";
    }
    public void StartFlowchart(string name)
    {
        flowchart.SendFungusMessage(name);
    }
    public void StopPlayer()
    {
        playerManager.OffMovement();
        playerManager.OffInteractions();
        playerManager.OffMovementCamera();
    }
    public void StartPlayer()
    {
        playerManager.OnMovement();
        playerManager.OnInteractions();
        playerManager.OnMovementCamera();
    }
    public void StopFlowChart()
    {
        flowchart.StopAllBlocks();
    }
}
