#if ENABLE_ERRORSS

using Cinemachine;
using NaughtyAttributes;
using RAY_Core;
using RAY_Cossacks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

namespace RAY_Cossacks
{
    public class GameStorage : BaseStorage<GameStorage>
    {
        //[SerializeField][Required] public Terrain t;
        //[SerializeField] public float threshold;
        //[SerializeField] public int xBase;
        //[SerializeField] public int yBase;
        //[SerializeField] public int width;
        //[SerializeField] public int height;

        //[Button]
        //private void CutoffGrass()
        //{
        //    xBase = Mathf.Clamp(xBase, 0, t.terrainData.detailWidth);
        //    yBase = Mathf.Clamp(yBase, 0, t.terrainData.detailHeight);
        //    width = Mathf.Clamp(width, 0, t.terrainData.detailWidth);
        //    height = Mathf.Clamp(height, 0, t.terrainData.detailHeight);

        //    // Get all of layer zero.
        //    var map = t.terrainData.GetDetailLayer(xBase, yBase, width, height, 0);

        //    // For each pixel in the detail map...
        //    for (int y = yBase; y < height; y++)
        //    {
        //        for (int x = xBase; x < width; x++)
        //        {
        //            // If the pixel value is below the threshold then
        //            // set it to zero.
        //            if (map[x, y] < threshold)
        //            {
        //                map[x, y] = 0;
        //            }
        //        }
        //    }

        //    // Assign the modified map back.
        //    t.terrainData.SetDetailLayer(xBase, yBase, 0, map);

        //    Debug.Log(t.terrainData.detailWidth + "||" + t.terrainData.detailHeight);
        //}

        [Header("RefObjects")]
        [SerializeField][Required] public Camera mainCamera;
        [SerializeField][Required] public NavMeshSurface navMeshSurface;

        [Header("Prefabs")]
        [SerializeField][Required] public Button buttonCommandPrefab;

        [Header("RefStorage")]
        [SerializeField][Required] public Transform storageInfo;
        [SerializeField][Required] public Transform storageCommand;

        [Header("Layers")]
        [SerializeField] public LayerMask layerMaskGround;
        [SerializeField] public LayerMask layerMaskGroundDop;
        [SerializeField][Layer] public int layerMaskUnit;
        [SerializeField][Layer] public int layerMaskHouse;
        [SerializeField][Layer] public int layerMaskForest;
        [SerializeField][Layer] public int layerMaskTarget;

        [Header("CameraGeneral")]
        [SerializeField][Required] public Transform markRef;

        [Header("ScaleCamera")]
        [SerializeField][MinValue(0)] public float minValueScale = 10f;
        [SerializeField] public float maxValueScale = 20f;
        [SerializeField] public float SpeedScale = 5f;

        [Header("MovementCamera")]
        [SerializeField] public float SpeedMovement = 10f;
        [SerializeField] public float SpeedRotation = 100f;

        [Header("RotationCamera")]
        [SerializeField][Required] public CinemachineInputProvider inputProvider;
        [SerializeField][Required] public CinemachineVirtualCamera vcam;

        //[Header("Factorys")]
        //[SerializeField][Required] public FactoryUnitPeasant factoryPeasant;
        //[SerializeField][Required] public FactoryUnitGuard factoryGuard;
        //[SerializeField][Required] public FactoryUnitShieldman factoryShieldman;
        //[SerializeField][Required] public FactoryUnitEnemy factoryEnemy;
        //[SerializeField][Required] public FactoryForest factoryForest;
        //[SerializeField][Required] public FactoryFog factoryFog;

        [Header("Selecting")]
        [SerializeField][MinValue(0)] public int capacitySelecting;

        [Header("SpawnersUnit")]
        [SerializeField] public float radiusSpawnUnit = 2f;
        [SerializeField] public int countSpawnUnit = 10;

        [Header("SpawnersEnemy")]
        [SerializeField] public float radiusSpawnEnemy = 2f;
        [SerializeField] public int countSpawnEnemy = 10;
        [SerializeField] public int timerSpawnEnemy = 10;
        [SerializeField][Required] public Transform markSpawnEnemy1;

        [Header("HotKeys")]
        [SerializeField] public int buttonMouseSelecting;
        [SerializeField] public int buttonMouseCommand;
        [SerializeField] public KeyCode Up = KeyCode.W;
        [SerializeField] public KeyCode Left = KeyCode.A;
        [SerializeField] public KeyCode Right = KeyCode.D;
        [SerializeField] public KeyCode Down = KeyCode.S;
        [SerializeField] public int buttonRotation;
        [SerializeField] public KeyCode keyKillUnit = KeyCode.B;
        [SerializeField] public KeyCode buttonKeySpawnUnit = KeyCode.Q;

