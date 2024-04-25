using LearnGame.Movement;
using LearnGame.Shooting;
using LearnGame.PickUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnGame {

    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        private Weapon myBaseWeaponPrefab;

        [SerializeField]
        private Transform myHand;

        [SerializeField]
        private float myHealth = 2f;

        private IMovementDirectionSource myIMovementDirSource;

        private CharacterMovementController myCharacterMovementController;
        private ShootingController myShootingController;

        private float myBonusAccelerationTimer = 0f;
        private float myBonusAccelerationScale = 1f;

        // Start is called before the first frame update
        protected void Start()
        {
            SetWeapon(myBaseWeaponPrefab);
        }

        protected void Awake()
        {
            myIMovementDirSource = GetComponent<IMovementDirectionSource>();

            myCharacterMovementController = GetComponent<CharacterMovementController>();
            myShootingController = GetComponent<ShootingController>();
        }

        // Update is called once per frame
        protected void Update()
        {
            myBonusAccelerationTimer -= Time.deltaTime;
            if (myBonusAccelerationTimer < 0f)
            {
                myBonusAccelerationScale = 1f;
            }
            myCharacterMovementController.BonusMultiplier = myBonusAccelerationScale;
            var direction = myIMovementDirSource.MovementDirection;
            var LookDirection = direction;
            if (myShootingController.HasTarget)
            {
                LookDirection = (myShootingController.TargetPosition - transform.position).normalized;
            }
            myCharacterMovementController.MovementDirection = direction;
            myCharacterMovementController.LookDirection = LookDirection;
            myCharacterMovementController.IsRunning = myIMovementDirSource.IsRunning;

            if (myHealth <= 0)
            {
                Destroy(gameObject);
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