#if ENABLE_ERRORS

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static BaseUnit;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEngine.UI.CanvasScaler;

public static class Utils
{
    private static Texture2D _whiteTexture;

    public static Texture2D WhiteTexture
    {
        get
        {
            if (_whiteTexture == null)
            {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }

            return _whiteTexture;
        }
    }

    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, WhiteTexture);
        GUI.color = Color.white;
    }

    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;

        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);

        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }
}
public static class UIManager
{
    public static void AddCommandsUI(IEnumerable<BaseCommandRef> commands)
    {
        foreach (var s in commands)
        {
            //var button = GameObject.Instantiate(s.button, GameStorage.Instance.storageCommand.transform);

            //button.onClick.AddListener(GameStorage.Instance.listCommands[s.nameCommand]);

            //button.GetComponentInChildren<TMP_Text>().text = s.nameButton;
        }
    }
    public static void ClearCommandsUI()
    {
        //foreach (Transform s in GameStorage.Instance.storageCommand.transform)
        //{
        //    GameObject.Destroy(s.gameObject);
        //}
    }
}
public class SelectingState : BaseState
{
    public override string nameState => "Selecting";
    private Vector3 lastPosition { get; set; }
    private bool isSelecting { get; set; }
    private Collider[] colliders => GameStorage.Instance.collidersSelecting;

    private protected override void GUI()
    {
        if (isSelecting)
        {
            var rect = Utils.GetScreenRect(lastPosition, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }

        foreach (var s in commandsOut)
        {
            s.OnGUI();
        }
    }
    private protected override void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(GameStorage.Instance.buttonMouseSelecting))
        {
            lastPosition = Input.mousePosition;

            isSelecting = true;
        }
        else if (Input.GetMouseButtonUp(GameStorage.Instance.buttonMouseSelecting))
        {
            UIManager.ClearCommandsUI();

            SelectingExt.CloseSelectAll();

            commandsOut = Enumerable.Empty<BaseCommandRef>();

            isSelecting = false;

            var ray1 = GameStorage.Instance.mainCamera.ScreenPointToRay(lastPosition);
            var ray2 = GameStorage.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);

