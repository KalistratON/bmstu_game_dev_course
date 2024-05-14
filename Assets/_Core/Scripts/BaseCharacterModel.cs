using LearnGame.Bonus;
using LearnGame.Movement;
using LearnGame.Shooting;
using LearnGame.Property;

using UnityEngine;

using System;


namespace LearnGame {

    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController), typeof(Animator))]
    public class BaseCharacterModel
    {
        public event Action Dead;
        public event Action AccelerationFinished;

        //animation related fields
        public event Action<float> AnimationDirection;

        public bool IsShooting => myShootingController.HasTarget;
        public bool HasWeapon => myShootingController.HasWeapon;


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

            myCharacterMovementController.Translate (theDirection);
            
            Transform.Position += myCharacterMovementController.Translate (theDirection);
            Transform.Rotation = myCharacterMovementController.Rotate (Transform.Rotation, aLookDirection);

            float anAnimDir = Vector3.Dot (theDirection, aLookDirection);
            AnimationDirection?.Invoke (anAnimDir /= Mathf.Abs (anAnimDir));
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

        public void AddBonus (Bonus.Bonus theBonus)
        {
            theBonus.OnBonusDestory += EraseBonus;

            if (myCharacterMovementController as CharacterMovementController == null)
            {
                return;
            }

            var aMovementController = myCharacterMovementController as CharacterMovementController;

            if ((theBonus as Acceleration) != null)
            {
                var aBonus = theBonus as Acceleration;
                aMovementController.SpeedScale *= aBonus.myAccelerationDescription.SpeedMultiplier;
            }
        }

        public void SetSpeedScale (float theScale)
        {
            if (myCharacterMovementController as CharacterMovementController == null)
            {
                return;
            }

            var aMovementController = myCharacterMovementController as CharacterMovementController;
            aMovementController.SpeedScale *= theScale;
        }

        public void EraseBonus (Bonus.Bonus theBonus)
        {
            theBonus.OnBonusDestory -= EraseBonus;

            if (myCharacterMovementController as CharacterMovementController == null)
            {
                return;
            }

            var aMovementController = myCharacterMovementController as CharacterMovementController;

            if ((theBonus as Acceleration) != null)
            {
                var aBonus = theBonus as Acceleration;
                aMovementController.SpeedScale /= aBonus.myAccelerationDescription.SpeedMultiplier;

                AccelerationFinished?.Invoke();
            }
        }

        public void AddProperty (Property.Property theProperty)
        {
            if (myCharacterMovementController as CharacterMovementController == null)
            {
                return;
            }

            var aMovementController = myCharacterMovementController as CharacterMovementController;

            if ((theProperty as Retreating) != null) {
                var aBonus = theProperty as Retreating;
                aMovementController.SpeedAdd += aBonus.RetreatingSpeed;
            }
        }
    }

}