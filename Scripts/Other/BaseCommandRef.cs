#if ENABLE_ERRORS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseCommandRef
{
    public Button button { get; set; }
    public abstract string nameCommand { get; }
    public abstract TypeCommand typeCommand { get; }
    public abstract string nameButton { get; }

    public virtual void OnUpdate() { }
    public virtual void OnGUI() { }

    public enum TypeCommand
    {
        Attack,
        Movement,
        BuildMainHouse,
        Kill,
        Choopping,
    }
}
public class CommandRefAttackUnit : BaseCommandRef
{
    public static bool IsContext { get; set; }
    public override string nameButton => "À";
    public override TypeCommand typeCommand => TypeCommand.Attack;
    public override string nameCommand => "Attack";

    public override void OnUpdate()
    {
        if (!IsContext)
        {
            return;
        }
        if (Input.GetMouseButtonDown(GameStorage.Instance.buttonMouseCommand))
        {
            if (Physics.Raycast(GameStorage.Instance.mainCamera.ScreenPointToRay(Input.mousePosition), out var hit, float.MaxValue, Physics.AllLayers))
            {
                foreach (var s in GameStorage.Instance.listEssenceSelecting)
                {
                    if (s. == BaseEssence.TypeEssence.Unit)
                    {
                        foreach (var n in ((BaseUnit)s).data.tagsEnemys)
                        {
                            if (hit.transform.CompareTag(n))
                            {
                                s.StartCommand(new CommandAttackUnit()
                                {
                                    commandManager = s,
                                    essence = s,
                                    target = hit.transform.GetComponent<BaseEssence>(),
                                });

                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
public class CommandRefDeath : BaseCommandRef
{
    public override string nameButton => "Ê";
    public override TypeCommand typeCommand => TypeCommand.Kill;
    public override string nameCommand => "Kill";

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(GameStorage.Instance.keyKillUnit))
        {
            foreach (var s in GameStorage.Instance.listEssenceSelecting)
            {
                s.StartCommand(new CommandDeath()
                {
                    commandManager = s,
                    essence = s,
                });
            }
        }
    }
}
public class CommandRefMovementUnit : BaseCommandRef
{
    public static bool IsContext { get; set; }
    public override string nameButton => "Ï";
    public override TypeCommand typeCommand => TypeCommand.Movement;
    public override string nameCommand => "Movement";

    public override void OnUpdate()
    {
        if (!IsContext)
        {
            return;
        }
        if (Input.GetMouseButtonDown(GameStorage.Instance.buttonMouseCommand))
        {
            if (Physics.Raycast(GameStorage.Instance.mainCamera.ScreenPointToRay(Input.mousePosition), out var hit, float.MaxValue, GameStorage.Instance.layerMaskGround))
            {
                foreach (var s in GameStorage.Instance.listEssenceSelecting)
                {
                    if (s.typeEssence == BaseEssence.TypeEssence.Unit)
                    {
                        s.StartCommand(new CommandMovementUnit()
                        {
                            commandManager = s,
                            essence = s,
                            isExit = true,
                            target = hit.point,
                        });
                    }
                }
            }
        }
    }
}
public class CommandRefChooppingUnit : BaseCommandRef
{
    public static bool IsContext { get; set; }
    public override string nameButton => "Ð";
    public override TypeCommand typeCommand => TypeCommand.Choopping;
    public override string nameCommand => "Choopping";
    private Vector3 lastPosition { get; set; }
    private bool isSelecting { get; set; }

    public override void OnGUI()
    {
        if (IsContext && isSelecting)
        {
            var rect = Utils.GetScreenRect(lastPosition, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }
    public override void OnUpdate()
    {
        if (!IsContext)
        {
            return;
        }
        if (Input.GetMouseButtonDown(GameStorage.Instance.buttonMouseCommand))
        {
            lastPosition = Input.mousePosition;

            isSelecting = true;
        }
        else if (Input.GetMouseButtonUp(GameStorage.Instance.buttonMouseCommand))
        {
            isSelecting = false;

            var ray1 = GameStorage.Instance.mainCamera.ScreenPointToRay(lastPosition);
            var ray2 = GameStorage.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);

            foreach (var s in GameStorage.Instance.listEssenceSelecting)
            {
                if (s.typeEssence == BaseEssence.TypeEssence.Unit)
                {
                    s.StartCommand(new CommandChooppingAreaUnit()
                    {
                        commandManager = s,
                        essence = s,
                        ray1 = ray1,
                        ray2 = ray2,
                    });
                }
            }
        }
    }
}

#endif