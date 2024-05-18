using UnityEngine;

using System;

namespace LearnGame.Movement
{ 

    public class JoystickPlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        public event Action<float> OnRunning;


        [SerializeField]
        private float myRunSpeedMulty = 1.0f;


        public Vector3 MovementDirection { get; private set; }
        public bool IsRunning { get; private set; } = false;


        private UnityEngine.Camera myCamera;
        private Joystick myJoystick;

        protected void Awake()
        {
            myCamera = UnityEngine.Camera.main;
            myJoystick = FindObjectOfType<Joystick>();
        }

        protected void Update()
        {
            var aHor = myJoystick.Horizontal;  
            var aVer = myJoystick.Vertical;

            var aDirection = new Vector3 (aHor, 0, aVer);
            aDirection = myCamera.transform.rotation * aDirection;
            aDirection.y = 0;

            MovementDirection = aDirection.normalized;
        }

        public void UpdateMovementDirection (Vector3 targetPosition, bool isDirect) { }
    }

}
