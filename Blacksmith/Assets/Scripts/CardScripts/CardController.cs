using DG.Tweening;
using UnityEngine;

namespace Manabound.Card
{
    public class CardController
    {
        public interface ICardView
        {
            string Name { get; set; }
            string Value { get; set; }
            Sprite CardSprite { set; }
            Color CardColor { set; }
            bool Glow { set; }
            Sequence FlipCard(float time);
        }

        private ICardView view;
        
        public void Initialize(ICardView view)
        {
            this.view = view;
        }
    }
}