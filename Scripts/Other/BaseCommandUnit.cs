#if ENABLE_ERRORS

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public abstract class BaseCommand : BaseState
{
    public enum IsDefaultCommand
    {
        Default,
        NoneDefault,
    }
    
    public event UnityAction<BaseCommand> actionExit;
    public IsDefaultCommand isDefaultCommand { get; set; } = IsDefaultCommand.NoneDefault;

    public void CloseCommand()
    {
        actionExit?.Invoke(this);

        commandManager.StartCommand(essence.defaultCommand);
    }
}
public class CommandChooppingUnit : BaseCommand
{
    public override string nameState => "CommandChooppingUnit";
    public EssenceForest target { get; set; }
    private BaseUnit unit { get; set; }
    private Vector3 posTree;

    private protected override void Init()
    {
        unit = essence as BaseUnit;
    }
    private void CallbackNullCommand(BaseEssence essence)
    {
        CloseCommand();
    }
    private protected override void Enter()
    {
        if (!unit)
        {
            CloseCommand();
            return;
        }

        target.deathEventCallback += CallbackNullCommand;

        unit.agent.isStopped = false;

        if (NavMesh.SamplePosition(target.transform.position, out var hit, target.colliderDetection.bounds.extents.magnitude, NavMesh.AllAreas))
        {
            unit.target = target;
            posTree = hit.position;
        }
        else
        {
            CloseCommand();
        }
    }
    private protected override void Exit()
    {
        if (!unit)
        {
            return;
        }

        target.deathEventCallback -= CallbackNullCommand;

        unit.agent.isStopped = true;

        unit.target = default;
        unit.positionTarget = default;

        unit.contextLife.stateMachine.SetState("ContextIdle");
    }
    private protected override void Update()
    {
        if (Vector3.Distance(unit.transform.position, posTree) < unit.data.distanceMovementStop)
        {
            unit.contextLife.stateMachine.SetState("ContextChoopping");

            unit.agent.isStopped = true;
        }
        else
        {
            unit.contextLife.stateMachine.SetState("ContextRun");

            unit.positionTarget = posTree;
        }
    }
}
public class CommandChooppingAreaUnit : BaseCommand, IStartCommand
{
    public override string nameState => "CommandChooppingAreaUnit";
    private Collider[] colliders => GameStorage.Instance.collidersSelecting;
    public Ray ray1 { get; set; }
    public Ray ray2 { get; set; }
    private BaseCommand currentCommand { get; set; }

    private protected override void Enter()
    {
        if (!(essence is BaseUnit))
        {
            CloseCommand();
            return;
        }
    }
    private protected override void Update()
    {
        currentCommand?.OnUpdate();
    }
    private protected override void FixedUpdate()
    {
        currentCommand?.OnFixedUpdate();

        if (currentCommand == null)
        {
            if (SelectingExt.CheckSelectingEssence(colliders, ray1, ray2, GameStorage.Instance.layerMaskForest, out var count))
            {
                var ess = SelectingExt.NearestEssence(essence, colliders, count, BaseEssence.DefaultCallbackCheckDeath);

                if (!ess)
                {
                    CloseCommand();
                }
                else
                {
                    StartCommand(new CommandChooppingUnit()
                    {
                        target = ess as EssenceForest,
                        commandManager = this,
                        essence = essence,
                    });
                }
            }
            else
            {
                CloseCommand();
            }
        }
    }
    private protected override void GUI()
    {
        currentCommand?.OnGUI();
    }
    private protected override void DrawGizmos()
    {
        currentCommand?.OnDrawGizmos();
    }
    public void StartCommand(BaseCommand command)
    {
        currentCommand?.OnExit();

        currentCommand = command.isDefaultCommand == IsDefaultCommand.Default ? default : command;

        currentCommand?.OnInit();

        currentCommand?.OnEnter();
    }
}
public class CommandAutoAttackUnit : BaseCommand
{
    public override string nameState => "CommandAutoAttack";
    public bool isAutoAttack { get; set; }

    private BaseUnit unit { get; set; }

    private protected override void Init()
    {
        unit = essence as BaseUnit;
    }
    private protected override void Enter()
    {
        if (!unit)
        {
            CloseCommand();
            return;
        }

        unit.isAutoAttack = isAutoAttack;

        CloseCommand();
    }
}
public class CommandDeath : BaseCommand
{
    public override string nameState => "CommandKill";

    private protected override void Enter()
    {
        essence.SetHP(0);

        CloseCommand();
    }
}
public class CommandMovementUnit : BaseCommand
{
    public override string nameState => "CommandMovementUnit";
    public Vector3 target { get; set; }
    public bool isExit { get; set; } = true;

    private BaseUnit unit { get; set; }

    private protected override void Init()
    {
        unit = essence as BaseUnit;
    }
    private protected override void Enter()
    {
        if (!unit)
        {
            CloseCommand();
            return;
        }

        unit.agent.isStopped = false;
    }
    private protected override void Exit()
    {
        if (!unit)
        {
            return;
        }

        unit.agent.isStopped = true;

        unit.positionTarget = default;
    }
    private protected override void Update()
    {
        unit.contextLife.stateMachine.SetState("ContextRun");

        if (!NavMesh.SamplePosition(target, out var hit, 5f, NavMesh.AllAreas))
        {
            unit.agent.isStopped = true;
        }
        else
        {
            unit.positionTarget = hit.position;

            unit.agent.isStopped = false;
        }

        if (unit.agent.isStopped == true || Vector3.Distance(unit.transform.position, hit.position) < unit.data.distanceMovementStop)
        {
            unit.contextLife.stateMachine.SetState("ContextIdle");

            unit.agent.isStopped = true;

            if (isExit)
            {
                CloseCommand();
            }
        }
    }
}
public class CommandAttackAllUnit : BaseCommand, IStartCommand
{
    public override string nameState => "CommandAttackAll";
    private BaseCommand currentCommand { get; set; }
    private Collider[] raycastHits => GameStorage.Instance.raycastHits;
    private BaseEssence lastTarget { get; set; }
    private BaseUnit unit { get; set; }

