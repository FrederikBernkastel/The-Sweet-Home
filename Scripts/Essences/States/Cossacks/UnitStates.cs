#if ENABLE_ERRORS

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation.Samples;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class ContextUnitIdle : BaseState
{
    public override string nameState => "Idle";
}
public class ContextUnitMining : BaseState
{
    public override string nameState => "Mining";
    private BaseUnit unit { get; set; }
    public float timer { get; set; } = 1f;
    private float acc { get; set; }

    private protected override void Init()
    {
        unit = (BaseUnit)essence;
    }
    private protected override void Enter()
    {
        unit.animator.SetBool(unit.data.nameIsMining, true);
    }
    private protected override void Exit()
    {
        unit.animator.SetBool(unit.data.nameIsMining, false);

        acc = 0;
    }
    private protected override void Update()
    {
        //unit.transform.forward = (unit.target.transform.position - unit.transform.position).normalized;

        acc += Time.deltaTime;

        if (acc > timer)
        {
            acc = 0;

            ResourcesStorage.AddRes(ResourcesStorage.Resources.Stone, 5);

            ///////////////////////////////
        }
    }
}
public class ContextUnitWorking : BaseState
{
    public override string nameState => "Working";
    private BaseUnit unit { get; set; }
    public float timer { get; set; } = 1f;
    private float acc { get; set; }

    private protected override void Init()
    {
        unit = (BaseUnit)essence;
    }
    private protected override void Enter()
    {
        unit.animator.SetBool(unit.data.nameIsWorking, true);
    }
    private protected override void Exit()
    {
        unit.animator.SetBool(unit.data.nameIsWorking, false);

        acc = 0;
    }
    private protected override void Update()
    {
        //unit.transform.forward = (unit.target.transform.position - unit.transform.position).normalized;

        acc += Time.deltaTime;

        if (acc > timer)
        {
            acc = 0;

            ///////////////////////////////
        }
    }
}
public class ContextUnitFarming : BaseState
{
    public override string nameState => "Farming";
    private BaseUnit unit { get; set; }
    public float timer { get; set; } = 1f;
    private float acc { get; set; }

    private protected override void Init()
    {
        unit = (BaseUnit)essence;
    }
    private protected override void Enter()
    {
        unit.animator.SetBool(unit.data.nameIsFarming, true);
    }
    private protected override void Exit()
    {
        unit.animator.SetBool(unit.data.nameIsFarming, false);

        acc = 0;
    }
    private protected override void Update()
    {
        //unit.transform.forward = (unit.target.transform.position - unit.transform.position).normalized;

        acc += Time.deltaTime;

        if (acc > timer)
        {
            acc = 0;

            ResourcesStorage.AddRes(ResourcesStorage.Resources.Food, 5);

            ///////////////////////////////
        }
    }
}
public class ContextUnitChoopping : BaseState
{
    public override string nameState => "Choopping";
    private BaseUnit unit { get; set; }
    public float timer { get; set; } = 1f;
    private float acc { get; set; }

    private protected override void Init()
    {
        unit = (BaseUnit)essence;
    }
    private protected override void Enter()
    {
        unit.animator.SetBool(unit.data.nameIsChoopping, true);
    }
    private protected override void Exit()
    {
        unit.animator.SetBool(unit.data.nameIsChoopping, false);

        acc = 0;
    }
    private protected override void Update()
    {
        unit.transform.forward = (unit.target.transform.position - unit.transform.position).normalized;

        acc += Time.deltaTime;

        if (acc > timer)
        {
            acc = 0;

            ResourcesStorage.AddRes(ResourcesStorage.Resources.Wood, 5);

            unit.target?.DecHP(100);
        }
    }
}
public class ContextUnitRun : BaseState
{
    public override string nameState => "Run";
    private BaseUnit unit { get; set; }

