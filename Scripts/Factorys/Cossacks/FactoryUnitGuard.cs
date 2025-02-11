#if ENABLE_ERRORS

using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.UI.CanvasScaler;

namespace RAY_Cossack
{
    public class FactoryUnitGuard : BaseAttackFactoryUnit<UnitGuard>
    {
        [SerializeField][Required] private UnitGuard unitPrefab;

        public override TypeUnit typeUnit => BaseFactoryUnit<UnitGuard>.TypeUnit.Guard;
        public override TypeEssence typeEssence => BaseFactoryEssence<UnitGuard>.TypeEssence.Unit;
        public override string nameEssence => "Стражник";
        private ObjectPool<UnitGuard> listPool { get; set; }
        public List<BaseUnit> listUnitGuard { get; private set; }

        public override void Init()
        {
            listUnitGuard = new(capacity);

            listPool = new ObjectPool<UnitGuard>(
                () =>
                {
                    var temp = GameObject.Instantiate(unitPrefab, storage);

                    temp.factory = this;
                    temp.maxHp = maxHp;
                    temp.transform.localScale = scale;
                    temp.speed = speed;

                    temp.agent.enabled = false;

                    temp.OnInit();

                    return temp;
                },
                u =>
                {
                    u.gameObject.SetActive(true);

                    u.OnReset();

                    listUnitGuard.Add(u);

                    GameInfo.CountGuard++;
                },
                u =>
                {
                    u.gameObject.SetActive(false);

                    u.agent.enabled = false;

                    listUnitGuard.Remove(u);

                    GameInfo.CountGuard--;

                    if (GameInfo.TotalFriendlyUnit == 0)
                    {
                        GameStorage.Instance.machineController.stateMachine.SetState("ContextEndGame");
                    }
                }, u => GameObject.Destroy(u), true, capacity, capacity);

            Stack<UnitGuard> stack = new Stack<UnitGuard>();

            for (int i = 0; i < capacity; i++)
            {
                stack.Push(listPool.Get());
            }
            foreach (var s in stack)
            {
                listPool.Release(s);
            }

            listCommmand = new BaseCommandRef[]
            {
                new CommandRefMovementUnit() { button = GameStorage.Instance.buttonCommandPrefab },
                new CommandRefDeath() { button = GameStorage.Instance.buttonCommandPrefab },
                new CommandRefAttackUnit() { button = GameStorage.Instance.buttonCommandPrefab },
            };
        }
        public override UnitGuard Get()
        {
            var ess = listPool.Get();

            var fog = GameStorage.Instance.factoryFog.Get();

            fog.startPosition = ess;
            fog.mainCamera = GameStorage.Instance.mainCamera;

            ess.correctFog = fog;

            return ess;
        }
        public override void Release(UnitGuard obj)
        {
            GameStorage.Instance.factoryFog.Release(obj.correctFog);

            listPool.Release(obj);
        }
    }
}
#endif