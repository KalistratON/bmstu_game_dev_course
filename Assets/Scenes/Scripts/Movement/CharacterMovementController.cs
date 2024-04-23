using UnityEngine;
using System.Collections;

namespace LearnGame.Movement {

    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;
        [SerializeField]
        private float mySpeed = 1f;
        [SerializeField]
        private float myRunMultiplier = 2f;
        [SerializeField]
        private float myRollSpeed = 1f;
        [SerializeField]
        private float myRetreatSpeed = 0.1f;

        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }
        public bool IsRunning { get; set; }
        public bool IsRetreating { get; set; }
        public float BonusMultiplier { get; set; }

        private CharacterController myCharacterController;

        protected void Awake()
        {
            myCharacterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            Translate();
            if (myRollSpeed > 0f && LookDirection != Vector3.zero)
            {
                Rotate();
            }
        }

        private void Translate()
        {
            var delta = MovementDirection * mySpeed * BonusMultiplier * Time.deltaTime;
            if (IsRunning)
            {
                delta *= myRunMultiplier;
            }
            if (IsRetreating)
            {
                delta += MovementDirection * myRetreatSpeed * Time.deltaTime;
            }
            myCharacterController.Move(delta);
        }

        private void Rotate()
        {
            var curLookDir = transform.rotation * Vector3.forward;
            float sqrMagnitude = (curLookDir - LookDirection).sqrMagnitude;
            if (sqrMagnitude > SqrEpsilon)
            {
                var aRotation = Quaternion.Slerp(transform.rotation,
                                                 Quaternion.LookRotation(LookDirection, Vector3.up),
                                                 myRollSpeed * Time.deltaTime);
                transform.rotation = aRotation;
            }
        }
    }

}
