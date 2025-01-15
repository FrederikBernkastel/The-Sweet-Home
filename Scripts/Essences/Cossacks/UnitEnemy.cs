#if ENABLE_ERRORS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RAY_Cossack
{
    public class UnitEnemy : BaseUnit
    {
        public FactoryUnitEnemy factory { get; set; }

        private protected override UnityAction<BaseEssence> deathEvent { get; } = u =>
        {
            u.StartCommand(default);

            ((BaseUnit)u).stateMachine.SetState("ContextDeath");
        };
        public override BaseCommand defaultCommand => new CommandAttackAllUnit()
        {
            commandManager = this,
            essence = this,
            isDefaultCommand = BaseCommand.IsDefaultCommand.Default,
        };
        private protected override StateMachine getStateMachine
        {
            get
            {
                contextLife = new ContextEnemyUnitLife() { essence = this };

                StateMachine machine = new(
                    new("ContextLife", contextLife),
                    new("ContextDeath", new ContextDeath() { essence = this, funcNameDeath = () => factory.nameIsDeath }));

                return machine;
            }
        }
        public override void SetInfo()
        {

        }
        public override void OnReset()
        {
            base.OnReset();

            StartCommand(defaultCommand);

            SetHP(factory.hp);
        }
    }
}
#endif