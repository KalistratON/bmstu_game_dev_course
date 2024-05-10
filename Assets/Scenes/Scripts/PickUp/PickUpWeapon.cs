using LearnGame.Shooting;
using UnityEngine;
using System.Collections;

namespace LearnGame.PickUp
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField]
        private WeaponFactory myWeaponFactory;

        public override void PickUp (BaseCharacterView theCharacter)
        {
            base.PickUp (theCharacter);
            theCharacter.SetWeapon (myWeaponFactory);
        }
    }
}
