using LearnGame.Movement;
using LearnGame.Shooting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnGame {

    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public class BaseCharacter : MonoBehaviour
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

        // Start is called before the first frame update
        protected void Start()
        {
            myShootingController.SetWeapon(myBaseWeaponPrefab, myHand);
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
            var direction = myIMovementDirSource.MovementDirection;
            var LookDirection = direction;
            if (myShootingController.HasTarget)
            {
                LookDirection = (myShootingController.TargetPosition - transform.position).normalized;
            }
            myCharacterMovementController.MovementDirection = direction;
            myCharacterMovementController.LookDirection = LookDirection;

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
        }
    }

}