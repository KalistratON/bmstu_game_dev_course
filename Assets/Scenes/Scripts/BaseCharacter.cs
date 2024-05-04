using LearnGame.Movement;
using LearnGame.Shooting;
using LearnGame.PickUp;
using LearnGame.Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LearnGame {

    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController), typeof(Animator))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        public event Action<BaseCharacter> Dead;

        [SerializeField]
        private Weapon myBaseWeaponPrefab;

        [SerializeField]
        private Transform myHand;

        [field: SerializeField]
        public float myHealth { get; private set; } = 2f;

        protected IMovementDirectionSource myIMovementDirSource;

        Animator myAnimator;
        protected CharacterMovementController myCharacterMovementController;
        private ShootingController myShootingController;
        public bool IsWeaponTaken { get; private set; }  = false;

        private float myBonusAccelerationTimer = 0f;
        private float myBonusAccelerationScale = 1f;

        protected void Start()
        {
            SetWeapon(myBaseWeaponPrefab);
        }

        protected void Awake()
        {
            myIMovementDirSource = GetComponent<IMovementDirectionSource>();

            myAnimator = GetComponent<Animator>();
            myCharacterMovementController = GetComponent<CharacterMovementController>();
            myShootingController = GetComponent<ShootingController>();
        }

        protected virtual void Update()
        {
            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Demise"))
            {
                if (myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9999999)
                {
                    Destroy(gameObject);
                }
                return;
            } else if (!myCharacterMovementController) {
                return;
            }

            myBonusAccelerationTimer -= Time.deltaTime;
            if (myBonusAccelerationTimer < 0f)
            {
                myBonusAccelerationScale = 1f;
            }
            myCharacterMovementController.SpeedMultiplier = myBonusAccelerationScale;
            var direction = myIMovementDirSource.MovementDirection;
            var LookDirection = direction;
            if (myShootingController.HasTarget)
            {
                LookDirection = (myShootingController.TargetPosition - transform.position).normalized;
            }
            myCharacterMovementController.MovementDirection = direction;
            myCharacterMovementController.LookDirection = LookDirection;
            myCharacterMovementController.IsRunning = myIMovementDirSource.IsRunning;

            myAnimator.SetBool("IsMoving", direction != Vector3.zero);
            myAnimator.SetBool("IsShooting", myShootingController.HasTarget);

            if (myHealth <= 0)
            {
                Dead?.Invoke(this);
                if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Demise"))
                {
                    myAnimator.SetTrigger("Dead");
                    myCharacterMovementController.SpeedMultiplier = 0.0f;
                    myCharacterMovementController = null;
                }
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();
                myHealth -= bullet.Damage;
                Destroy(other.gameObject);
            }
            else if (LayerUtils.IsPickUp(other.gameObject))
            {
                if (other.gameObject.GetComponent<PickUpWeapon>())
                {
                    var pickUp = other.gameObject.GetComponent<PickUpWeapon>();
                    pickUp.PickUp(this);
                    IsWeaponTaken = true;
                }
                else if (other.gameObject.GetComponent<PickUpAcceleration>())
                {
                    var pickUp = other.gameObject.GetComponent<PickUpAcceleration>();
                    pickUp.PickUp(this);
                }
                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            myShootingController.SetWeapon(weapon, myHand);
        }

        public void SetBonusAcceleration (float scale, float seconds)
        {
            myBonusAccelerationTimer = seconds;
            myBonusAccelerationScale = scale;
    }
    }

}