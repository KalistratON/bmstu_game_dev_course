using LearnGame.Bonus;

using UnityEngine;

namespace LearnGame.PickUp
{
    public class PickUpAcceleration : PickUpItem
    {
        [SerializeField]
        BonusFactory myBonusFactory;

        public override void PickUp (BaseCharacterView theCharacter)
        {
            base.PickUp (theCharacter);
            theCharacter.AddBonus (myBonusFactory);
        }
    }
}