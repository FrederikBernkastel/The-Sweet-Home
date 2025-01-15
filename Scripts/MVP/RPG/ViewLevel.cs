#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseViewLevel : BaseView
    {
        public BaseFriendlyCharacter CurrentCharacter { get; private protected set; } = default;
    
        public abstract void SetLevel(int level);
        public abstract void SetCurrentCharacter(BaseFriendlyCharacter character);
    }
    public class ViewLevel : BaseViewLevel
    {
        public override string Name { get; } = "ViewLevel";

        [BoxGroup("General")]
        [SerializeField][Required] private protected GameObject prefabLevel;
        [SerializeField][Required] private protected Transform refT;

        private protected override void _DisableIO()
        {
        
        }
        private protected override void _EnableIO()
        {
        
        }
        private protected override void _Hide()
        {
            refT.gameObject.SetActive(false);
        }
        private protected override void _OnAwake()
        {
        
        }
        private protected override void _OnDispose()
        {
        
        }
        private protected override void _Show()
        {
            refT.gameObject.SetActive(true);
        }

        public override void SetLevel(int level)
        {
            foreach (Transform t in refT.transform)
            {
                GameObject.Destroy(t.gameObject);
            }

            for (int i = 0; i < level; i++)
            {
                GameObject.Instantiate(prefabLevel, refT);
            }
        }
        public override void SetCurrentCharacter(BaseFriendlyCharacter character)
        {
            CurrentCharacter = character;
        }
    }
}
#endif