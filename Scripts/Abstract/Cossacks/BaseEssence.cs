#if ENABLE_ERRORS

using Den.Tools;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.UI.CanvasScaler;

namespace RAY_Cossacks
{
    public interface IStartCommand
    {
        public void StartCommand(BaseCommand baseCommand);
    }
    public abstract class BaseEssence : MonoBehaviour, IStartCommand
    {
        public static int IDTotal { get; set; }

        [SerializeField][Required] public Animator animator;
        [SerializeField][Required] public Collider colliderDetection;
        [SerializeField][Required] public Renderer render;
        [SerializeField][ReadOnly] public int hp;
        [SerializeField][ReadOnly] public int maxHp;
        [SerializeField][ReadOnly] public int ID;

        public event UnityAction<BaseEssence> deathEventCallback;
        private protected abstract UnityAction<BaseEssence> deathEvent { get; }
        public StateMachine stateMachine { get; private set; }
        private protected abstract StateMachine getStateMachine { get; }
        private BaseCommand currentCommand { get; set; }
        public virtual BaseCommand defaultCommand => default;
        private bool isSelectingFlag { get; set; }

        public abstract void SetInfo();
        public virtual void OnInit()
        {
            IDTotal++;
            ID = IDTotal;

            stateMachine = getStateMachine;

            stateMachine.OnInit();
        }
        public virtual void OnReset()
        {
            isSelectingFlag = false;
        }
        public void SetNormalEssence()
        {
            isSelectingFlag = false;
        }
        public void SetSelectEssence()
        {
            isSelectingFlag = true;
        }
        public void SetHP(int hp)
        {
            this.hp = Mathf.Clamp(hp, 0, maxHp);

            IsDeath();
        }
        public void DecHP(int hp)
        {
            this.hp = Mathf.Clamp(this.hp - hp, 0, maxHp);

            IsDeath();
        }
        public void IncHP(int hp)
        {
            this.hp = Mathf.Clamp(this.hp + hp, 0, maxHp);

            IsDeath();
        }
        private bool IsDeath()
        {
            if (hp <= 0)
            {
                deathEventCallback?.Invoke(this);

                deathEvent?.Invoke(this);

                return true;
            }

            return false;
        }
        public static Collider DefaultCallbackCheckDeath(Collider collider)
        {
            return collider.transform.GetComponent<BaseEssence>().stateMachine.currentNameState != "ContextDeath" ? collider : default;
        }
        public BaseEssence CheckEnemys(float radius, Collider[] colliders, LayerMask layerMaskEnemy, Func<Collider, Collider> func, Func<Collider, string[], bool> predicate, string[] tags)
        {
            var counter = Physics.OverlapSphereNonAlloc(
                transform.position,
                radius,
                colliders,
                layerMaskEnemy);

            Collider hit = null;

            for (int i = 0; i < counter; i++)
            {
                var tmp = colliders[i];

                if (predicate(tmp, tags))
                {
                    var coll = func(tmp);

                    if (coll)
                    {
                        hit = hit == null ? coll :
                            Vector3.Distance(tmp.transform.position, transform.position) <
                            Vector3.Distance(hit.transform.position, transform.position) ? tmp : hit;
                    }
                }
            }

            return hit?.GetComponent<BaseEssence>();
        }
        public void StartCommand(BaseCommand command)
        {
            currentCommand?.OnExit();

            currentCommand = command;

            currentCommand?.OnInit();

            currentCommand?.OnEnter();
        }
        public virtual void OnUpdate()
        {
            currentCommand?.OnUpdate();

            stateMachine.OnUpdate();
        }
        public virtual void OnFixedUpdate()
        {
            currentCommand?.OnFixedUpdate();

            stateMachine.OnFixedUpdate();
        }
        public virtual void GUI()
        {
            if (isSelectingFlag)
            {
                var pos1 = GameStorage.Instance.mainCamera.WorldToScreenPoint(render.bounds.min);
                var pos2 = GameStorage.Instance.mainCamera.WorldToScreenPoint(render.bounds.max);
                var rect = Utils.GetScreenRect(pos1, pos2);
                Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
            }
        
            currentCommand?.OnGUI();

            stateMachine.OnGUI();
        }
        public virtual void DrawGizmos()
        {
            currentCommand?.OnDrawGizmos();

            stateMachine.OnDrawGizmos();
        }
    }
    public abstract class BaseUnit : BaseEssence
    {
        [SerializeField][Required] public NavMeshAgent agent;
        [SerializeField][ReadOnly] public Vector3? positionTarget;
        [SerializeField][ReadOnly] public BaseEssence target;
        [SerializeField][ReadOnly] public float speed;
        [SerializeField] public bool isAutoAttack = true;

        public ContextLife contextLife { get; private protected set; }

        public override void OnUpdate()
        {
            agent.speed = speed;

            base.OnUpdate();
        }
    }
}
#endif