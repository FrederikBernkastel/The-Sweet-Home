#if ENABLE_ERRORS

using RAY_Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseViewCharacterInventory : BaseView
    {
        public bool IsOnHelmet { get; private protected set; } = false;
        public bool IsOnArmor { get; private protected set; } = false;
        public bool IsOnLegs { get; private protected set; } = false;
        public bool IsOnGloves { get; private protected set; } = false;
        public bool IsOnShoes { get; private protected set; } = false;
        public bool IsOnBelt { get; private protected set; } = false;

        public BaseFriendlyCharacter CurrentCharacter { get; private protected set; } = default;

        public abstract void OnHelmet();
        public abstract void OnArmor();
        public abstract void OnLegs();
        public abstract void OnGloves();
        public abstract void OnShoes();
        public abstract void OnBelt();
        public abstract void OffHelmet();
        public abstract void OffArmor();
        public abstract void OffLegs();
        public abstract void OffGloves();
        public abstract void OffShoes();
        public abstract void OffBelt();

        public abstract void SetCurrentCharacter(BaseFriendlyCharacter character);
    }
}
#endif