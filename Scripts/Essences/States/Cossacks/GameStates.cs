#if ENABLE_ERRORS

using RAY_Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.AI.Navigation;
using UnityEditor;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace RAY_Cossacks
{
    public class ContextGameState : BaseOrderState
    {
        public override string NameState => "Game";

        private protected override KeyValuePair<StateMachine, string>[] GetListStateMachine()
        {
            StateMachine cameraStateMachine = new(
                new KeyValuePair<string, BaseState>("ContextLife", new CameraLifeState()));

            //selectingStateMachine = new(
            //    new KeyValuePair<string, BaseState>("ContextLife", new SelectingState()));

            //timerSpawnEnemyStateMachine = new(
            //    new KeyValuePair<string, BaseState>("ContextLife", new TimerSpawnerEnemy()));

            //updateUnitsStateMachine = new(
            //    new KeyValuePair<string, BaseState>("ContextLife", new UpdateUnitsState()));

            //updateForestStateMachine = new(
            //    new KeyValuePair<string, BaseState>("ContextLife", new UpdateForestState()));

            KeyValuePair<StateMachine, string>[] list = new KeyValuePair<StateMachine, string>[]
            {
                new(cameraStateMachine, "ContextLife"),
                //new(selectingStateMachine, "ContextLife"),
                //new(timerSpawnEnemyStateMachine, "ContextLife"),
                //new(updateUnitsStateMachine, "ContextLife"),
                //new(updateForestStateMachine, "ContextLife"),
            };

            return list;
        }
        private protected override void Init()
        {
            //GameStorage.Instance.navMeshSurface.BuildNavMesh();

            base.Init();
        }
        private bool isFlag;
        private void UpdateNavMesh()
        {
            var surfaceBounds = new Bounds(GameStorage.Instance.navMeshSurface.center, Abs(GameStorage.Instance.navMeshSurface.size));

            var settings = GameStorage.Instance.navMeshSurface.GetBuildSettings();
            settings.preserveTilesOutsideBounds = true;

            NavMeshBuilder.UpdateNavMeshDataAsync(GameStorage.Instance.navMeshSurface.navMeshData, settings, sources, surfaceBounds);
        }
        private protected override void Update()
        {
            //if (!isFlag)
            //{
            //    CollectSources();
            //    UpdateNavMesh();
            //    isFlag = true;
            //}

            base.Update();
        }

        public static List<NavMeshBuildSource> sources = new List<NavMeshBuildSource>();
        public static List<NavMeshBuildMarkup> markups = new List<NavMeshBuildMarkup>();

        List<NavMeshBuildSource> CollectSources()
        {
            foreach (var m in NavMeshModifier.activeModifiers)
            {
                if ((GameStorage.Instance.navMeshSurface.layerMask & (1 << m.gameObject.layer)) == 0)
                    continue;
                if (!m.AffectsAgentType(GameStorage.Instance.navMeshSurface.agentTypeID))
                    continue;
                var markup = new NavMeshBuildMarkup();
                markup.root = m.transform;
                markup.overrideArea = m.overrideArea;
                markup.area = m.area;
                markup.ignoreFromBuild = m.ignoreFromBuild;
#if UNITY_2022_2_OR_NEWER
                markup.applyToChildren = m.applyToChildren;
                markup.overrideGenerateLinks = m.overrideGenerateLinks;
                markup.generateLinks = m.generateLinks;
#endif
                markups.Add(markup);
            }

#if UNITY_2022_2_OR_NEWER
            if (GameStorage.Instance.navMeshSurface.collectObjects == CollectObjects.Volume)
            {
                Matrix4x4 localToWorld = Matrix4x4.TRS(GameStorage.Instance.navMeshSurface.transform.position, GameStorage.Instance.navMeshSurface.transform.rotation, Vector3.one);
                var worldBounds = GetWorldBounds(localToWorld, GetInflatedBounds());
                NavMeshBuilder.CollectSources(
                    worldBounds, GameStorage.Instance.navMeshSurface.layerMask, GameStorage.Instance.navMeshSurface.useGeometry, 
                    GameStorage.Instance.navMeshSurface.defaultArea, false, markups, false, sources);
            }
#endif //UNITY_2022_2_OR_NEWER

            if (GameStorage.Instance.navMeshSurface.ignoreNavMeshAgent)
                sources.RemoveAll((x) => (x.component != null && x.component.gameObject.GetComponent<NavMeshAgent>() != null));

            if (GameStorage.Instance.navMeshSurface.ignoreNavMeshObstacle)
                sources.RemoveAll((x) => (x.component != null && x.component.gameObject.GetComponent<NavMeshObstacle>() != null));

            AppendModifierVolumes(ref sources);

            return sources;
        }
        Bounds GetInflatedBounds()
        {
            var settings = NavMesh.GetSettingsByID(GameStorage.Instance.navMeshSurface.agentTypeID);
            var agentRadius = settings.agentTypeID != -1 ? settings.agentRadius : 0f;

            var bounds = new Bounds(GameStorage.Instance.navMeshSurface.center, GameStorage.Instance.navMeshSurface.size);
            bounds.Expand(new Vector3(agentRadius, 0, agentRadius));
            return bounds;
        }
        static Vector3 Abs(Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }
        static Bounds GetWorldBounds(Matrix4x4 mat, Bounds bounds)
        {
            var absAxisX = Abs(mat.MultiplyVector(Vector3.right));
            var absAxisY = Abs(mat.MultiplyVector(Vector3.up));
            var absAxisZ = Abs(mat.MultiplyVector(Vector3.forward));
            var worldPosition = mat.MultiplyPoint(bounds.center);
            var worldSize = absAxisX * bounds.size.x + absAxisY * bounds.size.y + absAxisZ * bounds.size.z;
            return new Bounds(worldPosition, worldSize);
        }
        void AppendModifierVolumes(ref List<NavMeshBuildSource> sources)
        {
//#if UNITY_EDITOR
//            var myStage = StageUtility.GetStageHandle(GameStorage.Instance.navMeshSurface.gameObject);
//            if (!myStage.IsValid())
//                return;
//#endif

//            foreach (var m in NavMeshModifierVolume.activeModifiers)
//            {
//                if ((GameStorage.Instance.navMeshSurface.layerMask & (1 << m.gameObject.layer)) == 0)
//                    continue;
//                if (!m.AffectsAgentType(GameStorage.Instance.navMeshSurface.agentTypeID))
//                    continue;
//#if UNITY_EDITOR
//                if (!myStage.Contains(m.gameObject))
//                    continue;
//#endif
//                var mcenter = m.transform.TransformPoint(m.center);
//                var scale = m.transform.lossyScale;
//                var msize = new Vector3(m.size.x * Mathf.Abs(scale.x), m.size.y * Mathf.Abs(scale.y), m.size.z * Mathf.Abs(scale.z));

//                var src = new NavMeshBuildSource();
//                src.shape = NavMeshBuildSourceShape.ModifierBox;
//                src.transform = Matrix4x4.TRS(mcenter, m.transform.rotation, Vector3.one);
//                src.size = msize;
//                src.area = m.area;
//                sources.Add(src);
//            }
        }
    }
}
#endif