#if ENABLE_ERRORS

using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace RAY_Cossack
{
    public class FactoryUnitEnemy : BaseAttackFactoryUnit<UnitEnemy>
    {
        [SerializeField][Required] public UnitEnemy unitPrefab;

        public override TypeUnit typeUnit => BaseFactoryUnit<UnitEnemy>.TypeUnit.Enemy;
        public override TypeEssence typeEssence => BaseFactoryEssence<UnitEnemy>.TypeEssence.Unit;
        public override string nameEssence => "«лостный скелет";
        private ObjectPool<UnitEnemy> listPool { get; set; }
        public List<BaseUnit> listUnitEnemy { get; private set; }

        public override void Init()
        {
            listUnitEnemy = new(capacity);

            listPool = new ObjectPool<UnitEnemy>(
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

                    listUnitEnemy.Add(u);

                    GameInfo.CountEnemy++;
                },
                u =>
                {
                    u.gameObject.SetActive(false);

                    u.agent.enabled = false;

                    listUnitEnemy.Remove(u);

                    GameInfo.CountEnemy--;

                    if (GameInfo.TotalFriendlyUnit == 0)
                    {
                        GameStorage.Instance.machineController.stateMachine.SetState("ContextEndGame");
                    }
                }, u => GameObject.Destroy(u), true, capacity, capacity);

            Stack<UnitEnemy> stack = new Stack<UnitEnemy>();

            for (int i = 0; i < capacity; i++)
            {
                stack.Push(listPool.Get());
            }
            foreach (var s in stack)
            {
                listPool.Release(s);
            }
        }
        public override UnitEnemy Get()
        {
            return listPool.Get();
        }
        public override void Release(UnitEnemy obj)
        {
            listPool.Release(obj);
        }
    }
}
#endif