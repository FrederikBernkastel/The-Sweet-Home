#if ENABLE_ERRORS

using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace RAY_Cossack
{
    public class FactoryUnitPeasant : BaseFactoryUnit<UnitPeasant>
    {
        [SerializeField][Required] public UnitPeasant unitPrefab;

        [SerializeField] public string nameIsMining;
        [SerializeField] public string nameIsWorking;
        [SerializeField] public string nameIsFarming;
        [SerializeField] public string nameIsChoopping;
        [SerializeField] public string nameIsCarry;

        public override TypeUnit typeUnit => BaseFactoryUnit<UnitPeasant>.TypeUnit.Peasant;
        public override TypeEssence typeEssence => BaseFactoryEssence<UnitPeasant>.TypeEssence.Unit;
        public override string nameEssence => "Крестьянин";
        private ObjectPool<UnitPeasant> listPool { get; set; }
        public List<BaseUnit> listUnitPeasant { get; private set; }

        public override void Init()
        {
            listUnitPeasant = new(capacity);

            listPool = new ObjectPool<UnitPeasant>(
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

                    listUnitPeasant.Add(u);

                    GameInfo.CountPeasant++;
                },
                u =>
                {
                    u.gameObject.SetActive(false);

                    u.agent.enabled = false;

                    listUnitPeasant.Remove(u);

                    GameInfo.CountPeasant--;

                    if (GameInfo.TotalFriendlyUnit == 0)
                    {
                        GameStorage.Instance.machineController.stateMachine.SetState("ContextEndGame");
                    }
                }, u => GameObject.Destroy(u), true, capacity, capacity);

            Stack<UnitPeasant> stack = new Stack<UnitPeasant>();

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
                new CommandRefChooppingUnit() { button = GameStorage.Instance.buttonCommandPrefab },
            };
        }
        public override UnitPeasant Get()
        {
            var ess = listPool.Get();

            var fog = GameStorage.Instance.factoryFog.Get();

            fog.startPosition = ess;
            fog.mainCamera = GameStorage.Instance.mainCamera;

            ess.correctFog = fog;

            return ess;
        }
        public override void Release(UnitPeasant obj)
        {
            GameStorage.Instance.factoryFog.Release(obj.correctFog);

            listPool.Release(obj);
        }
    }
}
#endif