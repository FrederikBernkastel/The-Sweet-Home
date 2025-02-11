#if ENABLE_ERRORS

using Den.Tools;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.Rendering;
using UnityEngine.XR;

//public class UpdateUnitsState : BaseState
//{
//    public override string nameState => "UpdateUnits";

//    private protected override void Update()
//    {
//        foreach (var s in GameStorage.Instance.listUnits)
//        {
//            foreach (var n in s)
//            {
//                n.OnUpdate();
//            }
//        }
//    }
//    private protected override void FixedUpdate()
//    {
//        foreach (var s in GameStorage.Instance.listUnits)
//        {
//            foreach (var n in s)
//            {
//                n.OnFixedUpdate();
//            }
//        }
//    }
//    private protected override void GUI()
//    {
//        foreach (var s in GameStorage.Instance.listUnits)
//        {
//            foreach (var n in s)
//            {
//                n.GUI();
//            }
//        }
//    }
//    private protected override void DrawGizmos()
//    {
//        foreach (var s in GameStorage.Instance.listUnits)
//        {
//            foreach (var n in s)
//            {
//                n.DrawGizmos();
//            }
//        }
//    }
//}
//public class UpdateForestState : BaseState
//{
//    public override string nameState => "UpdateForest";

//    private protected override void Update()
//    {
//        foreach (var s in GameStorage.Instance.listForest)
//        {
//            s.OnUpdate();
//        }
//    }
//    private protected override void FixedUpdate()
//    {
//        foreach (var s in GameStorage.Instance.listForest)
//        {
//            s.OnFixedUpdate();
//        }
//    }
//    private protected override void GUI()
//    {
//        foreach (var s in GameStorage.Instance.listForest)
//        {
//            s.GUI();
//        }
//    }
//    private protected override void DrawGizmos()
//    {
//        foreach (var s in GameStorage.Instance.listForest)
//        {
//            s.DrawGizmos();
//        }
//    }
//}
//public class TimerSpawnerEnemy : BaseTimerSpawnerUnit
//{
//    public override string nameState => "TimerSpawnerEnemy";
//    private protected override UnityEngine.Pool.ObjectPool<BaseUnit> objectPool => GameStorage.Instance.objectPoolEnemy;
//    private protected override FactoryUnits unit => GameStorage.Instance.unitEnemy;
//    private protected override Transform markSpawn => GameStorage.Instance.markSpawnEnemy1;
//    private protected override float radiusSpawnEnemy => GameStorage.Instance.radiusSpawnEnemy;
//    private protected override int timer => GameStorage.Instance.timerSpawnEnemy;
//    private protected override int countSpawnEnemy => GameStorage.Instance.countSpawnEnemy;
//}
public class TimerSpawnerEnemyState : BaseState
{
    public Transform markSpawn { get; set; }
    public float radiusSpawnEnemy { get; set; }
    public int timer { get; set; }
    public int countSpawnEnemy { get; set; }

    public override string nameState => "TimerSpawnerEnemy";

    private float _timer { get; set; }

    private protected override void Exit()
    {
        _timer = 0;
    }
    private protected override void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > timer)
        {
            SpawnerUnitExt.SpawnUnitRadius(GameStorage.Instance.factoryEnemy, markSpawn.position, radiusSpawnEnemy, countSpawnEnemy, default);

            _timer = 0;
        }
    }
}
//public class SpawnerGuardState : BaseSpawnerState
//{
//    public override string nameState => "SpawnerGuard";
//    private protected override KeyCode buttonKey => GameStorage.Instance.buttonKeySpawnUnit;
//    private protected override FactoryUnits data => GameStorage.Instance.unitGuard;
//    private protected override UnityEngine.Pool.ObjectPool<BaseUnit> objectPool => GameStorage.Instance.objectPoolGuard;
//}
public static class SpawnerUnitExt
{
    private static bool IsRaycastLayerSpawn(Vector3 position, out RaycastHit hit)
    {
        hit = default;

        if (Physics.Raycast(new Vector3(position.x, 300, position.z), Vector3.down, out _, 300, GameStorage.Instance.layerMaskForest))
        {
            return false;
        }
        else if (Physics.Raycast(new Vector3(position.x, 300, position.z), Vector3.down, out _, 300, GameStorage.Instance.layerMaskHouse))
        {
            return false;
        }
        else if (Physics.Raycast(new Vector3(position.x, 300, position.z), Vector3.down, out _, 300, GameStorage.Instance.layerMaskUnit))
        {
            return false;
        }
        else if (Physics.Raycast(new Vector3(position.x, 300, position.z), Vector3.down, out var hitInfo, 300, GameStorage.Instance.layerMaskGround))
        {
            hit = hitInfo;

            return true;
        }

        return false;
    }
    public static bool SpawnUnit<T>(BaseFactoryUnit<T> factory, Vector3 position, out T unitInfo) where T : BaseUnit
    {
        unitInfo = default;

        if (IsRaycastLayerSpawn(position, out var hit))
        {
            var unit = factory.Get();

            if (!unit)
            {
                unit.transform.position = hit.point;

                unit.agent.enabled = true;

                unit.stateMachine.SetState("ContextLife");

                unitInfo = unit;

                return true;
            }
        }

        return false;
    }
    public static void SpawnUnitRadius<T>(BaseFactoryUnit<T> factory, Vector3 position, float radius, int count, UnityAction<T> spawnAction) where T : BaseUnit
    {
        for (int i = 0; i < count; i++)
        {
            int j = 0;
            while (true)
            {
                var wu = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0) * Vector3.forward;
                var lerp = wu * Mathf.Lerp(0, radius, UnityEngine.Random.Range(0f, 1f));
                var pos = position + lerp;

                if (SpawnUnit(factory, pos, out var unitInfo))
                {
                    spawnAction?.Invoke(unitInfo);
                    break;
                }
                if (j > 100)
                {
                    break;
                }

                j++;
            }
        }
    }
}
//public abstract class BaseSpawnerState : BaseState
//{
//    private protected abstract FactoryUnits data { get; }
//    private protected abstract KeyCode buttonKey { get; }
//    private protected virtual UnityAction<BaseUnit> spawnAction { get; }
//    private protected abstract UnityEngine.Pool.ObjectPool<BaseUnit> objectPool { get; }

//    private protected override void Update()
//    {
//        if (Input.GetKeyDown(buttonKey))
//        {
//            var ray = GameStorage.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);

//            if (Physics.Raycast(ray, out _, float.MaxValue, GameStorage.Instance.layerMaskForest))
//            {
                
//            }
//            else if (Physics.Raycast(ray, out _, float.MaxValue, GameStorage.Instance.layerMaskHouse))
//            {

//            }
//            else if (Physics.Raycast(ray, out _, float.MaxValue, GameStorage.Instance.layerMaskUnit))
//            {

//            }
//            else if (Physics.Raycast(ray, out var hitF, float.MaxValue, GameStorage.Instance.layerMaskGround))
//            {
//                if (SpawnerUnitExt.SpawnUnit(objectPool, data, hitF.point, out var unitInfo))
//                {
//                    spawnAction?.Invoke(unitInfo);
//                }
//            }
//        }
//    }
//}

#endif