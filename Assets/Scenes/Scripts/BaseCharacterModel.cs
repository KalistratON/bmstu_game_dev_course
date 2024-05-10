using LearnGame.Movement;
using LearnGame.Shooting;

using UnityEngine;

using System;

namespace LearnGame {

    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController), typeof(Animator))]
    public class BaseCharacterModel
    {
        public event Action Dead;


        public bool IsShooting => myShootingController.HasTarget;


        public float Health { get; private set; } = 10.0f;
        public TransformModel Transform { get; private set; }


        private readonly IMovementController myCharacterMovementController;
        private readonly ShootingController myShootingController;


        public BaseCharacterModel (IMovementController theMovementController,
                                   ShootingController theShootingController,
                                   ICharacterConfig theCharacterConfig)
        {
            myCharacterMovementController = theMovementController;
            myShootingController = theShootingController;
            Health = theCharacterConfig.Health;
        }

        public void Initialize (Vector3 thePosition, Quaternion theRotation)
        {
            Transform = new TransformModel (thePosition, theRotation);
        }

        public void Move (Vector3 theDirection)
        {
            var aLookDirection = theDirection;
            if (myShootingController.HasTarget)
            {
                aLookDirection = (myShootingController.GetTargetPosition - Transform.Position).normalized;
            }

            Transform.Position += myCharacterMovementController.Translate (theDirection);
            Transform.Rotation = myCharacterMovementController.Rotate (Transform.Rotation, aLookDirection);
        }

        public void Damage (float theDamage)
        {
            Health -= theDamage;
            if (Health <= 0f)
            {
                Dead?.Invoke();
            }
        }

        public void TryShoot (Vector3 theShotPosition)
        {
            myShootingController.TryShoot (theShotPosition);
        }

        public void SetWeapon (WeaponModel theWeapon)
        {
            myShootingController.SetWeapon (theWeapon);
        }
    }

}