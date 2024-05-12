using LearnGame.Bonus;
using LearnGame.Movement;
using LearnGame.Shooting;
using LearnGame.PickUp;

using UnityEngine;

using System.Collections.Generic;
using System;


namespace LearnGame {

    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public abstract class BaseCharacterView : MonoBehaviour
    {
        public event Action<BaseCharacterView> Dead;

        [SerializeField]
        private WeaponFactory myWeaponFactory;

        [SerializeField]
        private Transform myHand;

        [SerializeField]
        private ParticleSystem myBloodParticle;

        [SerializeField]
        private ParticleSystem mySpiritOutParticle;

        [SerializeField]
        private AudioSource myAudioDeathScreamSource;

        [SerializeField]
        private TrailRenderer myAccelerationTrail;

        private Animator myAnimator;
        private CharacterController myCharacterController;
        private WeaponView myWeaponView;
        protected IMovementDirectionSource myMovementDirSource;

        public BaseCharacterModel Model { get; private set; }

        protected virtual void Start()
        {
            myAnimator = GetComponent<Animator>();
            myMovementDirSource = GetComponent<IMovementDirectionSource>();
            myCharacterController = GetComponent<CharacterController>();

            SetWeapon (myWeaponFactory);

            myAccelerationTrail.gameObject.SetActive (false);
        }

        public void Initialize (BaseCharacterModel theModel)
        {
            Model = theModel;
            Model.Initialize (transform.position, transform.rotation);
            Model.Dead += OnDeath;

            Model.AccelerationFinished += FinishAcceleration;
            Model.AnimationDirection += SetAnimationDirection;
        }

        protected virtual void Update()
        {
            if (IsDeathAnimationFinished())
            {
                Dead?.Invoke (this);
                Destroy (gameObject);
                return;
            }

            if (Model == null)
            {
                return;
            }

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
                Model.AccelerationFinished -= FinishAcceleration;
                Model.AnimationDirection -= SetAnimationDirection;
            }
        }

        protected void OnTriggerEnter (Collider other)
        {
            if (LayerUtils.IsBullet (other.gameObject))
            {
                var aBullet = other.gameObject.GetComponent<BulletView>();
                Model?.Damage (aBullet.Model.Damage);

                myBloodParticle.Play();

                Destroy (other.gameObject);
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
                    pickUp.PickUp (this);
                }
                Destroy (other.gameObject);
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

        public void AddBonus (BonusFactory theBonusFactory)
        {
            var aBonus = theBonusFactory.Create (transform);
            if (aBonus as Acceleration != null)
            {
                myAccelerationTrail.gameObject.SetActive (true);
            }

            var aMesh = aBonus.GetComponent<MeshRenderer>();
            if (aMesh != null)
            {
                Destroy (aMesh);
            }

            Model.AddBonus (aBonus);
        }

        private bool IsDeathAnimationFinished()
        {
            return myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Demise") && 
                   myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9999999;
        }

        private void OnDeath()
        {
            Model = null;

            mySpiritOutParticle.Play();
            myAudioDeathScreamSource.Play();

            myAnimator.SetTrigger("Dead");
        }

        private void FinishAcceleration()
        {
            myAccelerationTrail.gameObject.SetActive (false);
        }

        private void SetAnimationDirection (float theAnimDir)
        {
            if (theAnimDir == 1.0f)
            {
                myAnimator.SetFloat ("DirectionOfAnim", theAnimDir);
            }
        }
    }

}