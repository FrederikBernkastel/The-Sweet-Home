#if ENABLE_ERRORS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static BaseUnit;
using static Den.Tools.Pathfinding;

namespace RAY_Cossack
{
    public class EssenceForest : BaseEssence
    {
        public FactoryForest factory { get; set; }
        public ContextLife contextLife { get; private protected set; }

        private protected override UnityAction<BaseEssence> deathEvent { get; } = u =>
        {
            u.StartCommand(default);

            u.stateMachine.SetState("ContextDeath");
        };
        private protected override StateMachine getStateMachine
        {
            get
            {
                contextLife = new ContextForestLife() { essence = this };

                StateMachine machine = new(
                    new("ContextLife", contextLife),
                    new("ContextDeath", new ContextDeath() { essence = this, funcNameDeath = () => "IsDeath" }));

                return machine;
            }
        }
        public override void SetInfo()
        {

        }
        public override void OnReset()
        {
            base.OnReset();

            SetHP(factory.hp);
        }
    }
}
#endif