    private protected override void Init()
    {
        unit = (BaseUnit)essence;
    }
    private protected override void Enter()
    {
        unit.animator.SetBool(unit.data.nameIsRun, true);
    }
    private protected override void Exit()
    {
        unit.animator.SetBool(unit.data.nameIsRun, false);
    }
    private protected override void Update()
    {
        unit.agent.isStopped = !unit.positionTarget.HasValue;

        if (unit.positionTarget.HasValue)
        {
            unit.animator.SetFloat("Blend", unit.agent.velocity.magnitude, 0.05f, Time.deltaTime);

            unit.agent.destination = unit.positionTarget.Value;

            unit.transform.forward = unit.agent.velocity.normalized;
        }
    }
}
public class ContextUnitAttack : BaseState
{
    public override string nameState => "Attack";

    private float acc { get; set; }
    private BaseUnit unit { get; set; }
    public float timer { get; set; } = 1f;

    private protected override void Init()
    {
        unit = (BaseUnit)essence;
    }
    private protected override void Enter()
    {
        unit.animator.SetBool(unit.data.nameIsAttack, true);
    }
    private protected override void Exit()
    {
        unit.animator.SetBool(unit.data.nameIsAttack, false);

        acc = 0;
    }
    private protected override void Update()
    {
        unit.transform.forward = (unit.target.transform.position - unit.transform.position).normalized;

        acc += Time.deltaTime;

        if (acc > timer)
        {
            acc = 0;

            unit.target?.DecHP(unit.data.damage);
        }
    }
}
public class ContextDeath : BaseState
{
    public override string nameState => "Death";
    public Func<string> funcNameDeath { get; set; }

    private protected override void Enter()
    {
        essence.animator.SetTrigger(funcNameDeath?.Invoke() ?? "None");
    }
}
public class ContextGuardUnitLife : ContextLife
{
    public float timer { get; set; } = 1f;
    private float acc { get; set; }

    private protected override void Init()
    {
        stateMachine = new(
            new("ContextIdle", new ContextUnitIdle() { essence = essence }),
            new("ContextRun", new ContextUnitRun() { essence = essence, commandManager = commandManager }),
            new("ContextAttack", new ContextUnitAttack() { essence = essence, commandManager = commandManager }),
            new("ContextChoopping", new ContextUnitChoopping() { essence = essence, commandManager = commandManager }));

        stateMachine.OnInit();
    }
    private protected override void Enter()
    {
        stateMachine.SetState("ContextIdle");
    }
    private protected override void Exit()
    {
        stateMachine.ExitMachine();
    }
    private protected override void Update()
    {
        acc += Time.deltaTime;

        if (acc > timer)
        {
            acc = 0;

            ResourcesStorage.SubRes(ResourcesStorage.Resources.Food, 0);

            ResourcesStorage.IsEndRes();
        }

        stateMachine.OnUpdate();
    }
    private protected override void FixedUpdate()
    {
        stateMachine.OnFixedUpdate();
    }
}
public class ContextEnemyUnitLife : ContextLife
{
    private protected override void Init()
    {
        stateMachine = new(
            new("ContextIdle", new ContextUnitIdle() { essence = essence }),
            new("ContextRun", new ContextUnitRun() { essence = essence, commandManager = commandManager }),
            new("ContextAttack", new ContextUnitAttack() { essence = essence, commandManager = commandManager }));

        stateMachine.OnInit();
    }
    private protected override void Enter()
    {
        stateMachine.SetState("ContextIdle");
    }
    private protected override void Exit()
    {
        stateMachine.ExitMachine();
    }
    private protected override void Update()
    {
        stateMachine.OnUpdate();
    }
    private protected override void FixedUpdate()
    {
        stateMachine.OnFixedUpdate();
    }
}
public class ContextForestLife : ContextLife
{
    
}
public abstract class ContextLife : BaseState
{
    public override string nameState => "Life";
    public StateMachine stateMachine { get; private protected set; }
}

#endif