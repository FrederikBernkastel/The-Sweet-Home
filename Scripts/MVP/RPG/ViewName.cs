#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using TMPro;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseViewText : BaseView
    {
        public abstract void SetText(string str);
    }
    public abstract class BaseViewName : BaseViewText
    {
        public BaseFriendlyCharacter CurrentCharacter { get; private protected set; } = default;
    
        public abstract void SetName(string name);
        public abstract void SetCurrentCharacter(BaseFriendlyCharacter character);
    }
    public class ViewName : BaseViewName
    {
        public override string Name { get; } = "ViewName";

        [BoxGroup("General")]
        [SerializeField][Required] private protected TMP_Text textNameCharacter;

        private protected override void _DisableIO()
        {
        
        }
        private protected override void _EnableIO()
        {
        
        }
        private protected override void _Hide()
        {
            textNameCharacter.gameObject.SetActive(false);
        }
        private protected override void _OnAwake()
        {
        
        }
        private protected override void _OnDispose()
        {
        
        }
        private protected override void _Show()
        {
            textNameCharacter.gameObject.SetActive(true);
        }

        public override void SetText(string str)
        {
            SetName(str);
        }
        public override void SetCurrentCharacter(BaseFriendlyCharacter character)
        {
            CurrentCharacter = character;
        }
        public override void SetName(string name)
        {
            textNameCharacter.text = name;
        }
    }
}
#endif