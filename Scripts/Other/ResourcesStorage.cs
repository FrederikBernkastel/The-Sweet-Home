#if ENABLE_ERRORS

using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public static class ResourcesStorage
{
    public enum Resources
    {
        Stone,
        Wood,
        Food,
        Gold,
        Iron,
        Coal,
    }
    
    private static Coroutine coroutineKill { get; set; }
    private static StringBuilder stringBuilder { get; set; } = new StringBuilder();

    private static Dictionary<Resources, int> dicRes { get; set; }
    private static Dictionary<Resources, TMP_Text> dicText { get; set; }
    private static List<List<BaseUnit>> listUnits { get; set; }

    public static void Init()
    {
        listUnits = new() 
        { 
            GameStorage.Instance.factoryPeasant.listUnitPeasant, 
            GameStorage.Instance.factoryGuard.listUnitGuard , 
            GameStorage.Instance.factoryShieldman.listUnitShieldman,
        };

        dicRes = new Dictionary<Resources, int>()
        {
            [Resources.Wood] = 0,
            [Resources.Food] = 0,
            [Resources.Stone] = 0,
            [Resources.Gold] = 0,
            [Resources.Iron] = 0,
            [Resources.Coal] = 0,
        };

        dicText = new Dictionary<Resources, TMP_Text>()
        {
            [Resources.Wood] = GameStorage.Instance.textWood,
            [Resources.Food] = GameStorage.Instance.textFood,
            [Resources.Stone] = GameStorage.Instance.textStone,
            [Resources.Gold] = GameStorage.Instance.textGold,
            [Resources.Iron] = GameStorage.Instance.textIron,
            [Resources.Coal] = GameStorage.Instance.textCoal,
        };

        SetRes(Resources.Wood, GameStorage.Instance.defaultCountWood);
        SetRes(Resources.Food, GameStorage.Instance.defaultCountFood);
        SetRes(Resources.Stone, GameStorage.Instance.defaultCountStone);
        SetRes(Resources.Gold, GameStorage.Instance.defaultCountGold);
        SetRes(Resources.Iron, GameStorage.Instance.defaultCountIron);
        SetRes(Resources.Coal, GameStorage.Instance.defaultCountCoal);
    }
    private static IEnumerator CoroutineKill()
    {
        while (GameInfo.TotalFriendlyUnit > 0)
        {
            var list = listUnits[UnityEngine.Random.Range(0, listUnits.Count)];
            if (list.Count == 0)
            {
                continue;
            }
            var unit = list[UnityEngine.Random.Range(0, list.Count)];

            unit.StartCommand(new CommandDeath()
            {
                commandManager = unit,
                essence = unit,
            });

            if (GameInfo.TotalFriendlyUnit == 0)
            {
                break;
            }
            yield return new WaitForSeconds(5);
        }

        coroutineKill = default;
    }
    public static void IsEndRes()
    {
        if (GetRes(Resources.Food) == 0)
        {
            if (GameInfo.TotalFriendlyUnit > 0 && coroutineKill == null)
            {
                coroutineKill = GameStorage.Instance.machineController.StartCoroutine(CoroutineKill());
            }
        }
        else
        {
            if (coroutineKill != null)
            {
                GameStorage.Instance.machineController.StopCoroutine(coroutineKill);
            }
        }
    }
    public static void SetRes(Resources resources, int res)
    {
        dicRes[resources] = Mathf.Clamp(res, 0, 999999);

        dicText[resources].text = stringBuilder.Clear().Append(dicRes[resources]).ToString();
    }
    public static void AddRes(Resources resources, int res)
    {
        dicRes[resources] = Mathf.Clamp(dicRes[resources] + res, 0, 999999);

        dicText[resources].text = stringBuilder.Clear().Append(dicRes[resources]).ToString();
    }
    public static void SubRes(Resources resources, int res)
    {
        dicRes[resources] = Mathf.Clamp(dicRes[resources] - res, 0, 999999);

        dicText[resources].text = stringBuilder.Clear().Append(dicRes[resources]).ToString();
    }
    public static int GetRes(Resources resources) => dicRes[resources];
}

#endif