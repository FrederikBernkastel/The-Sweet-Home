#if ENABLE_ERRORS

using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace RAY_Cossack
{
    public class FactoryUnitShieldman : BaseAttackFactoryUnit<UnitShieldman>
    {
        [SerializeField][Required] public UnitShieldman unitPrefab;

        public override TypeUnit typeUnit => BaseFactoryUnit<UnitShieldman>.TypeUnit.Shieldman;
        public override TypeEssence typeEssence => BaseFactoryEssence<UnitShieldman>.TypeEssence.Unit;
        public override string nameEssence => "ўитоносец";
        private ObjectPool<UnitShieldman> listPool { get; set; }
        public List<BaseUnit> listUnitShieldman { get; private set; }

        public override void Init()
        {
            listUnitShieldman = new(capacity);

            listPool = new ObjectPool<UnitShieldman>(
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

                    listUnitShieldman.Add(u);

                    GameInfo.CountShieldman++;
                },
                u =>
                {
                    u.gameObject.SetActive(false);

                    u.agent.enabled = false;

                    listUnitShieldman.Remove(u);

                    GameInfo.CountShieldman--;

                    if (GameInfo.TotalFriendlyUnit == 0)
                    {
                        GameStorage.Instance.machineController.stateMachine.SetState("ContextEndGame");
                    }
                }, u => GameObject.Destroy(u), true, capacity, capacity);

            Stack<UnitShieldman> stack = new Stack<UnitShieldman>();

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
        public override UnitShieldman Get()
        {
            var ess = listPool.Get();

            var fog = GameStorage.Instance.factoryFog.Get();

            fog.startPosition = ess;
            fog.mainCamera = GameStorage.Instance.mainCamera;

            ess.correctFog = fog;

            return ess;
        }
        public override void Release(UnitShieldman obj)
        {
            GameStorage.Instance.factoryFog.Release(obj.correctFog);

            listPool.Release(obj);
        }
    }
}
#endif