            if (SelectingExt.CheckSelectingEssence(colliders, ray1, ray2, GameStorage.Instance.layerMaskUnit, out var count))
            {
                SelectingExt.AddToList(count, colliders, ref commandsOut);
            }
            else if (SelectingExt.CheckSelectingEssence(colliders, ray1, ray2, GameStorage.Instance.layerMaskForest, out count))
            {
                SelectingExt.AddToList(count, colliders, ref commandsOut);
            }
        }

        foreach (var s in commandsOut)
        {
            s.OnUpdate();
        }
    }
    private protected override void FixedUpdate() 
    {
        //foreach (var s in GameStorage.Instance.listEssenceSelecting)
        //{
        //    if (s is BaseUnit)
        //    {
        //        if (Physics.Raycast(GameStorage.Instance.mainCamera.ScreenPointToRay(Input.mousePosition), out var hitInfo, float.MaxValue, GameStorage.Instance.layerMaskGround))
        //        {
        //            NavMeshPath path = new(); (s as BaseUnit).agent.
        //            if (!(s as BaseUnit).agent.CalculatePath(hitInfo.point, path))
        //            {
        //                if (NavMesh.SamplePosition(hitInfo.point, out var hit, 5f, NavMesh.AllAreas))
        //                {
        //                    (s as BaseUnit).agent.CalculatePath(hit.position, path);
        //                }
        //            }
        //            Vector3? pos = default;
        //            var array = path.corners;
        //            foreach (var n in array)
        //            {
        //                Color color = path.status == NavMeshPathStatus.PathInvalid ? Color.red : path.status == NavMeshPathStatus.PathPartial ? Color.blue : Color.yellow;
        //                Debug.DrawLine(!pos.HasValue ? s.transform.position : pos.Value, n, color);

        //                if (!pos.HasValue)
        //                {
        //                    pos = n;
        //                }
        //            }
        //        }
        //    }
        //}
    }
    //private protected override void DrawGizmos()
    //{
    //    if (Physics.Raycast(GameStorage.Instance.mainCamera.ScreenPointToRay(Input.mousePosition), out var hitInfo, float.MaxValue, GameStorage.Instance.layerMaskGround))
    //    {
    //        foreach (var s in GameStorage.Instance.listEssenceSelecting)
    //        {
    //            if (s is BaseUnit)
    //            {
    //                if (NavMesh.SamplePosition(hitInfo.point, out var hit, 0.5f, NavMesh.AllAreas))
    //                {
    //                    Gizmos.DrawWireSphere(hit.position, 0.2f);
    //                }
    //            }
    //        }
    //        Gizmos.DrawWireSphere(hitInfo.point, 0.5f);
    //    }
    //}

    private IEnumerable<BaseCommandRef> commandsOut = Enumerable.Empty<BaseCommandRef>();
}
public static class SelectingExt
{
    public static BaseEssence NearestEssence(BaseEssence essence, Collider[] colliders, int count, Func<Collider, Collider> func)
    {
        Collider hit = null;

        for (int i = 0; i < count; i++)
        {
            var tmp = colliders[i];

            var coll = func(tmp);

            if (coll)
            {
                hit = hit == null ? coll :
                    Vector3.Distance(tmp.transform.position, essence.transform.position) <
                    Vector3.Distance(hit.transform.position, essence.transform.position) ? tmp : hit;
            }
        }

        return hit?.GetComponent<BaseEssence>();
    }
    public static bool CheckSelectingEssence(Collider[] colliders, Ray ray1, Ray ray2, LayerMask layerMask, out int count)
    {
        Vector3 vector1 = default;
        Vector3 vector2 = default;

        if (Physics.Raycast(ray1, out var hitInfo1, float.MaxValue, GameStorage.Instance.layerMaskGround) ||
            Physics.Raycast(ray1, out hitInfo1, float.MaxValue, GameStorage.Instance.layerMaskGroundDop))
        {
            vector1 = hitInfo1.point;
        }
        if (Physics.Raycast(ray2, out var hitInfo2, float.MaxValue, GameStorage.Instance.layerMaskGround) ||
            Physics.Raycast(ray2, out hitInfo2, float.MaxValue, GameStorage.Instance.layerMaskGroundDop))
        {
            vector2 = hitInfo2.point;
        }

        var temp1 = vector1 + (vector2 - vector1) * 0.5f;
        var temp2 = (vector2 - vector1) * 0.5f;
        temp2.y += 5;
        temp2 = new(Mathf.Abs(temp2.x), Mathf.Abs(temp2.y), Mathf.Abs(temp2.z));

        count = Physics.OverlapBoxNonAlloc(temp1, temp2, colliders, Quaternion.identity, layerMask);

        return count > 0;
    }
    public static void AddToList(int count, Collider[] colliders, ref IEnumerable<BaseCommandRef> baseCommandRefs)
    {
        UIManager.ClearCommandsUI();

        SelectingExt.CloseSelectAll();

        for (int i = 0; i < count; i++)
        {
            var essence = colliders[i].GetComponent<BaseEssence>();

            if (essence.isSelecting)
            {
                SelectingExt.SelectEssence(essence);
            }
        }

        IEnumerable<BaseCommandRef> commandRefs = default;
        SelectingExt.CommandEqualityComparer commandEqualityComparer = new();

        foreach (var s in GameStorage.Instance.listEssenceSelecting)
        {
            SelectingExt.CommonItems(ref commandRefs, s.listCommmand, commandEqualityComparer);
        }

        UIManager.AddCommandsUI(commandRefs);

        baseCommandRefs = commandRefs == null ? baseCommandRefs : commandRefs;
    }
    public static void SelectEssence(BaseEssence essence)
    {
        essence.SetSelectEssence();

        GameStorage.Instance.listEssenceSelecting.Add(essence);
    }
    public static void CloseSelectAll()
    {
        foreach (var s in GameStorage.Instance.listEssenceSelecting)
        {
            s.SetNormalEssence();
        }

        GameStorage.Instance.listEssenceSelecting.Clear();
    }
    public static void SelectAllUnit(TypeUnit typeUnit)
    {
        foreach (var s in GameStorage.Instance.listUnits)
        {
            foreach (var n in s)
            {
                if (n.typeUnit == typeUnit)
                {
                    SelectEssence(n);
                }
            }
        }
    }
    public static void CommonItems(ref IEnumerable<BaseCommandRef> commandsOut, IEnumerable<BaseCommandRef> commands, IEqualityComparer<BaseCommandRef> comparer)
    {
        if (commandsOut == null)
        {
            commandsOut = commands;
        }
        else
        {
            commandsOut.Intersect(commands, comparer);
        }
    }
    public class CommandEqualityComparer : IEqualityComparer<BaseCommandRef>
    {
        public bool Equals(BaseCommandRef b1, BaseCommandRef b2)
        {
            if (b1.typeCommand == b2.typeCommand)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(BaseCommandRef command) => (int)command.typeCommand;
    }
}

#endif