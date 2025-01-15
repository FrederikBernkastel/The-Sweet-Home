#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using TMPro;
using UnityEngine;

namespace RAY_TestTask_1
{
    //public abstract class BaseViewField<T> where T: class
    //{
    //    public T Value { get; private protected set; } = default;
    
    //    public abstract void Refresh();
    //    public abstract void SetValue(T value);
    //}
    //public class ViewField<T> : BaseViewField<T> where T: class
    //{
    //    public override void Refresh() { }
    //    public override void SetValue(T value)
    //    {
    //        Value = value;
    //    }
    //}
    public class ViewCharacterInventory : BaseViewCharacterInventory
    {
        //public class ViewFieldCurrentCharacter : BaseViewField<BaseCharacter>
        //{
        //    public ViewFieldCurrentCharacter()

        //    public override void Refresh()
        //    {

        //    }
        //    public override void SetValue(BaseCharacter value)
        //    {
        //        Value = value;
        //    }
        //}

        [BoxGroup("DEBUG")]
        [SerializeField][ReadOnly] private protected int instanceID;

        public override string Name { get; } = "ViewCharacterInventory";

        [BoxGroup("Armor")]
        [SerializeField][Required] private protected Renderer helmet;
        [SerializeField][Required] private protected Renderer armor;
        [SerializeField][Required] private protected Renderer legs;
        [SerializeField][Required] private protected Renderer gloves;
        [SerializeField][Required] private protected Renderer shoes;
        [SerializeField][Required] private protected Renderer belt;

        [BoxGroup("Character")]
        [SerializeField][Required] private protected GameObject character;

        public override void SetCurrentCharacter(BaseFriendlyCharacter character)
        {
            CurrentCharacter = character;

            instanceID = CurrentCharacter?.InstanceID ?? 0;
        }
        public override void OnHelmet()
        {
            helmet.gameObject.SetActive(true);
        }
        public override void OffHelmet()
        {
            helmet.gameObject.SetActive(false);
        }
        public override void OnArmor()
        {
            armor.gameObject.SetActive(true);
        }
        public override void OffArmor()
        {
            armor.gameObject.SetActive(false);
        }
        public override void OnLegs()
        {
            legs.gameObject.SetActive(true);
        }
        public override void OffLegs()
        {
            legs.gameObject.SetActive(false);
        }
        public override void OnGloves()
        {
            gloves.gameObject.SetActive(true);
        }
        public override void OffGloves()
        {
            gloves.gameObject.SetActive(false);
        }
        public override void OnShoes()
        {
            shoes.gameObject.SetActive(true);
        }
        public override void OffShoes()
        {
            shoes.gameObject.SetActive(false);
        }
        public override void OnBelt()
        {
            belt.gameObject.SetActive(true);
        }
        public override void OffBelt()
        {
            belt.gameObject.SetActive(false);
        }

        private protected override void _OnAwake()
        {
        
        }
        private protected override void _OnDispose()
        {
        
        }
        private protected override void _EnableIO()
        {
        
        }
        private protected override void _DisableIO()
        {
        
        }
        private protected override void _Show()
        {
            character.gameObject.SetActive(true);
        }
        private protected override void _Hide()
        {
            character.gameObject.SetActive(false);
        }
    }
}
#endif