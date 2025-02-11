#if ENABLE_ERRORS

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RAY_Cossack
{
    public class FactoryForest : BaseFactoryEssence<EssenceForest>
    {
        public override TypeEssence typeEssence => BaseFactoryEssence<EssenceForest>.TypeEssence.Forest;
        public override string nameEssence => "Дерево";
        public List<EssenceForest> listForest { get; private set; }

        public override void Init()
        {
            listForest = new(GameObject.FindGameObjectsWithTag("Forest").Select(u => u.GetComponent<EssenceForest>()));

            foreach (var s in listForest)
            {
                s.factory = this;
                s.maxHp = maxHp;
                s.transform.localScale = scale;

                s.OnInit();

                s.OnReset();
            }

            listCommmand = new BaseCommandRef[]
            {
                new CommandRefAttackUnit() { button = GameStorage.Instance.buttonCommandPrefab },
            };
        }
        public override EssenceForest Get()
        {
            return default;
        }
        public override void Release(EssenceForest obj)
        {
        
        }
    }
}
#endif