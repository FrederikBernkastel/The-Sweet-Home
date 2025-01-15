#if ENABLE_ERRORS

using NaughtyAttributes;
using RAY_Core;
using TMPro;
using UnityEngine;

namespace RAY_TestTask_1
{
    public class ViewStats : BaseViewStats
    {
        public override string Name { get; } = "ViewStats";

        [BoxGroup("General")]
        [SerializeField][Required] private protected TMP_Text textAttack;
        [SerializeField][Required] private protected TMP_Text textSpeed;
        [SerializeField][Required] private protected TMP_Text textLife;
        [SerializeField][Required] private protected TMP_Text textDefense;

        private protected override void _DisableIO()
        {
        
        }
        private protected override void _EnableIO()
        {
        
        }
        private protected override void _Hide()
        {
            textAttack.gameObject.SetActive(false);
            textSpeed.gameObject.SetActive(false);
            textLife.gameObject.SetActive(false);
            textDefense.gameObject.SetActive(false);
        }
        private protected override void _OnAwake()
        {
        
        }
        private protected override void _OnDispose()
        {
        
        }
        private protected override void _Show()
        {
            textAttack.gameObject.SetActive(true);
            textSpeed.gameObject.SetActive(true);
            textLife.gameObject.SetActive(true);
            textDefense.gameObject.SetActive(true);
        }

        public override void SetAttack(int attack)
        {
            textAttack.text = "Attack " + "(" + attack + ")";
        }
        public override void SetSpeed(int speed)
        {
            textSpeed.text = "Speed " + "(" + speed + ")";
        }
        public override void SetLife(int life)
        {
            textLife.text = "Life " + "(" + life + ")";
        }
        public override void SetDefense(int defense)
        {
            textDefense.text = "Defense " + "(" + defense + ")";
        }
        public override void SetCurrentCharacter(BaseFriendlyCharacter character)
        {
            CurrentCharacter = character;
        }
    }
}
#endif