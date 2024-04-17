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

        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }
        public bool IsRunning { get; set; }

        private CharacterController myCharacterController;

        protected void Awake()
        {
            myCharacterController = GetComponent<CharacterController>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
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
            var delta = MovementDirection * mySpeed * myRunMultiplier * Time.deltaTime;
            if (IsRunning)
            {
                delta *= myRunMultiplier;
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