    private protected override void Init()
    {
        unit = essence as BaseUnit;
    }
    private protected override void Enter()
    {
        if (!unit)
        {
            CloseCommand();
            return;
        }
    }
    private protected override void Update()
    {
        if (!unit.isAutoAttack)
        {
            StartCommand(default);
        }
        
        currentCommand?.OnUpdate();
    }
    private protected override void FixedUpdate()
    {
        currentCommand?.OnFixedUpdate();

        if (!unit.isAutoAttack)
        {
            return;
        }

        BaseEssence hit = default;
        
        foreach (var s in unit.data.layerMasksAttack)
        {
            var hitTemp = unit.CheckEnemys(unit.data.radius, raycastHits, s, BaseEssence.DefaultCallbackCheckDeath, (u, j) =>
            {
                foreach (var s in j)
                {
                    if (u.CompareTag(s))
                    {
                        return u;
                    }
                }

                return false;
            }, unit.data.tagsEnemys);

            hit = hit == null ? hitTemp :
                Vector3.Distance(hitTemp.transform.position, unit.transform.position) <
                Vector3.Distance(hit.transform.position, unit.transform.position) ? hitTemp : hit;
        }

        if (hit)
        {
            if (lastTarget?.ID != hit.ID)
            {
                StartCommand(new CommandAttackUnit() 
                {
                    target = hit,
                    commandManager = this,
                    essence = unit,
                });
            }
        }

        lastTarget = hit;
    }
    private protected override void GUI()
    {
        currentCommand?.OnGUI();
    }
    private protected override void DrawGizmos()
    {
        currentCommand?.OnDrawGizmos();
    }
    public void StartCommand(BaseCommand command)
    {
        currentCommand?.OnExit();

        currentCommand = command;

        currentCommand?.OnInit();

        currentCommand?.OnEnter();
    }
}
public class CommandAttackUnit : BaseCommand
{
    public override string nameState => "CommandAttackUnit";
    public BaseEssence target { get; set; }

    private BaseUnit unit { get; set; }

    private protected override void Init()
    {
        unit = essence as BaseUnit;
    }
    private void CallbackNullCommand(BaseEssence essence)
    {
        CloseCommand();
    }
    private protected override void Enter()
    {
        if (!unit)
        {
            CloseCommand();
            return;
        }

        target.deathEventCallback += CallbackNullCommand;

        unit.agent.isStopped = true;
    }
    private protected override void Exit()
    {
        if (!unit)
        {
            return;
        }

        target.deathEventCallback -= CallbackNullCommand;

        unit.agent.isStopped = true;

        unit.target = default;
        unit.positionTarget = default;

        unit.contextLife.stateMachine.SetState("ContextIdle");
    }
    private protected override void Update()
    {
        var layer = target.gameObject.layer;
        target.gameObject.layer = GameStorage.Instance.layerMaskTarget;

        Physics.Raycast(
            unit.colliderDetection.bounds.center,
            (target.colliderDetection.bounds.center - unit.colliderDetection.bounds.center).normalized,
            out var hit1,
            float.MaxValue,
            1 << GameStorage.Instance.layerMaskTarget);
        
        target.gameObject.layer = layer;

        layer = unit.gameObject.layer;
        unit.gameObject.layer = GameStorage.Instance.layerMaskTarget;

        Physics.Raycast(
            target.colliderDetection.bounds.center, 
            (unit.colliderDetection.bounds.center - target.colliderDetection.bounds.center).normalized, 
            out var hit2, 
            float.MaxValue, 
            1 << GameStorage.Instance.layerMaskTarget);

        unit.gameObject.layer = layer;

        hit3 = hit1;
        hit4 = hit2;

        if (Vector3.Distance(hit1.point, hit2.point) < unit.data.distanceStop)
        {
            unit.contextLife.stateMachine.SetState("ContextAttack");

            unit.target = target;

            unit.agent.isStopped = true;
        }
        else
        {
            unit.contextLife.stateMachine.SetState("ContextRun");

            unit.positionTarget = target.transform.position;

            unit.agent.isStopped = false;
        }
    }
    private RaycastHit hit3;
    private RaycastHit hit4;
    private protected override void DrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(hit3.point, hit4.point);
    }
}
[Serializable]
public class NavAreaMask
{
    public int mask;
}

[CustomPropertyDrawer(typeof(NavAreaMask))]
public class NavAreaMaskDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var dropdownRect = new Rect(position.x, position.y, position.width, position.height);

        var navMask = property.FindPropertyRelative("mask");
        navMask.intValue = EditorGUI.MaskField(dropdownRect, navMask.intValue, GameObjectUtility.GetNavMeshAreaNames());

        EditorGUI.EndProperty();
    }
}

#endif