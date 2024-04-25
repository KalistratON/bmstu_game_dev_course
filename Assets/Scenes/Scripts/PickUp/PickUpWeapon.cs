using LearnGame.Shooting;
using UnityEngine;
using System.Collections;

namespace LearnGame.PickUp
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField]
        private Weapon WeaponPrefab;
        // Use this for initialization

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetWeapon(WeaponPrefab);
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
