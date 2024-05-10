using LearnGame.Movement;
using LearnGame.Shooting;
using LearnGame.PickUp;
using LearnGame.Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LearnGame {

    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public abstract class BaseCharacterView : MonoBehaviour
    {
        public event Action<BaseCharacterView> Dead;

        [SerializeField]
        private WeaponFactory myWeaponFactory;

        [SerializeField]
        private Transform myHand;

        private Animator myAnimator;
        private CharacterController myCharacterController;
        private WeaponView myWeaponView;
        private IMovementDirectionSource myMovementDirSource;

        public BaseCharacterModel Model { get; private set; }

        protected void Start()
        {
            myAnimator = GetComponent<Animator>();
            myMovementDirSource = GetComponent<IMovementDirectionSource>();
            myCharacterController = GetComponent<CharacterController>();

            SetWeapon (myWeaponFactory);
        }

        public void Initialize (BaseCharacterModel theModel)
        {
            Model = theModel;
            Model.Initialize (transform.position, transform.rotation);
            Model.Dead += OnDeath;
        }

        protected virtual void Update()
        {
            Model.Move (myMovementDirSource.MovementDirection);

            Model.TryShoot (myWeaponView.myBulletSpawnPos.position);

            var aMoveDelta = Model.Transform.Position - transform.position;
            myCharacterController.Move (aMoveDelta);
            Model.Transform.Position = transform.position; // recalculating after unity collision check

            transform.rotation = Model.Transform.Rotation;

            myAnimator.SetBool("IsMoving", aMoveDelta != Vector3.zero);
            myAnimator.SetBool("IsShooting", Model.IsShooting);
        }

        protected void OnDestroy()
        {
            if (Model != null)
            {
                Model.Dead -= OnDeath;
            }
        }

        protected void OnTriggerEnter (Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();
                Model.Damage (bullet.Damage);
                Destroy(other.gameObject);
            }
            else if (LayerUtils.IsPickUp(other.gameObject))
            {
                if (other.gameObject.GetComponent<PickUpWeapon>())
                {
                    var pickUp = other.gameObject.GetComponent<PickUpWeapon>();
                    pickUp.PickUp (this);
                }
                else if (other.gameObject.GetComponent<PickUpAcceleration>())
                {
                    var pickUp = other.gameObject.GetComponent<PickUpAcceleration>();
                    pickUp.PickUp(this);
                }
                Destroy(other.gameObject);
            }
        }

        public void SetWeapon (WeaponFactory theWeaponFactory)
        {
            if (myWeaponView != null)
            {
                Destroy (myWeaponView.gameObject);
            } 

            myWeaponView = theWeaponFactory.Create (myHand);
            Model.SetWeapon (myWeaponView.Model);
        }

        private void OnDeath()
        {
            Dead?.Invoke (this);
            Destroy (gameObject);
        }
    }

}