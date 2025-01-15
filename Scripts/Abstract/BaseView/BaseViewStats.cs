#if ENABLE_ERRORS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseViewStats : BaseView
    {
        public BaseFriendlyCharacter CurrentCharacter { get; private protected set; } = default;

        public abstract void SetAttack(int attack);
        public abstract void SetSpeed(int speed);
        public abstract void SetLife(int life);
        public abstract void SetDefense(int defense);

        public abstract void SetCurrentCharacter(BaseFriendlyCharacter character);
    }
}
#endif