        [Header("ResourcesGeneral")]
        [SerializeField] public TMP_Text textWood;
        [SerializeField] public TMP_Text textFood;
        [SerializeField] public TMP_Text textStone;
        [SerializeField] public TMP_Text textGold;
        [SerializeField] public TMP_Text textIron;
        [SerializeField] public TMP_Text textCoal;
        [SerializeField] public int defaultCountWood;
        [SerializeField] public int defaultCountFood;
        [SerializeField] public int defaultCountStone;
        [SerializeField] public int defaultCountGold;
        [SerializeField] public int defaultCountIron;
        [SerializeField] public int defaultCountCoal;

        public Dictionary<string, UnityAction> listCommands { get; private set; }
        //public List<BaseEssence> listEssenceSelecting { get; private set; }
        public Collider[] collidersSelecting { get; private set; }
        public Collider[] raycastHits { get; } = new Collider[500];
        //private CommandsManager commandsManager { get; } = new();

        private protected override void _OnInit()
        {
            StateMachine = new(
                new("ContextMessageBox", new MessageBoxState()
                {
                    Message = "HELLO",
                    ExitAction = () => GameStorage.Instance.StateMachine.SetState("ContextGame"),
                    ViewMessageBox = RAY_Cossacks.MainStorage.Instance.PresenterMessageBox,
                }),
                new("ContextEndGame", new MessageBoxState()
                {
                    Message = "Вы проиграли!!!",
                    ExitAction = () => RAY_Cossacks.MainStorage.Instance.StateMachine.SetState("LoadingFromGameToGameScene"),
                    ViewMessageBox = RAY_Cossacks.MainStorage.Instance.PresenterMessageBox,
                }),
                new("ContextGame", new ContextGameState()));

            StateMachine.OnInit();

            //factoryShieldman.Init();
            //factoryPeasant.Init();
            //factoryGuard.Init();
            //factoryEnemy.Init();
            //factoryFog.Init();
            //factoryForest.Init();

            //ResourcesStorage.Init();

            //listCommands = commandsManager.SetCommandsDictionary();

            //machineController.OnInit();

            //inputProvider.enabled = false;

            //collidersSelecting = new Collider[capacitySelecting];

            //listEssenceSelecting = new(500);
        }
        private protected override void _OnDispose()
        {
        
        }
    }
    public static class GameInfo
    {
        public static int CountPeasant { get; set; }
        public static int CountGuard { get; set; }
        public static int CountEnemy { get; set; }
        public static int CountShieldman { get; set; }
        public static int TotalFriendlyUnit => CountPeasant + CountGuard + CountShieldman;
        public static int TotalEnemyUnit => CountEnemy;
    }

    //public class CommandsManager
    //{
    //    public Dictionary<string, UnityAction> SetCommandsDictionary()
    //    {
    //        Dictionary<string, UnityAction> listCommands = new()
    //        {
    //            ["Attack"] = Attack,
    //            ["Movement"] = Movement,
    //            ["StartGameStage"] = SetStartGameStage,
    //            ["NextGameStage"] = SetNextGameStage,
    //            ["Kill"] = Kill,
    //            ["EndGame"] = EndGame,
    //            ["Choopping"] = Choopping,
    //        };

    //        return listCommands;
    //    }

    //    private void Attack()
    //    {
    //        CommandRefAttackUnit.IsContext = !CommandRefAttackUnit.IsContext;

    //        Debug.Log(CommandRefAttackUnit.IsContext);
    //    }
    //    private void Movement()
    //    {
    //        CommandRefMovementUnit.IsContext = !CommandRefMovementUnit.IsContext;

    //        Debug.Log(CommandRefMovementUnit.IsContext);
    //    }
    //    private void SetStartGameStage() => GameStorage.Instance.machineController.stateMachine.SetState("ContextMessageBox");
    //    private void SetNextGameStage() => GameStorage.Instance.machineController.stateMachine.SetState("ContextGame");
    //    private void Kill()
    //    {
    //        foreach (var s in GameStorage.Instance.listEssenceSelecting)
    //        {
    //            s.StartCommand(new CommandDeath()
    //            {
    //                essence = s,
    //                commandManager = s,
    //            });
    //        }
    //    }
    //    private void EndGame()
    //    {
    //        MainStorage.Instance.machineController.stateMachine.SetState("LoadingFromGameToGameScene");
    //    }
    //    private void Choopping()
    //    {
    //        CommandRefChooppingUnit.IsContext = !CommandRefChooppingUnit.IsContext;

    //        Debug.Log(CommandRefChooppingUnit.IsContext);
    //    }
    //}
}
#endif