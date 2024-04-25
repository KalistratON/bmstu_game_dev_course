using LearnGame.Shooting;
using UnityEngine;
using System.Collections;

namespace LearnGame.PickUp
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField]
        private Weapon WeaponPrefab;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetWeapon(WeaponPrefab);
        }
    }
}
